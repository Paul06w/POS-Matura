using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.WebRequestMethods;

namespace _231120_Ritterturnier_6._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> imagePaths;
        private int currentImageIndex = 0;
        Teilnehmerliste teilnehmerlist = new Teilnehmerliste();

        public MainWindow()
        {
            BitmapImage icon = new BitmapImage();
            icon.BeginInit();
            icon.UriSource = new Uri("./Knight.png", UriKind.RelativeOrAbsolute);
            icon.EndInit();
            // Setze das erstellte Bild als App-Icon
            Icon = BitmapFrame.Create(icon);


            InitializeComponent();

            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri("./Neuschwanstein2.png", UriKind.RelativeOrAbsolute));
            myBrush.Opacity = 0.5;
            this.Background = myBrush;

            LoadImagePaths();       // Annahme: Laden Sie Ihre Bildpfade
            ShowCurrentImage();

            //imageweapon.Source = new BitmapImage(new Uri("/Keule.png", UriKind.RelativeOrAbsolute));

        }

        private void LoadImagePaths()
        {
            // Annahme: Laden Sie Ihre Bildpfade aus der Datenquelle
            imagePaths = new List<string>
            {

                "/Schwert.png",
                "/Lanze.png",
                "/Keule.png",

            };
        }

        private void ShowCurrentImage()
        {
            if (imagePaths.Count > 0 && currentImageIndex >= 0 && currentImageIndex < imagePaths.Count)
            {
                string imagePath = imagePaths[currentImageIndex];
                imageweapon.Source = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
            }
            else
            {
                // Zeigen Sie eine leere Bildanzeige oder eine Meldung an, wenn keine Bilder vorhanden sind.
                //imageweapon.Source = null;
                imageweapon.Source = new BitmapImage(new Uri("/Schwert.png", UriKind.RelativeOrAbsolute));
            }
        }

        private void PreviousImage_Click(object sender, RoutedEventArgs e)
        {
            if (currentImageIndex > 0)
            {
                currentImageIndex--;
                ShowCurrentImage();
            }
        }

        private void NextImage_Click(object sender, RoutedEventArgs e)
        {
            if (currentImageIndex < imagePaths.Count - 1)
            {
                currentImageIndex++;
                ShowCurrentImage();
            }
        }

        private void buttonaddClick(object sender, RoutedEventArgs e)
        {
            string knightName = textboxname.Text;
            string knightrufname = textboxtitle.Text;
            string knighttelnum = textboxphone.Text;
            int knightweapon = currentImageIndex;
            string knightprefix = textboxprefix.Text;

            if (checkboxHasScarce.IsChecked == true)
            {
                string scarceName = textboxscarcename.Text;
                string scarcetelnum = textboxscarcephone.Text;
                double scarcegrade = sliderscarcegrade.Value;

                teilnehmerlist.addTeilnehmer(knightName, knightrufname, knighttelnum, knightweapon, knightprefix, scarceName, scarcetelnum, scarcegrade);
            }
            else
            {
                teilnehmerlist.addTeilnehmer(knightName, knightrufname, knighttelnum, knightweapon, knightprefix, null, null, 0);
            }

            listBoxTurnier.Items.Clear();
            listBoxTurnier.Items.Add(teilnehmerlist.listeAlleTeilnehmer());
        }



        private void buttonSave(object sender, RoutedEventArgs e)
        {
            List<Person> aktList = new List<Person>();
            aktList = teilnehmerlist.getTurnierList();

            MessageBoxResult result = MessageBox.Show("Wollen Sie wirklich speichern?", "Save", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {

                /*JsonSerializerOptions options = new JsonSerializerOptions();
                options.WriteIndented = true;
                string jsontext = JsonSerializer.Serialize(aktList, options);
                MessageBox.Show(jsontext, "OK", MessageBoxButton.YesNo, MessageBoxImage.Question);
                System.IO.File.WriteAllText("knights.json", jsontext);
                */

                string jason = JsonConvert.SerializeObject(aktList);
                System.IO.File.WriteAllText("knights.json", jason);
            }
        }

        private async void buttonLoad(object sender, RoutedEventArgs e)
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
                string json = await getJsonAsync(file);
                
                
                string json = System.IO.File.ReadAllText(file);
                //List<Person> list = JsonSerializer.Deserialize<List<Person>>(json);
                List<Person> list = JsonConvert.DeserializeObject<List<Person>>(json);
                if (list != null)
                {
                    listBoxTurnier.Items.Clear();
                    foreach (Person p in list)
                    {
                        listBoxTurnier.Items.Add(p);
                    }
                }
            }
        }


        private async Task<List<>> getJsonAsync(string file)
        {

        }

        private void buttonShow(object sender, RoutedEventArgs e)
        {
            listBoxTurnier.Items.Clear();
            listBoxTurnier.Items.Add(teilnehmerlist.listeAlleTeilnehmer());
        }

        private void buttonShowS(object sender, RoutedEventArgs e)
        {
            listBoxTurnier.Items.Clear();
            listBoxTurnier.Items.Add(teilnehmerlist.alleMitWaffenart("Schwert"));
        }

        private void buttonShowL(object sender, RoutedEventArgs e)
        {
            listBoxTurnier.Items.Clear();
            listBoxTurnier.Items.Add(teilnehmerlist.alleMitWaffenart("Lanze"));
        }

        private void buttonShowK(object sender, RoutedEventArgs e)
        {
            listBoxTurnier.Items.Clear();
            listBoxTurnier.Items.Add(teilnehmerlist.alleMitWaffenart("Keule"));
        }
    }
}