using InformiInventory.ViewModels;
using System;
using System.Collections.Generic;
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
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;

namespace InformiInventory.Views
{
    public partial class AssemblyListView : UserControl
    {
        public AssemblyListView()
        {
            InitializeComponent();

            DataContext = new AssemblyListViewModel();

            MainWindow.Instance.NavigationPanel.Visibility = Visibility.Visible;
        }
    }
}
