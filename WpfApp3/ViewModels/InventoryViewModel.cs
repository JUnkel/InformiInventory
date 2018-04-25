using InformiInventory.Commands;
using InformiInventory.Models;
using InformiInventory.ViewModels.Commands;
using InformiInventory.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Telerik.Windows.Data;

namespace InformiInventory.ViewModels
{
    public class InventoryViewModel : ViewModelBase
    {
        public InventoryViewModel()
        {
            var view = new CollectionViewSource();
            view.GroupDescriptions.Add(new PropertyGroupDescription("StoreId"));
            view.Source = InventoryModels;
            InventoriesView = view;
            GetInventoryLinesCommand = new RelayCommand(GetInventoryLines, CanExecute_GetinventoryLinesCommand);
            CreateInventoryCommand = new RelayCommand(CreateInventory, CanExecute_CreateInventoryCommand);
            DeleteInventoryCommand = new RelayCommand(DeleteInventory, CanExecute_DeleteInventoryCommand);
            SaveInventoryLineCommand = new RelayCommand(SaveInventoryLine, CanExecute_SaveInventoryLinesCommand);
        }

        protected virtual bool SetProperty<T>(ref T field, T value, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }

        public InventoryLineModel SelectedInventoryLineModel { get; set; }

        public InventoryModel SelectedInventoryModel { get; set; }

        public int SelectedInventory { get; set; }

        public CollectionViewSource InventoriesView { get; set; }

        RadObservableCollection<InventoryModel> _inventoriesModels;

        public RadObservableCollection<InventoryModel> InventoryModels
        {
            get
            {
                if (_inventoriesModels == null) _inventoriesModels = new RadObservableCollection<InventoryModel>();

                return _inventoriesModels;
            }
        }

        RadObservableCollection<InventoryLineModel> _inventoryLineModels;

        public RadObservableCollection<InventoryLineModel> InventoryLineModels
        {
            get
            {
                if (_inventoryLineModels == null) _inventoryLineModels = new RadObservableCollection<InventoryLineModel>();

                return _inventoryLineModels;
            }
        }

        public RelayCommand GetInventorysCommand { get; private set; }

        public void GetInventories(object patameter)
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

                    InventoryModels.Clear();

                    InventoryModels.AddRange(db.Fetch<InventoryModel>("SELECT InventoryId AS Id, Dt AS DATE, IsProcd AS IsProcd, StoreId AS StoreId, TemplateId AS TemplateId FROM Inventories WHERE (StoreId IS NULL OR StoreId = @0)", storeId));

                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Daten konnten nicht abgerufen werden:\n\n" + ex.Message), "Fehler");
                }
            }
        }

        public bool CanExecute_GetInventoriesCommand(object parameter)
        {
            var vm = (InventoryViewModel)parameter;

            if (vm == null) return false;
            else
            {
                return true;
            }
        }

        public RelayCommand GetInventoryLinesCommand { get; private set; }

        public bool CanExecute_GetinventoryLinesCommand(object parameter)
        {
            var selectedinventoryModel = (InventoryModel)parameter;

            if (selectedinventoryModel == null) return false;
            else
            {
                return true;
            }
        }

        public void GetInventoryLines(object parameter)
        {
            var selectedinventory = (InventoryModel)parameter;

            if (selectedinventory == null) return;

            using (var db = new PetaPoco.Database("db"))
            {
                try
                {
                    InventoryLineModels.Clear();

                    InventoryLineModels.AddRange(db.Fetch<InventoryLineModel>("SELECT a.GTIN AS GTIN, rsl.Pos AS POS, a.ADesc AS ArtDesc, s.StorageName AS StorageName, rsl.ArtId AS ArtId, rsl.Amt AS Amt, r.inventoryId AS inventoryId,rsl.inventoryLineId AS inventoryLineId FROM inventoryLines rsl INNER JOIN inventorys r ON (rsl.inventoryId = r.inventoryId) OR (r.TemplateId) INNER JOIN Articles a ON rsl.ArtId = a.ArticleId INNER JOIN Storages s ON s.StorageId = a.StorageId WHERE r.inventoryId = @0 OR r.TemplateId = @1 GROUP BY rsl.Pos, rsl.ArtId ORDER BY rsl.Pos ",selectedinventory.Id, selectedinventory.TemplateId));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Daten konnten nicht abgerufen werden:\n\n" + ex.Message), "Fehler");
                }
            }
        }

        public RelayCommand CreateInventoryCommand { get; private set; }

        public void CreateInventory(object paramater)
        {
            var selectedinventory = (InventoryModel)paramater;

            if (selectedinventory == null) return;

            try
            {
                using (var db = new PetaPoco.Database("db"))
                {
                    using (var scope = db.GetTransaction())
                    {
                        //var rowId = db.ExecuteScalar<int>("INSERT INTO inventorys(Dt, StoreId, UserId, TemplateId) VALUES(date('now'), @0, @1, @2);SELECT last_insert_rowid();", storeId, userId, selectedinventory.Id);

                        //var inventory = new InventoryModel()
                        //{
                        //    Date = DateTime.Now,
                        //    Id = rowId,
                        //    IsProcd = false,
                        //    StoreId = storeId,
                        //    UserId = userId,
                        //    TemplateId = selectedinventory.Id
                        //};

                        //scope.Complete();

                        //InventoryModels.Add(inventory);

                        InventoryLineModels.AddRange(db.Fetch<InventoryLineModel>("SELECT ArticleId AS ArtId, GTIN, ADesc AS ArtDesc, StorageId"));
                        InventoryModels.Add(new InventoryModel() { Date = DateTime.Now,IsProcd = false});

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Daten konnten nicht abgerufen werden:\n\n" + ex.Message), "Fehler");
            }
        }

        public bool CanExecute_CreateInventoryCommand(object parameter)
        {
            var selectedinventoryModel = (InventoryModel)parameter;

            if (selectedinventoryModel == null) return false;

            else if (selectedinventoryModel.TemplateId != null) return false;

            else
            {
                return true;
            }
        }

        public RelayCommand DeleteInventoryCommand { get; private set; }

        public RelayCommand SaveInventoryLineCommand { get; private set; }

        public bool CanExecute_DeleteInventoryCommand(object parameter)
        {
            var selectedInventoryModel = (InventoryModel)parameter;

            if (selectedInventoryModel == null) return false;

            else
            {
                return true;
            }
        }

        public void DeleteInventory(object parameter)
        {
            var selectedInventory = (InventoryModel)parameter;

            if (selectedInventory == null) return;

            if (MessageBox.Show("Soll die ausgewählte Bestückungsliste gelöscht werden?", "Frage", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.Cancel) return;

            try
            {
                using (var db = new PetaPoco.Database("db"))
                {

                    db.Execute(sql: "DELETE FROM inventorys WHERE inventoryId =@0;", selectedInventory.Id);

                    db.Execute(sql: "DELETE FROM inventoryLines WHERE inventoryId =@0;", selectedInventory.Id);

                    InventoryModels.Remove(selectedInventory);

                    InventoryModels.Clear();

                    MessageBox.Show("Ausgewählte Bestückungsliste wurde gelöscht.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Daten konnten nicht abgerufen werden:\n\n" + ex.Message), "Fehler");
            }
        }

        public RelayCommand SaveInventoryLinesCommand { get; private set; }

        public bool CanExecute_SaveInventoryLinesCommand(object parameter)
        {
            var vm = (InventoryViewModel)parameter;

            if (vm == null) return false;

            else
            {
                var id = SelectedInventoryLineModel.InventoryLineId;

                if (SelectedInventoryLineModel.Amt != vm.InventoryLineModels.FirstOrDefault(x => x.InventoryLineId == id).Amt) return true;
                else
                {
                    return false;
                }
            }
        }

        public void SaveInventoryLine(object parameter)
        {
            using (var db = new PetaPoco.Database("db"))
            {
                try
                {
                    var selectedinventoryLine = (InventoryLineModel)parameter;

                    if (selectedinventoryLine == null) return;
                    //DB inventorys: Id ,inventoryId,Pos,ArtId, Amt

                    if (selectedinventoryLine.Amt == 0) db.Execute("DELETE FROM inventoryLines WHERE inventoryLineId = @0");

                    var changes = db.ExecuteScalar<int>("Update inventoryLines SET Amt = @0 WHERE inventoryLineId = @1 AND inventoryId =@2;Select Changes()", selectedinventoryLine.Amt, selectedinventoryLine.InventoryLineId, selectedinventoryLine.InventoryId);

                    if (changes == 0)
                    {
                        var lastid = db.ExecuteScalar<int>("Select MAX(inventoryLineId) FROM inventoryLines");

                        selectedinventoryLine.InventoryLineId = db.ExecuteScalar<int>("Insert Into inventoryLines(inventoryLineId,inventoryId, Pos, ArtId, Amt) VALUES(@0, @1, @2, @3,@4);SELECT last_insert_rowid();", lastid + 1, selectedinventoryLine.InventoryLineId, selectedinventoryLine.Pos, selectedinventoryLine.ArtId, selectedinventoryLine.Amt);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Daten konnten nicht gespeichert werden:\n\n" + ex.Message), "Fehler", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
            }
        }

        public RelayCommand DeleteInventoryModelCommand { get; private set; }

        //private ICommand _getinventoryLineModelsCommand = null;

        //public ICommand GetinventoryLineModelsCommand => _getinventoryLineModelsCommand ?? (_getinventoryLineModelsCommand = new inventoryCommand(this));

        //private ICommand _getinventoryModelsCommand = null;

        //public RelayCommand GetinventoryModelsCommand { get; private set; }

        //private ICommand _createNewinventoryModelCommand = null;

        //public ICommand CreateNewinventoryModelCommand { get; private set; }

        //private ICommand _deleteinventoryModelCommand = null;

        //public ICommand DeleteinventoryModelCommand => _deleteinventoryModelCommand ?? (_deleteinventoryModelCommand = new DeleteinventoryModelCommand(this));
    }
}