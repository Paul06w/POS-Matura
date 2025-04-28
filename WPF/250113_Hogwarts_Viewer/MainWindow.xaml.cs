using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
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

namespace _250113_Hogwarts_Viewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            Background = new ImageBrush(new BitmapImage(new Uri("./hogwarts_background.jpg", UriKind.RelativeOrAbsolute)));

            InitializeComponent();

            DataContext = this;

            Houses = new ObservableCollection<string> { "All", "Gryffindor", "Slytherin", "Hufflepuff", "Ravenclaw" };            
        }

        private const string ApiUrl = "https://hp-api.herokuapp.com/api/characters";
        private ObservableCollection<Character> _characters;
        private ObservableCollection<string> _houses;
        private Character _selectedCharacter;
        private string _selectedHouse;
        private Visibility _showImages = Visibility.Visible;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Character> Characters
        {
            get => _characters;
            set
            {
                _characters = value;
                OnPropertyChanged(nameof(Characters));                  //Notify the UI that the property has changed
                OnPropertyChanged(nameof(FilteredCharacters));
            }
        }

        public ObservableCollection<string> Houses
        {
            get => _houses;
            set
            {
                _houses = value;
                OnPropertyChanged(nameof(Houses));
            }
        }

        public ObservableCollection<Character> FilteredCharacters
        {
            get
            {
                if (string.IsNullOrEmpty(SelectedHouse) || SelectedHouse == "All")
                    return Characters;

                return new ObservableCollection<Character>(
                    Characters?.Where(c => c.House.Equals(SelectedHouse, StringComparison.OrdinalIgnoreCase))    //Filter the characters based on the selected house
                );
            }
        }

        public Character SelectedCharacter
        {
            get => _selectedCharacter;
            set
            {
                _selectedCharacter = value;
                OnPropertyChanged(nameof(SelectedCharacter));
            }
        }

        public string SelectedHouse
        {
            get => _selectedHouse;
            set
            {
                _selectedHouse = value;
                OnPropertyChanged(nameof(SelectedHouse));
                OnPropertyChanged(nameof(FilteredCharacters));
            }
        }

        public Visibility ShowImages
        {
            get => _showImages;
            set
            {
                _showImages = value;
                OnPropertyChanged(nameof(ShowImages));
            }
        }

        private void ShowImage_Checked(object sender, RoutedEventArgs e)
        {
            ShowImages = Visibility.Visible;
        }

        private void ShowImage_Unchecked(object sender, RoutedEventArgs e)
        {
            ShowImages = Visibility.Collapsed;
        }

        private async Task LoadCharactersAsync()
        {
            try
            {
                string jsonFilePath = "characters.json";

                //Load characters from the API
                using HttpClient client = new HttpClient();
                string jsonResponse = await client.GetStringAsync(ApiUrl);
                var characterList = JsonSerializer.Deserialize<ObservableCollection<Character>>(jsonResponse);
                //string fileContent = await File.ReadAllTextAsync(jsonFilePath);
                //var characterList = JsonSerializer.Deserialize<ObservableCollection<Character>>(fileContent);

                Characters = characterList;

                
                //string jsonConent = JsonSerializer.Serialize(_characters);
                File.WriteAllText(jsonFilePath, jsonResponse);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load characters: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void LoadAllButton_Click(object sender, RoutedEventArgs e)
        {
            await LoadCharactersAsync();
        }

        //Notify the UI that a property has changed
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}