using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;

namespace _240131_Labordiagnostik
{
    class Mikrotiterplatte
    {
        List<Messwert> messwertList = new List<Messwert>();

        public Mikrotiterplatte()
        {

        }


        public async void LoadValues()
        {
            
            //Methode ohne Design Pattern Strategy
            /*MessageBoxResult mresult = MessageBoxResult.OK;

            while(mresult == MessageBoxResult.OK)
            {
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
                        List<Messwert> list = JsonSerializer.Deserialize<List<Messwert>>(json);
                        messwertList = list;
                        mresult = MessageBoxResult.Cancel;
                    }
                    catch (JsonException ex)
                    {
                        mresult = MessageBox.Show("Fehler beim Laden der Datei! \nBitte andere Datei wählen!", "OK", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                        if (mresult != MessageBoxResult.OK)
                        {
                            mresult = MessageBoxResult.Cancel;
                        }
                    }
                }
            }   */

            
            IImportStrategy jsonStrategy = new JsonImportStrategy();        // Wähle Einlesestrategie
            MesswertImporter importer = new MesswertImporter(jsonStrategy);     // Erzeuge den MesswertImporter mit der ausgewählten Strategie
            List<Messwert> messwerte = await importer.ImportMesswerte();      // Einlesen der Messwerte und Zuweisung zur Liste
            messwertList = messwerte;
        }

        public async void LoadValuesMQTT(string ip, string port, string wellinfo)
        {
            IImportStrategy mqttStrategy = new MQTTImportStrategy(ip, port, wellinfo);        // Wähle Einlesestrategie
            MesswertImporter importer = new MesswertImporter(mqttStrategy);     // Erzeuge den MesswertImporter mit der ausgewählten Strategie
            List<Messwert> messwerte = await importer.ImportMesswerte();      // Einlesen der Messwerte und Zuweisung zur Liste
            messwertList = messwerte;
        }


        public int[] SetColor(double? value, int proof)
        {
            double hue = 0.00;

            if (value == null)
            {
                int[] white = new int[3];
                white[0] = 255;
                white[1] = 255;
                white[2] = 255;
                return white;
            }

            double value2 = Convert.ToDouble(value);

            if (proof == 0)
            {
                double minOd = 0;
                double maxOd = 5;
                hue = ((value2 - minOd) / (maxOd - minOd)) * 270;       //Bereich von OD wird auf Bereich von 0 bis 270 umgerechnet
            }
            else if(proof == 1)
            {
                double minF = 800;
                double maxF = 65535;
                hue = ((value2 - minF) / (maxF - minF)) * 270;          //Bereich von RFU wird auf Bereich von 0 bis 270 umgerechnet
            }

            int[] rgb = ConvertHSLToRGB(hue, 100, 50);          //50, da HSL und nicht HSV

            return rgb;

        }

        public int[] ConvertHSLToRGB(double h, double s, double l)
        {
            //Umrechnung in RGB und Rückgabe als Array
            int[] result = new int[3];

            double hstrich = h / 60;
            double sstrich = s / 100;
            double lstrich = l / 100;

            double c = sstrich * (1 - Math.Abs(2 * lstrich - 1));
            double x = c * (1 - Math.Abs(hstrich % 2 - 1));
            double m = lstrich - c / 2;

            double r1;
            double g1;
            double b1;

            if (hstrich >= 2 && hstrich <= 4)
            {
                r1 = 0;
            }
            else if ((hstrich >= 0 && hstrich <= 1) || hstrich >= 5 && hstrich <= 6)
            {
                r1 = c;
            }
            else
            {
                r1 = x;
            }

            if (hstrich >= 4 && hstrich <= 6)
            {
                g1 = 0;
            }
            else if (hstrich >= 1 && hstrich <= 3)
            {
                g1 = c;
            }
            else
            {
                g1 = x;
            }

            if (hstrich >= 0 && hstrich <= 2)
            {
                b1 = 0;
            }
            else if (hstrich >= 3 && hstrich <= 5)
            {
                b1 = c;
            }
            else
            {
                b1 = x;
            }


            result[0] = Convert.ToInt32(Math.Round((r1 + m) * 255));
            result[1] = Convert.ToInt32(Math.Round((g1 + m) * 255));
            result[2] = Convert.ToInt32(Math.Round((b1 + m) * 255));
            

            return result;
        }


        public List<Messwert> MesswertList
        {
            get { return messwertList; }
            set { messwertList = value; }
        }


    }
}
