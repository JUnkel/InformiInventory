using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InformiInventory.Models
{
    public class RestockModel : ViewModelBase
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

    public class RestockLineModel : ViewModelBase
    {
        protected virtual bool SetProperty<T>(ref T field, T value, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }

        //CSV Format 

        //1;   40069;   Westfro Karottenwürfel 4x2, 5 kg    ;   ;1 ;
        //318; K115313; Häagen Dazs Pralines Cream 8x500 ml.;   ;0 ;E


        int _restockId;
        public int RestockId
        {
            get { return _restockId; }
            set { SetProperty(ref _restockId, value); }
        }

        int _restockLineId;
        public int RestockLineId
        {
            get { return _restockLineId; }
            set { SetProperty(ref _restockLineId, value); }
        }


        int _pos;
        public int Pos
        {
            get { return _pos; }
            set { SetProperty(ref _pos, value); }
        }

        string _gtin;
        public string GTIN
        {
            get { return _gtin; }
            set { SetProperty(ref _gtin, value); }
        }

        string _artDesc;
        public string ArtDesc
        {
            get { return _artDesc; }
            set { SetProperty(ref _artDesc, value); }
        }

        int _artId;
        public int ArtId
        {
            get { return _artId; }
            set { SetProperty(ref _artId, value); }
        }


        int _amt;
        public int Amt
        {
            get { return _amt; }
            set { SetProperty(ref _amt, value); }

        }

        string _storageName;


        public string StorageName
        {
            get { return _storageName; }
            set { SetProperty(ref _storageName, value); }
        }
    }

    public class Article : ViewModelBase
    {
        protected virtual bool SetProperty<T>(ref T field, T value, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }
 
        string _gtin;
        public string GTIN
        {
            get { return _gtin; }
            set { SetProperty(ref _gtin, value); }
        }

        string _desc;
        public string Desc
        {
            get { return _desc; }
            set { SetProperty(ref _desc, value); }
        }

        int _amt;
        public int Amt
        {
            get { return _amt; }
            set { SetProperty(ref _amt, value); }
        }

        string _storage;


        public string Storage
        {
            get { return _storage; }
            set { SetProperty(ref _storage, value); }
        }
    }

}
