using DocumentFormat.OpenXml.Spreadsheet;
using ExcelDataReader;
using InformiInventory.Commands;
using InformiInventory.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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

            var csvlist = new List<string[]>();

            if (fileName.EndsWith(".csv"))
            {
                using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
                {
                    try
                    {

                        using (var reader = ExcelReaderFactory.CreateCsvReader(stream,null))
                        {
                            do
                            {

                                while (reader.Read())
                                {
                                    var rslm = new RestockLineModel();
                                    string[] stringArray = new string[reader.FieldCount];

                                    for (var i = 0; i >= reader.FieldCount; i++)
                                    {
                                        var value = reader.GetValue(i);
                                        stringArray[i] = value.ToString();
                                    }
                                    csvlist.Add(stringArray);

                                }
                            } while (reader.NextResult());


                        }
                    }
                    catch
                    {

                    }

                }
            }
            else
            {
                return;
            }
        }
    }
}
