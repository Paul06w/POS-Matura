using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _230922_Uebung_WPF_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ObservableCollection<StarWarsPeople> StarWarsPeoples {  get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text))
            {
                lstNames.Items.Add(txtName.Text);
                txtName.Clear();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Wollen Sie wirklich speichern?", "Save", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                string jason = JsonConvert.SerializeObject(lstNames.Items);
                File.WriteAllText("namelist.json", jason);
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
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
            if(result != null && result.Value == true)
            {
                file = ofd.FileName;
                string jason = File.ReadAllText(file);
                List<String> list = JsonConvert.DeserializeObject<List<String>>(jason);
                if(list != null)
                {
                    lstNames.Items.Clear();
                    foreach( String item in list )
                    {
                        lstNames.Items.Add(item);
                    }
                }
            }
        }

        private void btnApi_Click(object sender, RoutedEventArgs e)
        {
            //string people = getRequest("/api/people/1");

            //StarWarsPeople? starWarsPeople = System.Text.Json.JsonSerializer.Deserialize<StarWarsPeople>(people);
            //lstNames.Items.Add(starWarsPeople.Name);


            /*string people = getRequest("/api/people");
            List<StarWarsPeople> list = System.Text.Json.JsonSerializer.Deserialize<List<StarWarsPeople>>(people);
            foreach (StarWarsPeople item in list)
            {
                lstNames.Items.Add(item.Name);
            }
            */

            string peoples = "/api/people";


            while (!string.IsNullOrEmpty(peoples))
            {
                string jsonResponse = getRequest(peoples);
                var swapiResponse = JsonConvert.DeserializeObject<StarWarsPeople>(jsonResponse);



                if (swapiResponse != null)
                {
                    foreach (var person in swapiResponse.Results)
                    {
                        lstNames.Items.Add(person.Name);
                    }



                    peoples = swapiResponse.getNext(); // Nächste Seite abrufen
                }
            }
        }

        private string getRequest(string url)
        {
            HttpClient client = new HttpClient();
            Uri baseUri = new Uri("https://swapi.dev");
            client.BaseAddress = baseUri;
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            //make the request
            var task = client.SendAsync(requestMessage);
            var response = task.Result;
            HttpResponseMessage msg = response.EnsureSuccessStatusCode();
            string responseBody = response.Content.ReadAsStringAsync().Result;
            //MessageBox.Show(responseBody);
            return responseBody;
        }

        private void btnWebsite_Click(object sender, RoutedEventArgs e)
        {
            /*
            string website = "";
            if (!string.IsNullOrEmpty(txtWebsite.Text))
            {
                website = txtWebsite.Text;
                txtWebsite.Clear();


                HttpClient httpClient = new HttpClient();
                //Task<string> task = httpClient.GetStringAsync("https://en.wikipedia.org/wiki/List_of_programmers");
                //Task<string> task = httpClient.GetStringAsync("https://www.orf.at");
                Task<string> task = httpClient.GetStringAsync("https://" + website);
                string data = task.Result;

                string expr = @"http[s]?://(?:[a-zA-Z]|[0-9]|[$-_@.&+]|[!*\\(\\),]|(?:%[0-9a-fA-F][0-9a-fA-F]))+";  // TODO: Find all HTML - links
                Regex regEx = new Regex(expr);
                MatchCollection matchedLinks = regEx.Matches(data);
                foreach (Match match in matchedLinks)
                {
                    string s = match.Value;
                    lstNames.Items.Add(s);
                }
            }
            */




            lstNames.Foreground = new SolidColorBrush(Colors.DarkBlue);
            lstNames.Background = new SolidColorBrush(Colors.LightGreen);

            string sUrl = txtWebsite.Text;
            if (String.IsNullOrEmpty(sUrl))
            {
                MessageBox.Show("Url must not be empty", "Invalid Urld", MessageBoxButton.OK);
                return;
            }
            if(!sUrl.StartsWith("http"))
            {
                sUrl = "https://" + sUrl;
            }
            Uri url = new Uri(sUrl);
            if(!Uri.IsWellFormedUriString(sUrl, UriKind.Absolute))
            {
                MessageBox.Show("Url not well-formed", "Invalid Url", MessageBoxButton.OK);
                return;
            }
            HttpClient httpClient = new HttpClient();
            Task<string> task = httpClient.GetStringAsync(url);
            string data = "";
            try
            {
                data = task.Result;
            }
            catch(Exception ex)
            {
                string sText = ex.Message;
                if(ex.InnerException is not null)
                {
                    sText += "\n";
                    sText += ex.InnerException.Message;
                }
                MessageBox.Show("Exception: " + sText, "Invalid Url", MessageBoxButton.OK);
                return;
            }

            string expr = @"<a [^\<\>]*href\=\""([^\#][^\""]*?)\""";
            Regex regex = new Regex(expr);
            MatchCollection matchedLinks = regex.Matches(data);
            foreach(Match match in matchedLinks)
            {
                string s = match.Groups[1].Value;
                if(s.StartsWith("http"))
                {
                    lstNames.Items.Add(s);
                }
            }
        }

        private void radioButtonName_Checked(object sender, RoutedEventArgs e)
        {
            txtName.IsEnabled = true;
            btnAdd.IsEnabled = true;
        }

        private void radioButtonWebsite_Checked(object sender, RoutedEventArgs e)
        {
            txtName.IsEnabled = false;
            btnAdd.IsEnabled = false;
        }

        private void txtWebsite_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnWebsite_Click(sender, e);
            }
        }

        private void lstNames_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string txt = "---";
            ListBox listBox = sender as ListBox;
            if(listBox is not null)
            {
                txt = listBox.SelectedItem as string;
            }
            //MessageBox.Show(txt);

            Uri uri;
            if(Uri.TryCreate(txt, UriKind.Absolute, out uri))
            {
                try
                {
                    ProcessStartInfo processStartInfo = new ProcessStartInfo();
                    processStartInfo.FileName = uri.AbsoluteUri;
                    processStartInfo.UseShellExecute = true;
                    Process.Start(processStartInfo);
                }
                catch(System.ComponentModel.Win32Exception noBrowser)
                {
                    if(noBrowser.ErrorCode == -2147467259)
                    {
                        MessageBox.Show(noBrowser.Message);
                    }
                }
                catch(System.Exception other)
                {
                    MessageBox.Show(other.Message);
                }
            }
        }

        private async void btnApi2_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            StarWarsPeoples = new();

            string people = await getRequestAsynch("/api/people");
            StarWarsPeopleList? peopleList = System.Text.Json.JsonSerializer.Deserialize<StarWarsPeopleList>(people);
            while(peopleList?.Next is not null)
            {
                //Erwarten deer asynchronen Ergebnisse, nicht blockieren
                people = await getRequestAsynch(peopleList.Next);
                StarWarsPeopleList? tempList = System.Text.Json.JsonSerializer.Deserialize<StarWarsPeopleList>(people);
                //Füge die neue Liste zur alten hinzu
                if(peopleList.Peoples is not null && tempList?.Peoples is not null)
                {
                    foreach(StarWarsPeople p in tempList.Peoples)
                    {
                        lstNames.Items.Add(p.Name);
                    }
                    peopleList.Peoples?.AddRange(tempList.Peoples);
                }
                // und setze den neuen Next-Zeiger
                peopleList.Next = tempList?.Next;                
            }
            ObservableCollection<StarWarsPeople> starWarsPeoples = new();
            //Alle "People" zur ListBox hinzufügen
            if(peopleList?.Peoples is not null)
            {
                foreach(StarWarsPeople p in peopleList.Peoples)
                {
                    StarWarsPeoples.Add(p);
                }
            }
            dataGridNames.ItemsSource = starWarsPeoples;
            Mouse.OverrideCursor = null;
        }

        private async Task<string> getRequestAsynch(string url)
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
    }
}
