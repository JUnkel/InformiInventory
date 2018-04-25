using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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

        ObservableCollection<InformiInventory.Models.DifferenceListModel> _differences;

        ObservableCollection<InformiInventory.Models.DifferenceListModel> Differences
        {
            get
            {
                if (_differences == null) _differences = new ObservableCollection<Models.DifferenceListModel>();

                return _differences;
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


        decimal _difference;

        public decimal Difference
        {
            get { return _difference; }
            set { SetProperty(ref _difference, value); }
        }

        int _userId;

        public int UserId
        {
            get { return _userId; }
            set { SetProperty(ref _userId, value); }
        }

    }
}
