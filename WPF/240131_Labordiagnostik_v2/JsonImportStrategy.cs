using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace _240131_Labordiagnostik
{
    internal class JsonImportStrategy : IImportStrategy
    {
        public JsonImportStrategy() { }

        public async Task<List<Messwert>> ImportData()
        {
            MessageBoxResult mresult = MessageBoxResult.OK;
            List<Messwert> list = new List<Messwert>();

            while (mresult == MessageBoxResult.OK)          //Wenn OK --> wird wieder aufgerufen um neue Datei zu wählen
            {
                //öffnet FileDialog mit einem JSON Filter               
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Browse for JSON file";
                ofd.DefaultExt = ".json";
                ofd.Filter = "Json files | *.json";
                ofd.Multiselect = false;

                string dir = Directory.GetCurrentDirectory();
                ofd.InitialDirectory = dir;
                bool? result = ofd.ShowDialog();

                string file = "";
                if (result != null && result.Value == true)
                {
                    file = ofd.FileName;
                    string json = File.ReadAllText(file);

                    try
                    {
                        list = JsonSerializer.Deserialize<List<Messwert>>(json);            //wird in die Liste deserialisiert
                        mresult = MessageBoxResult.Cancel;
                    }
                    catch (JsonException ex)
                    {
                        mresult = MessageBox.Show("Fehler beim Laden der Datei! \nBitte andere Datei wählen!", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                        if (mresult != MessageBoxResult.OK)
                        {
                            mresult = MessageBoxResult.Cancel;
                        }
                    }
                }
            }

            return list;
        }
    }
}
