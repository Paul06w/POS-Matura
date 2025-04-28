using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Labordiagnostik
{
    internal class JSONReadingStrategy : IReadingStrategy
    {
        public async void Read(ObservableList<Well> wells)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.txt)|*.json";

            string fileName = "";
            if (openFileDialog.ShowDialog() == true)
            {
                fileName = openFileDialog.FileName;
            }
            else
            {
                wells.Clear();
                return;
            }

            JsonSerializerOptions options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;

            wells.AddRange(JsonSerializer.Deserialize<ObservableList<Well>>(File.ReadAllText(fileName), options));
        }
    }
}
