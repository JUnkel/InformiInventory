using informiInventory;
using InformiInventory.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InformiInventory.Views
{
    public partial class MainWindow : Window
    {
        public static MainWindow Instance { get; private set; }

        //private object _selectedViewModel;

        //public object SelectedViewModel
        //{
        //    get { return _selectedViewModel; }

        //    set { _selectedViewModel = value; }
        //}

        public MainWindow() 
        {
            InitializeComponent();

            Instance = this;

            Instance.MainWindowContentControl.Content = new LoginView();

            var uri = new Uri("pack://application:,,,/Images/app-background.png");

            var image = new BitmapImage(uri);

            Instance.Background = new ImageBrush(image);
        }
    }
}