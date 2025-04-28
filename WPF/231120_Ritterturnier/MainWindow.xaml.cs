using Microsoft.Win32;
using System.IO;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.WebRequestMethods;

namespace _231120_Ritterturnier
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

            LoadImagePaths();       // Laden der Bildpfade für die ImageShow in eine Liste
            ShowCurrentImage();

            //imageweapon.Source = new BitmapImage(new Uri("/Keule.png", UriKind.RelativeOrAbsolute));

        }

        private void LoadImagePaths()
        {
            
            imagePaths = new List<string>
            {

                "/Schwert.png",
                "/Lanze.png",
                "/Keule.png",               
                
            };
        }

        //Aktuelles Bild wird ausgegeben --> wird über currentImageIndex geregelt
        private void ShowCurrentImage()
        {
            if (imagePaths.Count > 0 && currentImageIndex >= 0 && currentImageIndex < imagePaths.Count)
            {
                string imagePath = imagePaths[currentImageIndex];
                imageweapon.Source = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
            }
            else
            {
                imageweapon.Source = null;
                //imageweapon.Source = new BitmapImage(new Uri("/Schwert.png", UriKind.RelativeOrAbsolute));
            }
        }

        //Ein Bild zurückgehen
        private void PreviousImage_Click(object sender, RoutedEventArgs e)
        {
            if (currentImageIndex > 0)
            {
                currentImageIndex--;
                ShowCurrentImage();
            }
        }

        //Ein Bild nach vorne gehen
        private void NextImage_Click(object sender, RoutedEventArgs e)
        {
            if (currentImageIndex < imagePaths.Count - 1)
            {
                currentImageIndex++;
                ShowCurrentImage();
            }
        }

        //Button zum hinzufügen eines Ritters
        private void ButtonAddClick(object sender, RoutedEventArgs e)
        {
            string knightName = textboxname.Text;
            string knightrufname = textboxtitle.Text;
            string knighttelnum = textboxphone.Text;
            int knightweapon = currentImageIndex;
            string knightprefix = textboxprefix.Text;

            if(checkboxHasScarce.IsChecked == true)
            {
                string scarceName = textboxscarcename.Text;
                string scarcetelnum = textboxscarcephone.Text;
                double scarcegrade = sliderscarcegrade.Value;

                teilnehmerlist.AddTeilnehmer(knightName, knightrufname, knighttelnum, knightweapon, knightprefix, scarceName, scarcetelnum, scarcegrade);
            }
            else
            {
                teilnehmerlist.AddTeilnehmer(knightName, knightrufname, knighttelnum, knightweapon, knightprefix, null, null, 0);
            }

            listBoxTurnier.Items.Clear();           //Clearen damit es nicht zweimal drinnen ist
            listBoxTurnier.Items.Add(teilnehmerlist.ListeAlleTeilnehmer());     //Ausgabe
        }

        
        //Speichern der Liste
        private void ButtonSave(object sender, RoutedEventArgs e)
        {
            List<Ritter> aktList = new List<Ritter>();
            //Ritter r = new Ritter("knightName", "knightrufname", "telnumKnight", 1, "knightprefix", "scarceName", "telnumScarce", 69);
            aktList = teilnehmerlist.TurnierList;
            string filename = "";
            
            MessageBoxResult result = MessageBox.Show("Wollen Sie wirklich speichern?", "Save", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {           

                JsonSerializerOptions options = new JsonSerializerOptions();        
                options.WriteIndented = true;           //Option zur schöneren Ausgabe
                string jsontext = JsonSerializer.Serialize(aktList, options);       //Serialisieren der Liste mit den ausgewählten Optionen
                System.IO.File.WriteAllText("knights.json", jsontext);     //Schreiben in die Datei
                

                //string jason = JsonConvert.SerializeObject(aktList);
                //System.IO.File.WriteAllText("knights.json", jason);
            }
        }

        //Laden einer Datei mit FileDialog zum Auswählen
        private async void ButtonLoad(object sender, RoutedEventArgs e)
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
                List<Ritter> list = await GetJsonAsync(file);       //Async Methode

                
                teilnehmerlist.TurnierList = list;
                //List<Person> list = JsonConvert.DeserializeObject<List<Person>>(json);
                if (list != null)
                {
                    listBoxTurnier.Items.Clear();
                    /*foreach (Person p in list)
                    {
                        listBoxTurnier.Items.Add(p);
                    }*/
                    listBoxTurnier.Items.Add(teilnehmerlist.ListeAlleTeilnehmer());
                }
            }
        }

        //Async Methode, die das Json File ließt und deserialisiert
        private async Task<List<Ritter>> GetJsonAsync(string file)
        {
            //string json = System.IO.File.ReadAllText(file);
            string json = await System.IO.File.ReadAllTextAsync(file);
            List<Ritter> list = JsonSerializer.Deserialize<List<Ritter>>(json);

            return list;
        }

        //4 Buttons für die Ausgabe und filtern nach Waffenart
        private void ButtonShow(object sender, RoutedEventArgs e)
        {
            listBoxTurnier.Items.Clear();
            listBoxTurnier.Items.Add(teilnehmerlist.ListeAlleTeilnehmer());
        }

        private void ButtonShowS(object sender, RoutedEventArgs e)
        {
            listBoxTurnier.Items.Clear();
            listBoxTurnier.Items.Add(teilnehmerlist.AlleMitWaffenart("Schwert"));
        }

        private void ButtonShowL(object sender, RoutedEventArgs e)
        {
            listBoxTurnier.Items.Clear();
            listBoxTurnier.Items.Add(teilnehmerlist.AlleMitWaffenart("Lanze"));
        }

        private void ButtonShowK(object sender, RoutedEventArgs e)
        {
            listBoxTurnier.Items.Clear();
            listBoxTurnier.Items.Add(teilnehmerlist.AlleMitWaffenart("Keule"));
        }
    }
}