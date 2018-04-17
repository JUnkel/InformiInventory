using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            GetRestockModels();
            var view = new CollectionViewSource();
            view.GroupDescriptions.Add(new PropertyGroupDescription("StoreId"));
            view.Source = RestockModels;
            RestocksView = view;
        }

        //int _selectedIndexStoreRestockModels = ;
        //public int SelectedIndexStoreRestockModels
        //{
        //    get { return _selectedIndexStoreRestockModels; }
        //    set
        //    {
        //        _selectedIndexStoreRestockModels = value;
        //        _selectedIndexTemplateRestockModels = -1;
        //        SetProperty(ref _selectedIndexStoreRestockModels,value);
        //    }
        //}

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

        private ICommand _getRestockLineModelsCommand = null;

        public ICommand GetRestockLineModelsCommand => _getRestockLineModelsCommand ?? (_getRestockLineModelsCommand = new GetRestockLineModelsCommand(this));

        public void GetRestockLineModels()
        {
            using (var db = new PetaPoco.Database("db"))
            {
                try
                {
                    if (SelectedRestockModel == null) return;

                    RestockLineModels.Clear();

                    RestockLineModels.AddRange(db.Fetch<RestockLineModel>("SELECT a.GTIN AS GTIN, rsl.Pos AS POS, a.ADesc AS ArtDesc, s.StorageName AS StorageName, rsl.ArtId AS ArtId, sum(rsl.Amt) AS Amt FROM RestockLines rsl INNER JOIN Articles a ON rsl.ArtId = a.ArticleId INNER JOIN Storages s ON s.StorageId = a.StorageId Group by rsl.Pos", SelectedRestockModel.Id,SelectedRestockModel.TemplateId));

                    //if(SelectedRestockModel.TemplateId != null)
                    //{
                    //    var tempRestock = db.Fetch<RestockLineModel>("SELECT a.GTIN AS GTIN, rsl.Pos AS POS, a.ADesc AS ArtDesc, s.StorageName AS StorageName, rsl.ArtId AS ArtId, rsl.Amt AS Amt FROM RestockLines rsl INNER JOIN Articles a ON rsl.ArtId = a.ArticleId INNER JOIN Restocks r ON rsl.RestockId = r.RestockId LEFT JOIN Storages s ON s.StorageId = a.StorageId WHERE r.RestockId = @0", SelectedRestockModel.TemplateId);

                    //    foreach(var restock in tempRestock)
                    //    {
                    //        var found = RestockLineModels.FirstOrDefault(x => x.ArtId == restock.ArtId);

                    //        if(found != null)
                    //        {
                    //            restock.Amt = found.Amt;
                    //        }
                    //    }
                    //    RestockLineModels.Clear();

                    //    RestockLineModels.AddRange(tempRestock);
                    //}
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
                    //DB Restocks: Id ,RestockId,Pos,ArtId, Amt
                    db.Execute(sql: "INSERT INTO RestockLines(RestockId, Pos, ArtId, Amt) VALUES(@0, @1, @2, @3);", SelectedRestockModel.Id, vm.SelectedRestockLineModel.Pos, vm.SelectedRestockLineModel.ArtId, vm.SelectedRestockLineModel.Amt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Daten konnten nicht gespeichert werden:\n\n" + ex.Message), "Fehler", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
            }
        }

        private ICommand _createNewRestockModelCommand = null;

        public ICommand CreateNewRestockModelCommand => _createNewRestockModelCommand ?? (_createNewRestockModelCommand = new CreateNewRestockModelCommand(this));

        public void CreateNewRestockModel(object parameter)
        {
            var vm = (RestockViewModel)parameter;

            if (vm == null) return;

            if (SelectedRestockModel == null) throw new Exception("Keine Bestückungsliste ausgewählt");

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

            if (SelectedRestockModel.StoreId !=  null) throw new Exception("Neue Bestückslisten können nur aus einer Vorlage heraus erstellt werden.");

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

            try
            {
                using (var db = new PetaPoco.Database("db"))
                {
                    using (var scope = db.GetTransaction())
                    {
                        var rowId = db.ExecuteScalar<int>("INSERT INTO Restocks(Dt, StoreId, UserId, TemplateId) VALUES(date('now'), @0, @1, @2);SELECT last_insert_rowid();", storeId, userId, SelectedRestockModel.Id);

                        var restock = new RestockModel()
                        {
                            Date = DateTime.Now,
                            Id = rowId,
                            IsProcd = false,
                            StoreId = storeId,
                            UserId = userId,
                            TemplateId = SelectedRestockModel.Id
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

       private ICommand _deleteRestockModelCommand = null;

        public ICommand DeleteRestockModelCommand => _deleteRestockModelCommand ?? (_deleteRestockModelCommand = new DeleteRestockModelCommand(this));

        public void DeleteRestockModel(object parameter)
        {
            if(MessageBox.Show("Soll die ausgewählte Bestückungsliste gelöscht werden?","Frage",MessageBoxButton.OKCancel,MessageBoxImage.Question) == MessageBoxResult.Cancel ) return ;

            using (var db = new PetaPoco.Database("db"))
            {
                try
                {
                    var vm = (RestockViewModel)parameter;

                    if (vm == null) return;

                    if (vm.SelectedRestockModel == null) return;

                    db.Execute(sql: "DELETE FROM Restocks WHERE RestockId =@0;", SelectedRestockModel.Id);

                    db.Execute(sql: "DELETE FROM RestockLines WHERE RestockId =@0;", SelectedRestockModel.Id);

                    RestockModels.Remove(SelectedRestockModel);

                    RestockLineModels.Clear();

                    MessageBox.Show("Ausgewählte Bestückungsliste wurde gelöscht.");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Daten konnten nicht abgerufen werden:\n\n" + ex.Message), "Fehler");
                }
            }
        }
    }
}