using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformiInventory.ViewModels
{
    public class AssemblyListViewModel :ViewModelBase
    {
        ObservableCollection<InformiInventory.Models.AssemblyListModel> _assemblies;

        ObservableCollection<InformiInventory.Models.AssemblyListModel> Assemblies
        {
            get
            {
                if (_assemblies == null) _assemblies = new ObservableCollection<Models.AssemblyListModel>();

                return _assemblies;
            }
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

        string _art;

        public string Art
        {
            get { return _art; }
            set { SetProperty(ref _art, value); }
        }

        string _an;

        public string AN
        {
            get { return _an; }
            set { SetProperty(ref _an, value); }
        }

        string _gtin;

        public string GTIN
        {
            get { return _gtin; }
            set { SetProperty(ref _gtin, value); }
        }


        decimal _orderAmnt;

        public decimal _OrderAmnt
        {
            get { return _orderAmnt; }
            set { SetProperty(ref _orderAmnt, value); }
        }

        int _userId;

        public int UserId
        {
            get { return _userId; }
            set { SetProperty(ref _userId, value); }
        }
    }
}
