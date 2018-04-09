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
using Telerik.Windows.Data;

namespace InformiInventory.ViewModels
{
    public class RestockViewModel : ViewModelBase
    {
        RadObservableCollection<RestockLineModel> _restockLineModels;

        public RadObservableCollection<RestockLineModel> RestockLineModels
        {
            get
            {
                if (_restockLineModels == null) _restockLineModels = new RadObservableCollection<RestockLineModel>();

                return _restockLineModels;
            }
            set
            {
                SetProperty(ref _restockLineModels, value);
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
                    vm.RestockLineModels.AddRange(db.Fetch<RestockLineModel>("SELECT a.GTIN AS GTIN, rsl.Pos AS POS, a.ADesc AS ArtDesc FROM RestockLines rsl LEFT JOIN Articles a ON rsl.ArtId = a.Id LEFT JOIN Storages s ON s.Id = rsl."));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Daten konnten nicht abgerufen werden:\n\n" + ex.Message), "Fehler");
                }
            }
        }



        //string _store;

        //public string Store
        //{
        //    get { return _store; }
        //    set { SetProperty(ref _store, value); }
        //}

        //DateTime _dt = DateTime.Now.Date;

        //public DateTime Dt
        //{
        //    get { return _dt; }
        //    set { SetProperty(ref _dt, value); }
        //}

        //string _art;

        //public string Art
        //{
        //    get { return _art; }
        //    set { SetProperty(ref _art, value); }
        //}

        //string _an;

        //public string AN
        //{
        //    get { return _an; }
        //    set { SetProperty(ref _an, value); }
        //}

        //string _gtin;

        //public string GTIN
        //{
        //    get { return _gtin; }
        //    set { SetProperty(ref _gtin, value); }
        //}


        //decimal _orderAmnt;

        //public decimal _OrderAmnt
        //{
        //    get { return _orderAmnt; }
        //    set { SetProperty(ref _orderAmnt, value); }
        //}

        //int _userId;

        //public int UserId
        //{
        //    get { return _userId; }
        //    set { SetProperty(ref _userId, value); }
        //}
    }
}
