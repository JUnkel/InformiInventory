//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace InformiInventory
//{ 
//    //public class INotifyPropertyChanged
//    //{
//    //    //public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

//    //    //public override bool Equals(object obj)
//    //    //{
//    //    //    return base.Equals(obj);
//    //    //}

//    //    //public override int GetHashCode()
//    //    //{
//    //    //    return base.GetHashCode();
//    //    //}

//    //    public event PropertyChangedEventHandler PropertyChanged;

//    //    public override bool Equals(object obj)
//    //    {
//    //        return base.Equals(obj);
//    //    }

//    //    public override int GetHashCode()
//    //    {
//    //        return base.GetHashCode();
//    //    }

//    //    public override string ToString()
//    //    {
//    //        return base.ToString();
//    //    }

//    //    protected virtual void OnPropertyChanged(string propertyName)
//    //    {
//    //        PropertyChangedEventHandler handler = PropertyChanged;
//    //        if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
//    //    }
//    //    protected bool SetProperty<T>(ref T field, T value, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
//    //    {
//    //        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
//    //        field = value;
//    //        OnPropertyChanged(propertyName);
//    //        return true;
//    //    }


//        //public bool SetProperty<T>(ref T field, T value, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
//        //{
//        //    if (EqualityComparer<T>.Default.Equals(field, value)) return false;
//        //    field = value;
//        //    PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
//        //    return true;
//        //}

//        //protected bool Set<t>(ref T field, T value, [CallerMemberName]string propertyName = "")
//        //{
//        //    if (field == null || EqualityComparer<t>.Default.Equals(field, value)) { return false; }
//        //    field = value;
//        //    RaisePropertyChanged(propertyName);
//        //    return true;
//        //}</t></t>



//        //public override string ToString()
//        //{
//        //    return base.ToString();
//        //}
//    }
//}
