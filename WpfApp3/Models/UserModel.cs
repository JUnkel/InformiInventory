using InformiInventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace informiInventory
{
    public class User : INotifyPropertyChanged
    {
        int _userId;
        public int UserId
        {
            get { return _userId; }
            set { SetProperty(ref _userId, value); }
        }

        int _storeId;
        public int StoreId
        {
            get { return _storeId; }
            set { SetProperty(ref _storeId, value); }
        }

        string _userName;
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

    }
}
