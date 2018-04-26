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
    public class DifferenceListViewModel : ViewModelBase
    {
        protected virtual bool SetProperty<T>(ref T field, T value, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }

        

        RadObservableCollection<DifferenceModel> _differences;

        public RadObservableCollection<DifferenceModel> Differences
        {
            get
            {
                if (_differences == null) _differences = new RadObservableCollection<DifferenceModel>();

                return _differences;
            }
        }

        public RelayCommand GetDifferenceInventoryLinesCommand { get; private set; }

        public bool CanExecute_GetDifferenceInventoryLinesCommand(object parameter)
        {
            var vm = (DifferenceListViewModel)parameter;

            if (vm == null) return false;

            else
            {
                return true;
            }
        }

        public void GetGetDifferenceInventoryLines(object parameter)
        {
            var selectedinventory = (InventoryModel)parameter;

            if (selectedinventory == null) return;

            using (var db = new PetaPoco.Database("db"))
            {
                try
                {
                    Differences.Clear();

                    Differences.AddRange(db.Fetch<DifferenceModel>("SELECT a.GTIN AS GTIN, a.ADesc AS ArtDesc, s.StorageName AS StorageName, a.ArticleId AS ArtId, il.Amt AS Amt, il.InventoryId AS InventoryId, il.InventoryLineId AS InventoryLineId FROM Articles a LEFT JOIN Storages s ON a.StorageId = s.StorageId LEFT JOIN InventoryLines il ON a.ArticleId = il.ArtId LEFT JOIN Inventories i ON i.InventoryId = il.InventoryId  WHERE (il.inventoryId = @0 OR il.inventoryId IS NULL) AND il.Amt <> 0 ORDER BY i.Dt "));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Daten konnten nicht abgerufen werden:\n\n" + ex.Message), "Fehler");
                }
            }
        }

    }
}
