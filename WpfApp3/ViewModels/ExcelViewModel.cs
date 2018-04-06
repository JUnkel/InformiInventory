using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InformiInventory.ViewModels
{
    public class ExcelViewModel : ViewModelBase
    {
        private ICommand _importExcelCommand = null;

        public ICommand ImportExcelCommand => _importExcelCommand ?? (_importExcelCommand = new _importExcelCommand(this));

        public void ImportExcel()
        {
            var fileDialog = new System.Windows.Forms.OpenFileDialog();

            var result = fileDialog.ShowDialog();

            switch (result)
            {
                case System.Windows.Forms.DialogResult.OK:
                    var file = fileDialog.FileName;
                    //TxtFile.Text = file;
                    //TxtFile.ToolTip = file;
                    break;
                case System.Windows.Forms.DialogResult.Cancel:
                default:
                    //TxtFile.Text = null;
                    //TxtFile.ToolTip = null;
                    break;
            }






            using (var db = new PetaPoco.Database("db"))
            {
            }
        }
    }
}
