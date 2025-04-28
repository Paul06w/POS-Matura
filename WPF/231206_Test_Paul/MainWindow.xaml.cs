using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;

namespace _231206_Test_Paul
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ObservableCollection<User> userList = new ObservableCollection<User>();
        private ObservableCollection<Album> albumList = new ObservableCollection<Album>();

        public MainWindow()
        {
            InitializeComponent();
            //findAlbum(1);
            //findPhoto(1);
        }


        private async void getUser()
        {
            dataGridUserInformation.ItemsSource = userList;
            Mouse.OverrideCursor = Cursors.Wait;

            string user = await getRequestAsync("users");
            List<User> list = JsonConvert.DeserializeObject<List<User>>(user);
            if (list != null)
            {
                foreach (User u in list)
                {
                    userList.Add(u);
                }
                list.AddRange(list);
            }
            dataGridUserInformation.ItemsSource = userList;

            /*HttpClient client = new HttpClient();
            HttpResponseMessage http = await client.GetAsync("https://jsonplaceholder.typicode.com/users/");
            List<User> list = JsonConvert.DeserializeObject<List<User>>(await http.Content.ReadAsStringAsync());
            foreach (User u in list)
            {
                userList.Add(u);
            }
            */


            Mouse.OverrideCursor = null;
        }

        private async Task<string> getRequestAsync(string url)
        {
            HttpClient client = new HttpClient();
            Uri baseUri = new Uri("https://jsonplaceholder.typicode.com/");
            client.BaseAddress = baseUri;
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var task = await client.SendAsync(requestMessage);
            HttpResponseMessage msg = task.EnsureSuccessStatusCode();
            string responseBody = await task.Content.ReadAsStringAsync();
            return responseBody;
        }

        private void dataGridUserInformation_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            User people = (User)dataGrid.SelectedItem;

            findAlbum(people.id);
        }

        private async void findAlbum(int userId)
        {
            albumList.Clear();
            dataGridFavouriteAlbums.ItemsSource = albumList;

            Mouse.OverrideCursor = Cursors.Wait;

            HttpClient client = new HttpClient();
            HttpResponseMessage http = await client.GetAsync("https://jsonplaceholder.typicode.com/users/" + userId + "/albums");
            Album album = JsonConvert.DeserializeObject<Album>(await http.Content.ReadAsStringAsync());
            albumList.Add(album);
            Mouse.OverrideCursor = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            getUser();
        }

        private void dataGridFavouriteAlbums_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            Album album = (Album)dataGrid.SelectedItem;

            findPhoto(album.id);
        }

        private async void findPhoto(int albumId)
        {
            Mouse.OverrideCursor = Cursors.Wait;

            HttpClient client = new HttpClient();
            HttpResponseMessage http = await client.GetAsync("https://jsonplaceholder.typicode.com/albums/" + albumId + "/photos");
            Photo photo = JsonConvert.DeserializeObject<Photo>(await http.Content.ReadAsStringAsync());
            var uri = new Uri(photo.url);
            var bitmap = new BitmapImage(uri);
            imagePhoto.Source = bitmap;
            Mouse.OverrideCursor = null;
        }
    }



}
