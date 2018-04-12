using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
           GetRestockModels();
        }

        int _selectedIndexStoreRestockModels;
        public int SelectedIndexStoreRestockModels
        {
            get { return _selectedIndexStoreRestockModels; }
            set
            {
                _selectedIndexStoreRestockModels = value;
                _selectedIndexStoreRestockModels = -1;
                SetProperty(ref _selectedIndexStoreRestockModels,value);
            }
        }

        int _selectedIndexTemplateRestockModels;
        public int SelectedIndexTemplateRestockModels
        {
            get { return _selectedIndexTemplateRestockModels; }
            set
            {
                _selectedIndexTemplateRestockModels = value;
                _selectedIndexTemplateRestockModels = -1;
                SetProperty(ref _selectedIndexTemplateRestockModels, value);
            }
        }


        public RestockLineModel SelectedRestockLineModel { get; set; }

        public RestockModel SelectedRestockModel { get; set; }

        public int SelectedRestock { get; set; }

        RadObservableCollection<RestockModel> _storeRestockModels;

        public RadObservableCollection<RestockModel> StoreRestockModels
        {
            get
            {
                if (_storeRestockModels == null) _storeRestockModels = new RadObservableCollection<RestockModel>();

                return _storeRestockModels;
            }
          
        }


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

        private ICommand _getRestockLineModelsCommand = null;

        public ICommand GetRestockLineModelsCommand => _getRestockLineModelsCommand ?? (_getRestockLineModelsCommand = new GetRestockLineModelsCommand(this));

        public void GetRestockLineModels()
        {
            using (var db = new PetaPoco.Database("db"))
            {
                try
                {
                    RestockLineModels.Clear();
                    
                    RestockLineModels.AddRange(db.Fetch<RestockLineModel>("SELECT a.GTIN AS GTIN, rsl.Pos AS POS, a.ADesc AS ArtDesc, s.StorageName AS StorageName, rsl.ArtId AS ArtId  FROM RestockLines rsl INNER JOIN Articles a ON rsl.ArtId = a.Id INNER JOIN Restocks r ON rsl.RestockId = r.Id LEFT JOIN Storages s ON s.Id = a.StorageId WHERE r.Id = @0 ", SelectedRestockModel.Id));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Daten konnten nicht abgerufen werden:\n\n" + ex.Message), "Fehler");
                }
            }
        }

        private ICommand _getRestockModelsCommand = null;

        public ICommand GetRestockModelsCommand => _getRestockModelsCommand ?? (_getRestockModelsCommand = new GetRestockModelsCommand(this));

        public void GetRestockModels()
        {
            using (var db = new PetaPoco.Database("db"))
            {
                try
                {
                    RestockModels.Clear();

                    RestockModels.AddRange(db.Fetch<RestockModel>("SELECT Id AS Id, Dt AS DATE,IsTemplate AS IsTemplate, IsProcd AS IsProcd FROM Restocks WHERE IsTemplate = 1"));

                    int? storeId = null;
                    var isStoreId = int.TryParse(App.Current.Properties["StoreId"].ToString(), out int resultStoreId);

                    if (isStoreId)
                    {
                        storeId = resultStoreId;
                    }

                    StoreRestockModels.AddRange(db.Fetch<RestockModel>("SELECT Id AS Id, Dt AS DATE,IsTemplate AS IsTemplate, IsProcd AS IsProcd FROM Restocks WHERE IsTemplate = 0 AND StoreId= @0", storeId));

                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Daten konnten nicht abgerufen werden:\n\n" + ex.Message), "Fehler");
                }
            }
        }

        private ICommand _saveRestockLineCommand = null;

        public ICommand SaveStoreRestockLineCommand => _saveRestockLineCommand ?? (_saveRestockLineCommand = new SaveStoreRestockLineCommand(this));

        public void SaveRestockLine(object parameter)
        {
            using (var db = new PetaPoco.Database("db"))
            {
                try
                {
                    var vm = (RestockViewModel)parameter;

                    if (vm == null) return;

                    int userId;
                    var isUserId = int.TryParse(App.Current.Properties["UserId"].ToString(), out int resultUserId);

                    if(isUserId)
                    {
                        userId = resultUserId;
                    }
                    else
                    {
                        throw new Exception("Vorgang nicht möglich, da kein Benutzer angemeld.");
                    }


                    var userName = App.Current.Properties["UserName"].ToString();

                    int storeId;
                    var isStoreId = int.TryParse(App.Current.Properties["StoreId"].ToString(), out int resultStoreId);

                    if(isStoreId)
                    {
                        storeId = resultStoreId;
                    }
                    else
                    {
                        throw new Exception("Vorgang nicht möglich, Benutzer ist keiner Filiale zugeordnet.");
                    }


                    var storeName = App.Current.Properties["StoreName"];

                    int? restockId = null;

                    if (vm.SelectedRestockModel.IsTemplate)
                    {
                        restockId =  db.Execute(sql: "INSERT INTO Restocks(Dt, StoreId, UserId, IsTemplate) VALUES(date('now'), @0, @1,@3); SELECT last_insert_rowid();", storeId, userId, 1 ) ;
                    }

                    //DB Restocks: Id ,RestockId,Pos,ArtId, Amt
                    db.Execute(sql: "INSERT INTO RestockLines(RestockId, Pos, ArtId, Amt) VALUES(@0, @1, @2, @3);SELECT last_insert_rowid();", restockId.HasValue? restockId : vm.SelectedRestockModel.Id , vm.SelectedRestockLineModel.Pos ,vm.SelectedRestockLineModel.ArtId, vm.SelectedRestockLineModel.Amt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Daten konnten nicht abgerufen werden:\n\n" + ex.Message), "Fehler");
                }
            }
        }

        private ICommand _createNewRestockModelCommand = null;

        public ICommand CreateNewRestockModelCommand => _createNewRestockModelCommand ?? (_createNewRestockModelCommand = new CreateNewRestockModelCommand(this));

        public void CreateNewRestockModel(object parameter)
        {
            using (var db = new PetaPoco.Database("db"))
            {
                try
                {
                    var vm = (RestockViewModel)parameter;

                    if (vm == null) return;

                    int userId;
                    var isUserId = int.TryParse(App.Current.Properties["UserId"].ToString(), out int resultUserId);

                    if (isUserId)
                    {
                        userId = resultUserId;
                    }
                    else
                    {
                        throw new Exception("Vorgang nicht möglich, da kein Benutzer angemeld.");
                    }

                    int storeId;
                    var isStoreId = int.TryParse(App.Current.Properties["StoreId"].ToString(), out int resultStoreId);

                    if (isStoreId)
                    {
                        storeId = resultStoreId;
                    }
                    else
                    {
                        throw new Exception("Vorgang nicht möglich, Benutzer ist keiner Filiale zugeordnet.");
                    }

                    db.Execute(sql: "INSERT INTO Restocks(Dt, StoreId, UserId, IsTemplate, TemplateId) VALUES(date('now'), @0, @1, @2, @3); SELECT last_insert_rowid();", storeId, userId, 0 ,SelectedRestockModel.Id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Daten konnten nicht abgerufen werden:\n\n" + ex.Message), "Fehler");
                }
            }
        }
    }
}
