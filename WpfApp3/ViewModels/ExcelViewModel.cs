using DocumentFormat.OpenXml.Spreadsheet;
using InformiInventory.Commands;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx;
using Telerik.Windows.Documents.Spreadsheet.Model;

namespace InformiInventory.ViewModels
{
    public class ExcelViewModel : ViewModelBase
    {
        private ICommand _importExcelCommand = null;

        public ICommand ImportExcelCommand => _importExcelCommand ?? (_importExcelCommand = new ExcelCommand(this));

        public void ImportExcel()
        {
            var fileDialog = new System.Windows.Forms.OpenFileDialog();

            var result = fileDialog.ShowDialog();

            var fileName = "";

            switch (result)
            {
                case System.Windows.Forms.DialogResult.OK:
                    fileName = fileDialog.FileName;
                    //TxtFile.Text = file;
                    //TxtFile.ToolTip = file;
                    break;
                case System.Windows.Forms.DialogResult.Cancel:
                default:
                    //TxtFile.Text = null;
                    //TxtFile.ToolTip = null;
                    break;
            }


            var provider = new (); 

            Workbook workbook;

            try
            {
                using (var stream = File.OpenRead(fileName))
                {
                    workbook = provider.Import(stream);

                    stream.Close();
                }
            }
            catch (Exception e)
            {
                //("Fehler beim einlesen der Datei" + e.Message);
                return;
            }

            using (var db = new PetaPoco.Database("db"))
            {
            }
        }
    }
}
