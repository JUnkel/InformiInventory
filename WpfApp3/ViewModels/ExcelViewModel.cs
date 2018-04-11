using DocumentFormat.OpenXml.Spreadsheet;
using ExcelDataReader;
using InformiInventory.Commands;
using InformiInventory.Models;
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
    public class ExcelViewModel : ViewModelBase
    {
        private ICommand _importExcelRestockLinesCommand = null;

        public ICommand ImportExcelRestockLinesCommand => _importExcelRestockLinesCommand ?? (_importExcelRestockLinesCommand = new ImportExcelRestockLinesCommand(this));

        private ICommand _saveImportedExcelRestockLinesCommand = null;

        public ICommand SaveImportedExcelRestockLinesCommand => _saveImportedExcelRestockLinesCommand ?? (_saveImportedExcelRestockLinesCommand = new SaveImportedExcelRestockLinesCommand(this));

        private ICommand _deleteImportedExcelRestockLinesCommand = null;

        public ICommand DeleteImportedExcelRestockLinesCommand => _deleteImportedExcelRestockLinesCommand ?? (_deleteImportedExcelRestockLinesCommand = new DeleteImportedExcelRestockLinesCommand(this));

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

        public void ImportExcel()
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
                    MessageBox.Show(string.Format("Fehler beim Importieren:\n\n{0}", ex.Message));
                }
            }
            else
            {
                return;
            }
        }

        public void SaveImportedExcelRestockLines(ExcelViewModel vm)
        {
            var navContext = (NavigationViewModel)MainWindow.Instance.NavigationPanel.DataContext;

            var restock = new RestockModel()
            {
                Date = DateTime.Now,
                UserId = navContext.CurrentUser.UserId,
                StoreId = navContext.CurrentUser.StoreId
            };

            try
            {
                using (var db = new PetaPoco.Database("db"))
                {
                    using (var scope = db.GetTransaction())
                    {
                        var restockId = db.ExecuteScalar<int>("INSERT INTO Restocks(Dt, StoreId, UserId, IsProcd, IsTemplate) VALUES(@0, @1, @2, @3, @4);SELECT last_insert_rowid();", restock.Date, restock.StoreId, restock.UserId, 0, 1);

                        foreach(var item in vm.RestockModelLines)
                        {
                            //db.Execute("INSERT INTO RestockLines(, StoreId, UserId) VALUES(@0, @1, @2)", restock.Date, restock.StoreId, restock.UserId);

                            var ArtId = db.FirstOrDefault<int?>("SELECT Id From Articles WHERE GTIN = @0", item.GTIN);

                            if(ArtId == null)
                            {
                                ArtId = db.ExecuteScalar<int>("INSERT INTO Articles(GTIN, ADesc) VALUES(@0, @1); SELECT last_insert_rowid();", item.GTIN, item.ArtDesc);
                            }
                            db.Execute("INSERT INTO RestockLines(RestockId, Pos, ArtId, Amt) VALUES(@0, @1, @2, @3);", restockId,item.Pos,ArtId,0);
                        }

                        scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Daten konnten nicht gespeichert werden:\n\n" + ex.Message), "Fehler");
            }
        }

        public void DeleteImportedExcelRestockLines()
        {
            RestockModelLines.Clear();
        }
    }
}
