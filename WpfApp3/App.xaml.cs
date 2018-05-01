using InformiInventory.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Telerik.Windows.Controls;

namespace InformiInventory
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public App()
        {
            var culture = System.Globalization.CultureInfo.CurrentCulture.IetfLanguageTag;

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(culture);

            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(culture);

            // Set culture for all controls.
            FrameworkElement.LanguageProperty.OverrideMetadata(
                            typeof(FrameworkElement),
                            new FrameworkPropertyMetadata(System.Windows.Markup.XmlLanguage.GetLanguage(culture)));

            InitializeComponent();

        }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            StyleManager.ApplicationTheme = new Telerik.Windows.Controls.Windows8TouchTheme();

            //Application.Current.Properties["Settings"] = "SettingsPropertyValue";
            this.Properties["UserName"] = null;
            this.Properties["UserId"] = null;
            this.Properties["StoreId"] = null;
            this.Properties["StoreName"] = null;

            try
            {

                var upgrader = DbUp.DeployChanges.To.SQLiteDatabase("Data Source=db.sqlite; Version=3;").WithScriptsAndCodeEmbeddedInAssembly(System.Reflection.Assembly.GetExecutingAssembly()).Build();

                var result = upgrader.PerformUpgrade();

                if (!result.Successful)
                {
                    MessageBox.Show("Datenbank konnte nicht aktualisiert werden:\n\n" + result.Error, "informiPos", MessageBoxButton.OK, MessageBoxImage.Error);

                    Shutdown(-1);
                }
                else
                {
                    this.MainWindow = new MainWindow();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Datenbank konnte nicht aktualisiert werden:\n\n" + ex.Message, "informiPos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
