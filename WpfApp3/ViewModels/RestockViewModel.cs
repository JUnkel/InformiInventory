using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using InformiInventory.Models;
using InformiInventory.ViewModels.Commands;
using InformiInventory.Views;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;

namespace InformiInventory.ViewModels
{
    public class RestockViewModel : ViewModelBase
    {
        public RestockViewModel()
        {
            var view = new CollectionViewSource();
            view.GroupDescriptions.Add(new PropertyGroupDescription("StoreId"));
            view.Source = RestockModels;
            RestocksView = view;
            GetRestockLinesCommand = new RelayCommand(GetRestockLines, CanExecute_GetRestockLinesCommand);
            GetRestocksCommand = new RelayCommand(GetRestocks, CanExecute_GetRestocksCommand);
            CreateRestockCommand = new RelayCommand(CreateRestock, CanExecute_CreateRestockCommand);
            DeleteRestockCommand = new RelayCommand(DeleteRestock, CanExecute_DeleteRestockCommand);
            CreateRestockLineCommand = new RelayCommand(CreateRestockLine, CanExecute_CreateRestockLineCommand);
        }

        protected virtual bool SetProperty<T>(ref T field, T value, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }

        //int _selectedIndexTemplateRestockModels;
        //public int SelectedIndexTemplateRestockModels
        //{
        //    get { return _selectedIndexTemplateRestockModels; }
        //    set
        //    {
        //        _selectedIndexTemplateRestockModels = value;
        //        _selectedIndexStoreRestockModels = -1;
        //        SetProperty(ref _selectedIndexTemplateRestockModels, value);
        //    }
        //}

        //RestockLineModel _selectedRestockLineModel;
        public RestockLineModel SelectedRestockLineModel { get; set; }

        //RestockModel _selectedRestockModel;
        public RestockModel SelectedRestockModel { get; set; }

        public int SelectedRestock { get; set; }

        //RadObservableCollection<RestockModel> _storeRestockModels;

        //public RadObservableCollection<RestockModel> StoreRestockModels
        //{
        //    get
        //    {
        //        if (_storeRestockModels == null) _storeRestockModels = new RadObservableCollection<RestockModel>();

        //        return _storeRestockModels;
        //    }

        //}

        public CollectionViewSource RestocksView { get; set; }

        RadObservableCollection<RestockModel> _restockModels;

        public RadObservableCollection<RestockModel> RestockModels
        {
            get
            {
                if (_restockModels == null) _restockModels = new RadObservableCollection<RestockModel>();

                return _restockModels;
            }
        }

        RadObservableCollection<RestockLineModel> _restockLineModels;

        public RadObservableCollection<RestockLineModel> RestockLineModels
        {
            get
            {
                if (_restockLineModels == null) _restockLineModels = new RadObservableCollection<RestockLineModel>();

                return _restockLineModels;
            }
        }


        public RelayCommand GetRestocksCommand { get; private set; }

        public void GetRestocks(object patameter)
        {
            using (var db = new PetaPoco.Database("db"))
            {
                try
                {
                    int? storeId = null;
                    var isStoreId = int.TryParse(App.Current.Properties["StoreId"].ToString(), out int resultStoreId);

                    if (isStoreId)
                    {
                        storeId = resultStoreId;
                    }

                    RestockModels.Clear();

                    RestockModels.AddRange(db.Fetch<RestockModel>("SELECT RestockId AS Id, Dt AS DATE, IsProcd AS IsProcd, StoreId AS StoreId, TemplateId AS TemplateId FROM Restocks WHERE (StoreId IS NULL OR StoreId = @0)", storeId));

                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Daten konnten nicht abgerufen werden:\n\n" + ex.Message), "Fehler");
                }
            }
        }

        public bool CanExecute_GetRestocksCommand(object parameter)
        {
            var vm = (RestockViewModel)parameter;

            if (vm == null) return false;
            else
            {
                return true;
            }
        }


        public RelayCommand GetRestockLinesCommand { get; private set; }

        public bool CanExecute_GetRestockLinesCommand(object parameter)
        {
            var selectedRestockModel = (RestockModel)parameter;

            if (selectedRestockModel == null) return false;
            else
            {
                return true;
            }
        }

        public void GetRestockLines(object parameter)
        {
            var selectedRestock = (RestockModel)parameter;

            if (selectedRestock == null) return;

            using (var db = new PetaPoco.Database("db"))
            {
                try
                {
                    RestockLineModels.Clear();

                    RestockLineModels.AddRange(db.Fetch<RestockLineModel>("SELECT a.GTIN AS GTIN, rsl.Pos AS POS, a.ADesc AS ArtDesc, s.StorageName AS StorageName, rsl.ArtId AS ArtId, rsl.Amt AS Amt, r.RestockId AS RestockId,rsl.RestockLineId AS RestockLineId FROM RestockLines rsl INNER JOIN Restocks r ON (rsl.RestockId = r.RestockId) OR (r.TemplateId) INNER JOIN Articles a ON rsl.ArtId = a.ArticleId INNER JOIN Storages s ON s.StorageId = a.StorageId WHERE r.RestockId = @0 OR r.TemplateId = @1 GROUP BY rsl.Pos, rsl.ArtId ORDER BY rsl.Pos ", selectedRestock.Id, selectedRestock.TemplateId));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Daten konnten nicht abgerufen werden:\n\n" + ex.Message), "Fehler");
                }
            }
        }


        public RelayCommand CreateRestockCommand { get; private set; }

        public void CreateRestock(object paramater)
        {
            var selectedRestock = (RestockModel)paramater;

            if (selectedRestock == null) return;

            try
            {
                if (selectedRestock.TemplateId != null) throw new Exception("Neue Bestückslisten können nur aus Vorlagen erstellt werden.");

                int? storeId = null;

                var isStoreId = int.TryParse(App.Current.Properties["StoreId"].ToString(), out int resultStoreId);

                if (isStoreId)
                {
                    storeId = resultStoreId;
                }
                else
                {
                    throw new Exception("Vorgang nicht möglich:\n\nBenutzer ist keiner Filiale zugeordnet.");
                }

                if (RestockModels.Any(x => x.StoreId == storeId && x.IsProcd == false)) throw new Exception("Vorgang nicht möglich:\n\n Bitte zunächst die offene Bestückungsliste abschließen.");

                int userId;

                var isUserId = int.TryParse(App.Current.Properties["UserId"].ToString(), out int resultUserId);

                if (isUserId)
                {
                    userId = resultUserId;
                }
                else
                {
                    throw new Exception("Vorgang nicht möglich:\n\nUnbekannter Benutzer.");
                }

                using (var db = new PetaPoco.Database("db"))
                {
                    using (var scope = db.GetTransaction())
                    {
                        var rowId = db.ExecuteScalar<int>("INSERT INTO Restocks(Dt, StoreId, UserId, TemplateId) VALUES(date('now'), @0, @1, @2);SELECT last_insert_rowid();", storeId, userId, selectedRestock.Id);

                        var restock = new RestockModel()
                        {
                            Date = DateTime.Now,
                            Id = rowId,
                            IsProcd = false,
                            StoreId = storeId,
                            UserId = userId,
                            TemplateId = selectedRestock.Id
                        };

                        scope.Complete();

                        RestockModels.Add(restock);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Daten konnten nicht abgerufen werden:\n\n" + ex.Message), "Fehler");
            }
        }

        public bool CanExecute_CreateRestockCommand(object parameter)
        {
            var selectedRestockModel = (RestockModel)parameter;

            if (selectedRestockModel == null) return false;

            else if (selectedRestockModel.TemplateId != null) return false;

            else
            {
                return true;
            }
        }


        public RelayCommand DeleteRestockCommand { get; private set; }

        public bool CanExecute_DeleteRestockCommand(object parameter)
        {
            var selectedRestockModel = (RestockModel)parameter;

            if (selectedRestockModel == null) return false;

            else
            {
                return true;
            }
        }

        public void DeleteRestock(object parameter)
        {
            var selectedRestock = (RestockModel)parameter;

            if (selectedRestock == null) return;

            if (MessageBox.Show("Soll die ausgewählte Bestückungsliste gelöscht werden?", "Frage", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.Cancel) return;

            try
            {

                using (var db = new PetaPoco.Database("db"))
                {

                    db.Execute(sql: "DELETE FROM Restocks WHERE RestockId =@0;", selectedRestock.Id);

                    db.Execute(sql: "DELETE FROM RestockLines WHERE RestockId =@0;", selectedRestock.Id);

                    RestockModels.Remove(selectedRestock);

                    RestockLineModels.Clear();

                    MessageBox.Show("Ausgewählte Bestückungsliste wurde gelöscht.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Daten konnten nicht abgerufen werden:\n\n" + ex.Message), "Fehler");
            }
        }


        public RelayCommand CreateRestockLineCommand { get; private set; }

        public bool CanExecute_CreateRestockLineCommand(object parameter)
        {
            var vm = (RestockViewModel)parameter;

            if (vm == null) return false;

            else
            {
                var id = SelectedRestockLineModel.RestockLineId;

                if(SelectedRestockLineModel.Amt != vm.RestockLineModels.FirstOrDefault(x => x.RestockLineId == id).Amt) return true;
                else
                {
                    return false;
                }
            }
        }

        public void CreateRestockLine(object parameter)
        {
            using (var db = new PetaPoco.Database("db"))
            {
                try
                {
                    var selectedRestockLine = (RestockLineModel)parameter;
                    
                    if (selectedRestockLine == null) return;
                    //DB Restocks: Id ,RestockId,Pos,ArtId, Amt

                    if (selectedRestockLine.Amt == 0) db.Execute("DELETE FROM RestockLines WHERE RestockLineId = @0"); 

                    var changes = db.ExecuteScalar<int>("Update RestockLines SET Amt = @0 WHERE RestockLineId = @1 AND RestockId =@2;Select Changes()", selectedRestockLine.Amt, selectedRestockLine.RestockLineId, selectedRestockLine.RestockId);

                    if (changes == 0)
                    {
                        var lastid = db.ExecuteScalar<int>("Select MAX(RestockLineId) FROM RestockLines");

                        selectedRestockLine.RestockLineId = db.ExecuteScalar<int>("Insert Into RestockLines(RestockLineId,RestockId, Pos, ArtId, Amt) VALUES(@0, @1, @2, @3,@4);SELECT last_insert_rowid();", lastid + 1,selectedRestockLine.RestockId, selectedRestockLine.Pos, selectedRestockLine.ArtId, selectedRestockLine.Amt);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Daten konnten nicht gespeichert werden:\n\n" + ex.Message), "Fehler", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
            }
        }

        public RelayCommand DeleteRestockModelCommand { get; private set; }
        
        //private ICommand _getRestockLineModelsCommand = null;

        //public ICommand GetRestockLineModelsCommand => _getRestockLineModelsCommand ?? (_getRestockLineModelsCommand = new RestockCommand(this));

        //private ICommand _getRestockModelsCommand = null;

        //public RelayCommand GetRestockModelsCommand { get; private set; }

        //private ICommand _createNewRestockModelCommand = null;

        //public ICommand CreateNewRestockModelCommand { get; private set; }

        //private ICommand _deleteRestockModelCommand = null;

        //public ICommand DeleteRestockModelCommand => _deleteRestockModelCommand ?? (_deleteRestockModelCommand = new DeleteRestockModelCommand(this));

        }
    }
