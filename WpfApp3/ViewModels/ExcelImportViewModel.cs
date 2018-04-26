
using ExcelDataReader;
using InformiInventory.Commands;
using InformiInventory.Models;
using InformiInventory.ViewModels.Commands;
using InformiInventory.Views;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Telerik;
using Telerik.Windows.Data;

namespace InformiInventory.ViewModels
{
    public class ExcelImportViewModel : ViewModelBase
    {
        public ExcelImportViewModel()
        {
            ImportExcelRestockLinesCommand = new RelayCommand(ImportExcel, CanExecute_ImportExcelRestockLinesCommand);
            SaveImportedExcelRestockLinesCommand = new RelayCommand(SaveImportedExcelRestockLines, CanExecute_SaveImportedExcelRestockLinesCommand);
            DeleteImportedExcelRestockLinesCommand = new RelayCommand(DeleteImportedExcelRestockLines, CanExecute_DeleteImportedExcelRestockLinesCommand);
        }

        RadObservableCollection<RestockLineModel> _restockModelLines;
        public RadObservableCollection<RestockLineModel> RestockModelLines
        {
            get
            {
                if (_restockModelLines == null)
                {
                    _restockModelLines = new RadObservableCollection<RestockLineModel>();

                }
                return _restockModelLines;
            }
        }

        public RelayCommand ImportExcelRestockLinesCommand { get; private set; }

        public bool CanExecute_ImportExcelRestockLinesCommand(object parameter)
        {
            var vm = (ExcelImportViewModel)parameter;

            if (vm == null) return false;

            else
            {
                return true;
            }
        }

        public void ImportExcel(object parameter)
        {
            var fileDialog = new System.Windows.Forms.OpenFileDialog();

            var result = fileDialog.ShowDialog();

            var fileName = "";

            switch (result)
            {
                case System.Windows.Forms.DialogResult.OK:
                    fileName = fileDialog.FileName;
                    //TxtFile.Text = file;
                    //TxtFile.ToolTip = file;
                    break;
                case System.Windows.Forms.DialogResult.Cancel:
                default:
                    //TxtFile.Text = null;
                    //TxtFile.ToolTip = null;
                    break;
            }

            var csvlist = new List<string[]>();

            if (fileName.EndsWith(".csv"))
            {
                try
                {
                    using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateCsvReader(stream, null))
                        {
                            do
                            {
                                while (reader.Read())
                                {
                                    string[] stringArray = new string[6];

                                    for (int i = 0; i <= 5; i++)
                                    {
                                        var value = reader.GetString(i);

                                        stringArray[i] = value.ToString();
                                    }

                                    csvlist.Add(stringArray);

                                }
                            } while (reader.NextResult());
                        }
                    }

                    RestockModelLines.Clear();

                    foreach (var array in csvlist)
                    {
                        var rsm = new RestockLineModel();

                        var pos = 0;

                        int.TryParse(array[0], out pos);

                        rsm.Pos = pos;

                        rsm.GTIN = array[1];

                        rsm.ArtDesc = array[2];

                        var amt = 0;

                        int.TryParse(array[3], out amt);

                        rsm.Amt = rsm.Amt;

                        rsm.StorageName = array[5];

                        RestockModelLines.Add(rsm);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Fehler beim Importieren:\n\n{0}", ex.Message),"Fehler",MessageBoxButton.OK,MessageBoxImage.Error);
                }
            }
            else
            {
                return;
            }
        }

        public RelayCommand SaveImportedExcelRestockLinesCommand { get; private set; }

        public bool CanExecute_SaveImportedExcelRestockLinesCommand(object parameter)
        {
            var vm = (ExcelImportViewModel)parameter;

            if (vm == null) return false;

            if (vm.RestockModelLines.Count == 0) return false;

            else
            {
                return true;
            }
        }

        public void SaveImportedExcelRestockLines(object parameter)
        {
            //var navContext = (NavigationViewModel)MainWindow.Instance.NavigationPanel.DataContext;
            try
            {

                var vm = (ExcelImportViewModel)parameter;

                var storeId = App.Current.Properties["StoreId"].ToString();

                int resultStoreId;
                int.TryParse(storeId, out resultStoreId);

                if (resultStoreId == 0) throw new Exception("Benutzer ist keine Filiale zu geordnet.");

                var userId = App.Current.Properties["UserId"].ToString();
                int resultUserId;
                int.TryParse(userId, out resultUserId);

                if (resultUserId == 0) throw new Exception("Unbekannter Benutzer.");

                using (var db = new PetaPoco.Database("db"))
                {
                    using (var scope = db.GetTransaction())
                    {
                        var storages = from rsml in vm.RestockModelLines
                                     group rsml by rsml.StorageName into rsmlGroup
                                     orderby rsmlGroup.Key
                                     select rsmlGroup.Key;

                        var list = storages.ToList();


                        //var storagelist =  vm.RestockModelLines.Where(x => string.IsNullOrWhiteSpace(x.StorageName) == false).GroupBy(x => x.StorageName).ToList();

                        foreach(var storageName in list)
                        {
                            var storageId = db.FirstOrDefault<int?>("SELECT StorageId From Storages WHERE StorageName = @0;", storageName);

                            if(storageId == null)
                            {
                                db.Execute("INSERT INTO Storages(StorageName) VALUES(@0);", storageName);
                            }
                        }

                        var restockId = db.ExecuteScalar<int>("INSERT INTO Restocks(Dt, UserId) VALUES(Date('now'),@0);SELECT last_insert_rowid();", resultUserId);

                        foreach(var item in vm.RestockModelLines)
                        {
                            //db.Execute("INSERT INTO RestockLines(, StoreId, UserId) VALUES(@0, @1, @2)", restock.Date, restock.StoreId, restock.UserId);

                            var ArtId = db.FirstOrDefault<int?>("SELECT ArticleId From Articles WHERE GTIN = @0;", item.GTIN);

                            if(ArtId == null)
                            {
                                var storageId = db.ExecuteScalar<int?>("Select StorageId FROM Storages WHERE StorageName = @0;", item.StorageName);

                                ArtId = db.ExecuteScalar<int>("INSERT INTO Articles(GTIN, ADesc, StorageId) VALUES(@0, @1, @2); SELECT last_insert_rowid();", item.GTIN, item.ArtDesc, storageId);
                            }
                            db.Execute("INSERT INTO RestockLines(RestockId, Pos, ArtId, Amt) VALUES(@0, @1, @2, @3);", restockId,item.Pos,ArtId,0);
                        }

                        scope.Complete();
                    }
                }
                MessageBox.Show("Daten wurden erfolgreich importiert.", "Hinweis", MessageBoxButton.OK, MessageBoxImage.Information);
                vm.RestockModelLines.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Daten konnten nicht gespeichert werden:\n\n" + ex.Message), "Fehler");
            }
        }

        public RelayCommand DeleteImportedExcelRestockLinesCommand { get; private set; }

        public bool CanExecute_DeleteImportedExcelRestockLinesCommand(object parameter)
        {
            var vm = (ExcelImportViewModel)parameter;

            if (vm == null) return false;

            else
            {
                return true;
            }
        }

        public void DeleteImportedExcelRestockLines(object parameter)
        {
            var vm = (ExcelImportViewModel)parameter;
            vm.RestockModelLines.Clear();
        }
    }
}
