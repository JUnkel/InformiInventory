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
using System.Windows.Input;

namespace InformiInventory.ViewModels
{
    public class InventoryViewModel : ViewModelBase
    {
        public InventoryViewModel()
        {
            GetInventories();
        }

        ObservableCollection<InformiInventory.Models.InventoryModel> _inventories;

        public ObservableCollection<InformiInventory.Models.InventoryModel> Inventories
        {
            get
            {
                if (_inventories == null) _inventories = new ObservableCollection<Models.InventoryModel>();

                return _inventories;
            }
            set { SetProperty(ref _inventories, value); }
        }

        string _store;

        public string Store
        {
            get { return _store; }
            set { SetProperty(ref _store, value); }
        }

        DateTime _dt = DateTime.Now.Date;

        public DateTime Dt
        {
            get { return _dt; }
            set { SetProperty(ref _dt, value); }
        }

        int _id;

        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        int _crUserId;

        public int CrUserId
        {
            get { return _crUserId; }
            set { SetProperty(ref _crUserId, value); }
        }

        int _inventoryUserId;

        public int InventoryUserId
        {
            get { return _inventoryUserId; }
            set { SetProperty(ref _inventoryUserId, value); }
        }

        #region Commands
        private ICommand _getInventoriesCommand = null;

        public ICommand GetInventoriesCommand => _getInventoriesCommand ?? (_getInventoriesCommand = new InventoryCommand(this));

        public void GetInventories()
        {
            using (var db = new PetaPoco.Database("db"))
            {
                db.Fetch<InventoryModel>("SELECT StoreId, CrDt, CrUserId, InventoryUserId FROM Inventories").ForEach(Inventories.Add);
            }
        }
        #endregion
    }
}
