using InformiInventory.Models;
using InformiInventory.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Data;

namespace InformiInventory.ViewModels
{
    public class InventoryDifferenceViewModel : ViewModelBase
    {
        public InventoryDifferenceViewModel()
        {
            GetInventoryDifferencesCommand = new RelayCommand(GetInventoryDifferences, CanExecute_GetInventoryDifferencesCommand);
            GetInventoryDifferences(this);
        }

        protected virtual bool SetProperty<T>(ref T field, T value, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }

        RadObservableCollection<InventoryDifferenceModel> _inventoryDifferences;

        public RadObservableCollection<InventoryDifferenceModel> InventoryDifferences
        {
            get
            {
                if (_inventoryDifferences == null) _inventoryDifferences = new RadObservableCollection<InventoryDifferenceModel>();

                return _inventoryDifferences;
            }
        }

        public RelayCommand GetInventoryDifferencesCommand { get; private set; }

        public bool CanExecute_GetInventoryDifferencesCommand(object parameter)
        {
            var vm = (InventoryDifferenceViewModel)parameter;

            if (vm == null) return false;
            else
            {
                return true;
            }
        }

        public void GetInventoryDifferences(object parameter)
        {
            var vm = (InventoryDifferenceViewModel)parameter;

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
                try
                {
                    var secoundLastInventories = db.Fetch<InventoryModel>("SELECT Dt AS Date, InventoryId AS Id FROM Inventories WHERE StoreId = @0 AND IsProcd = 1 ORDER BY Dt DESC LIMIT 2 ", storeId).ToArray();

                    InventoryDifferences.Clear();

                    InventoryDifferences.AddRange(db.Fetch<InventoryDifferenceModel>("SELECT a.ADesc AS ArtDesc, a.GTIN AS GTIN, il.Amt AS Amt1, il2.Amt AS Amt2, (il2.Amt - il.Amt) AS Difference,s.StorageName AS StorageName FROM InventoryLines il INNER JOIN InventoryLines il2 ON il.ArtId = il2.ArtId INNER JOIN Articles a ON a.ArticleId = il.ArtId INNER JOIN Storages s ON s.StorageId = a.StorageId WHERE il.InventoryId = @0 AND il2.InventoryId = @1 ORDER BY a.GTIN", secoundLastInventories[0].Id, secoundLastInventories[1].Id ));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Daten konnten nicht abgerufen werden:\n\n" + ex.Message), "Fehler");
                }
            }
        }
    }
}
