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
        public int selectedRestock { get; set; }


        RadObservableCollection<RestockModel> _storeRestockModels;

        public RadObservableCollection<RestockModel> StoreRestockModels
        {
            get
            {
                if (_storeRestockModels == null) _restockModels = new RadObservableCollection<RestockModel>();

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

        public void GetRestockLineModels(RestockViewModel vm)
        {
            using (var db = new PetaPoco.Database("db"))
            {
                try
                {
                    RestockLineModels.AddRange(db.Fetch<RestockLineModel>("SELECT a.GTIN AS GTIN, rsl.Pos AS POS, a.ADesc AS ArtDesc FROM RestockLines rsl INNER JOIN Articles a ON rsl.ArtId = a.Id INNER JOIN Restocks r ON rsl.RestockId = r.Id WHERE r.Id = @0 INNER JOIN Storages s ON s.Id = r.StoreId",vm.selectedRestock));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Daten konnten nicht abgerufen werden:\n\n" + ex.Message), "Fehler");
                }
            }
        }

        private ICommand _getRestockModelsCommand = null;

        public ICommand GetRestockModelsCommand => _getRestockModelsCommand ?? (_getRestockModelsCommand = new GetRestockModelsCommand(this));

        public void GetRestockModels(RestockViewModel vm)
        {
            using (var db = new PetaPoco.Database("db"))
            {
                try
                {
                    RestockModels.AddRange(db.Fetch<RestockModel>("SELECT Id AS Id, Dt AS DATE FROM Restocks WHERE IsTemplate = 1"));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Daten konnten nicht abgerufen werden:\n\n" + ex.Message), "Fehler");
                }
            }
        }
    }
}
