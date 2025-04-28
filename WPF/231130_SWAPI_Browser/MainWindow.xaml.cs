using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _231130_SWAPI_Browser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<StarWarsPeople> starWarsPeoples = new ObservableCollection<StarWarsPeople>();
        private ObservableCollection<Starship> starWarsShips = new ObservableCollection<Starship>();

        public MainWindow()
        {
            InitializeComponent();

            //Icon = BitmapFrame.Create(new BitmapImage(new Uri("./Darth_Vader.png", UriKind.RelativeOrAbsolute)));
            Background = new ImageBrush(new BitmapImage(new Uri("./background_new.jpg", UriKind.RelativeOrAbsolute)));

            imageStarWars.Source = new BitmapImage(new Uri("./StarWars.png", UriKind.Relative));

            getStarWarsPeople();

        }

        private async void getStarWarsPeople()
        {
            dataGridPeopleList.ItemsSource = starWarsPeoples;
            Mouse.OverrideCursor = Cursors.Wait;
            string charakter = await getRequestAsync("/api/people");
            StarWarsPeopleList? list = JsonSerializer.Deserialize<StarWarsPeopleList>(charakter);
            if (list.Peoples is not null)
            {
                foreach (StarWarsPeople c in list.Peoples)
                {
                    starWarsPeoples.Add(c);
                }
                list.Peoples?.AddRange(list.Peoples);
                list.Next = list.Next;
            }
            while (list?.Next is not null)
            {
                charakter = await getRequestAsync(list.Next);
                StarWarsPeopleList? templist = JsonSerializer.Deserialize<StarWarsPeopleList>(charakter);
                if (list.Peoples is not null)
                {
                    foreach (StarWarsPeople c in templist.Peoples)
                    {
                        starWarsPeoples.Add(c);
                    }
                    list.Peoples?.AddRange(templist.Peoples);
                    list.Next = templist.Next;
                }
            }
            Mouse.OverrideCursor = null;
            labelinfo.Content = starWarsPeoples.Count + " Personen wurden vollständig geladen!";
        }

        private async Task<string> getRequestAsync(string url)
        {
            HttpClient client = new HttpClient();
            Uri baseUri = new Uri("https://swapi.dev");
            client.BaseAddress = baseUri;
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            //make the request
            var task = await client.SendAsync(requestMessage);
            HttpResponseMessage msg = task.EnsureSuccessStatusCode();
            string responseBody = await task.Content.ReadAsStringAsync();
            return responseBody;
        }

        private void dataGridPeopleList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dataGridStarshipList.Visibility = Visibility.Visible;

            DataGrid dataGrid = sender as DataGrid;
            StarWarsPeople people = (StarWarsPeople)dataGrid.SelectedItem;

            findShips(people.Starships);
        }

        private async void findShips(List<string> starships)
        {
            starWarsShips.Clear();
            dataGridStarshipList.ItemsSource = starWarsShips;
            if (starships.Count > 0)
            {
                Mouse.OverrideCursor = Cursors.Wait;
                foreach (string starship in starships)
                {
                    HttpClient client = new HttpClient();
                    HttpResponseMessage http = await client.GetAsync(starship);
                    Starship? ship = JsonSerializer.Deserialize<Starship>(await http.Content.ReadAsStringAsync());
                    starWarsShips.Add(ship);
                }
                Mouse.OverrideCursor = null;
            }
        }
    }
}