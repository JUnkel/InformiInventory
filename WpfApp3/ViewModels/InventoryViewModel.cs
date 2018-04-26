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
            //var view = new CollectionViewSource();
            //view.GroupDescriptions.Add(new PropertyGroupDescription("StoreId"));
            GetInventories(this);
            //view.Source = InventoryModels;
            //InventoriesView = view;
            GetInventoryLinesCommand = new RelayCommand(GetInventoryLines, CanExecute_GetinventoryLinesCommand);
            CreateInventoryCommand = new RelayCommand(CreateInventory, CanExecute_CreateInventoryCommand);
            DeleteInventoryCommand = new RelayCommand(DeleteInventory, CanExecute_DeleteInventoryCommand);
            SaveInventoryLineCommand = new RelayCommand(SaveInventoryLine, CanExecute_SaveInventoryLineCommand);
            BookInventoryCommand = new RelayCommand(BookInventory, CanExecute_BookInventoryCommand);
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

                    InventoryModels.AddRange(db.Fetch<InventoryModel>("SELECT InventoryId AS Id, Dt AS DATE, IsProcd AS IsProcd, StoreId AS StoreId FROM Inventories WHERE StoreId = @0", storeId));

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

                    InventoryLineModels.AddRange(db.Fetch<InventoryLineModel>("SELECT a.GTIN AS GTIN, a.ADesc AS ArtDesc, s.StorageName AS StorageName, a.ArticleId AS ArtId, il.Amt AS Amt, il.InventoryId AS InventoryId, il.InventoryLineId AS InventoryLineId FROM Articles a LEFT JOIN Storages s ON a.StorageId = s.StorageId LEFT JOIN InventoryLines il ON a.ArticleId = il.ArtId LEFT JOIN Inventories i ON i.InventoryId = il.InventoryId  WHERE (il.inventoryId = @0 OR il.inventoryId IS NULL) ORDER BY a.GTIN", selectedinventory.Id));
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
            try
            {
                var vm = (InventoryViewModel)paramater;

                if (vm == null) return;

                int? storeId = null;
                var isStoreId = int.TryParse(App.Current.Properties["StoreId"].ToString(), out int resultStoreId);

                if (isStoreId)
                {
                    storeId = resultStoreId;
                }

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
                        var rowId = db.ExecuteScalar<int>("INSERT INTO Inventories(Dt, StoreId, UserId, IsProcd) VALUES(date('now'), @0, @1,@2);SELECT last_insert_rowid();", storeId, userId, 0);

                        var inventory = new InventoryModel()
                        {
                            Date = DateTime.Now,
                            Id = rowId,
                            IsProcd = false,
                            StoreId = storeId,
                            UserId = userId,
                        };

                        //InventoryModels.Add(inventory);
                        //InventoryLineModels.AddRange(db.Fetch<InventoryLineModel>("SELECT a.GTIN AS GTIN, a.ADesc AS ArtDesc, s.StorageName AS StorageName, a.ArticleId AS ArtId FROM Articles a LEFT JOIN Storages s ON a.StorageId = s.StorageId ORDER BY a.GTIN"));

                        InventoryModels.Add(inventory);

                        scope.Complete();
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
            var vm = (InventoryViewModel)parameter;

            if (vm == null) return false;

            else
            {
                return true;
            }
        }


        public RelayCommand DeleteInventoryCommand { get; private set; }

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

            if (MessageBox.Show("Soll die ausgewählte Inventurliste gelöscht werden?", "Frage", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.Cancel) return;

            try
            {
                using (var db = new PetaPoco.Database("db"))
                {

                    db.Execute(sql: "DELETE FROM Inventories WHERE inventoryId =@0;", selectedInventory.Id);

                    db.Execute(sql: "DELETE FROM InventoryLines WHERE InventoryId =@0;", selectedInventory.Id);

                    InventoryModels.Remove(selectedInventory);

                    InventoryLineModels.Clear();

                    MessageBox.Show("Ausgewählte Bestückungsliste wurde gelöscht.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Daten konnten nicht abgerufen werden:\n\n" + ex.Message), "Fehler");
            }
        }


        public RelayCommand SaveInventoryLineCommand { get; private set; }

        public bool CanExecute_SaveInventoryLineCommand(object parameter)
        {
            var vm = (InventoryViewModel)parameter;

            if (vm == null) return false;

            if (vm.SelectedInventoryModel == null) return false;

            if (vm.SelectedInventoryModel.IsProcd == true)
            {
                return false;
            }

            if (vm.SelectedInventoryLineModel == null) return false;

            else
            {
                return true;
            }
        }

        public void SaveInventoryLine(object parameter)
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

                    using (var scope = db.GetTransaction())
                    {

                        var vm = (InventoryViewModel)parameter;

                        if (vm == null) return;

                        if (vm.SelectedInventoryModel == null) return;

                        var iLM = vm.SelectedInventoryLineModel;


                        if (iLM == null) return;

                        if (iLM.Amt <= 0)
                        {
                            db.Execute("DELETE FROM InventoryLines WHERE InventoryLineId = @0", iLM.InventoryLineId);
                        }
                        else
                        {
                            var changes = db.ExecuteScalar<int>("Update InventoryLines SET Amt = @0 WHERE InventoryLineId = @1 AND InventoryId =@2 AND ArtId = @3  ; Select Changes()", iLM.Amt, iLM.InventoryLineId, SelectedInventoryModel.Id,iLM.ArtId);

                            if (changes == 0)
                            {
                                var lastid = db.ExecuteScalar<int?>("Select MAX(InventoryLineId) FROM InventoryLines");

                                iLM.InventoryLineId = db.ExecuteScalar<int>("Insert Into InventoryLines(InventoryLineId,InventoryId, ArtId, Amt, UserId) VALUES(@0, @1, @2, @3,@4);SELECT last_insert_rowid();", lastid == null? 1 : lastid + 1, SelectedInventoryModel.Id, iLM.ArtId, iLM.Amt, userId);
                            }
                        }
                        scope.Complete();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Daten konnten nicht gespeichert werden:\n\n" + ex.Message), "Fehler", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
            }
        }


        public RelayCommand BookInventoryCommand { get; private set; }

        public bool CanExecute_BookInventoryCommand(object parameter)
        {
            var vm = (InventoryViewModel)parameter;

            if (vm == null) return false;

            if (vm.SelectedInventoryModel == null) return false;

            if (vm.SelectedInventoryModel.IsProcd == true) return false;

            else
            {
                return true;
            }
        }

        public void BookInventory(object parameter)
        {
            var vm = (InventoryViewModel)parameter;

            if (vm == null) return;

            if (vm.SelectedInventoryModel == null) return;

            if (MessageBox.Show("Soll die ausgewählte Inventurliste gelöscht werden?", "Frage", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.Cancel) return;

            try
            {
                using (var db = new PetaPoco.Database("db"))
                {
                    db.Execute(sql: "Update Inventories SET IsProcd = 1 WHERE InventoryId =@0;", vm.SelectedInventoryModel.Id);

                    InventoryLineModels.Clear();

                    vm.SelectedInventoryModel.IsProcd = true;

                    MessageBox.Show("Ausgewählte Inventurliste wurde abgeschlossen.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Daten konnten nicht gespeichert werden:\n\n" + ex.Message), "Fehler");
            }
        }

    }
}