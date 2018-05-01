using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace InformiInventory.Models
{
    public class InventoryDifferenceModel : ViewModelBase 
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

        int _inventoryId;
        public int InventoryId
        {
            get { return _inventoryId; }
            set { SetProperty(ref _inventoryId, value); }
        }

        int _inventoryLineId;
        public int InventoryLineId
        {
            get { return _inventoryLineId; }
            set { SetProperty(ref _inventoryLineId, value); }
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

        int _amt1;
        public int Amt1
        {
            get { return _amt1; }
            set { SetProperty(ref _amt1, value); }

        }

        int _amt2;
        public int Amt2
        {
            get { return _amt2; }
            set { SetProperty(ref _amt2, value); }
        }

        int _difference;
        public int Difference
        {
            get { return _difference; }
            set { SetProperty(ref _difference, value); }
        }

        string _storageName;
        public string StorageName
        {
            get { return _storageName; }
            set { SetProperty(ref _storageName, value); }
        }
    }
}
