using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformiInventory.Models
{
    //1;40069;Westfro Karottenwürfel 4x2,5 kg;;1;
    public class RestockModel : INotifyPropertyChanged
    {

        string _store;

        public string Store
        {
            get { return _store; }
            set { SetProperty(ref _store, value); }
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

        int _requestId;
        public int RequestId
        {
            get { return _requestId; }
            set { SetProperty(ref _requestId, value); }
        }
    }

    public class RestockLineModel : INotifyPropertyChanged
    {
        //CSV Format 
        //1;   40069;   Westfro Karottenwürfel 4x2, 5 kg    ;   ;1 ;
        //318; K115313; Häagen Dazs Pralines Cream 8x500 ml.;   ;0 ;E

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

    public class Article : INotifyPropertyChanged
    {
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
