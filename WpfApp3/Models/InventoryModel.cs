using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace InformiInventory.Models
{
    public class InventoryModel : ViewModelBase
    {
        //string _storeName;
        //public string StoreName
        //{
        //    get { return _storeName; }
        //    set { SetProperty(ref _storeName, value); }
        //}

        protected virtual bool SetProperty<T>(ref T field, T value, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            this.OnPropertyChanged(propertyName);

            return true;
        }

        int? _storeId;
        public int? StoreId
        {
            get { return _storeId; }
            set { SetProperty(ref _storeId, value); }
        }

        DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set { SetProperty(ref _date, value); }
        }

        int _userId;
        public int UserId
        {
            get { return _userId; }
            set { SetProperty(ref _userId, value); }
        }

        int _id;
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        int? _templateId;
        public int? TemplateId
        {
            get { return _templateId; }
            set { SetProperty(ref _templateId, value); }
        }

        //bool _isTemplate;
        //public bool IsTemplate
        //{
        //    get { return _isTemplate; }
        //    set { SetProperty(ref _isTemplate, value); }
        //}

        bool _isProcd;

        public bool IsProcd
        {
            get { return _isProcd; }
            set { SetProperty(ref _isProcd, value); }
        }
    }
}
