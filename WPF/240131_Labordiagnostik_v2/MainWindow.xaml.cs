using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq.Expressions;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace _240131_Labordiagnostik
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Mikrotiterplatte mikrotiterplatte = new Mikrotiterplatte();
        bool isClicked = false;
        ObservableCollection<Messwert> wells = new ObservableCollection<Messwert>();

        public MainWindow()
        {
            InitializeComponent();

            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri("./labor.jpg", UriKind.RelativeOrAbsolute));      //Hintergrund
            //myBrush.Opacity = 0.8;
            this.Background = myBrush;
        }

        private void ButtonLoadValues_Click(object sender, RoutedEventArgs e)
        {
            mikrotiterplatte.LoadValues();          //Laden der Daten

            foreach(Messwert m in mikrotiterplatte.MesswertList)
            {
                m.CalculateIndexes();
            }
        }

        private void ComboBoxAuswahl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Messwert> messwertList = new List<Messwert>();
            
            if(ComboBoxAuswahl.SelectedIndex == 0)
            {
                messwertList = mikrotiterplatte.MesswertList;

                foreach(Messwert m in messwertList)
                {
                    int[] rgb = mikrotiterplatte.SetColor(m.AbsorbanceValue, 0);
                    SolidColorBrush brush = new SolidColorBrush(Color.FromRgb((byte)rgb[0], (byte)rgb[1], (byte)rgb[2]));
                    string text = "";

                    if (m.AbsorbanceValue == null)      //wenn kein Messwert --> Ausgabe NaN
                    { 
                        text = "NaN"; 
                    }
                    else
                    {
                        text = m.AbsorbanceValue.ToString();
                    }

                    switch (m.WellName)         //Zuweisung zum jeweiligen Well
                    {
                        case "A1": TextA1.Text = text; A1.Fill = brush; break;
                        case "A2": TextA2.Text = text; A2.Fill = brush; break;
                        case "A3": TextA3.Text = text; A3.Fill = brush; break;
                        case "A4": TextA4.Text = text; A4.Fill = brush; break;
                        case "B1": TextB1.Text = text; B1.Fill = brush; break;
                        case "B2": TextB2.Text = text; B2.Fill = brush; break;
                        case "B3": TextB3.Text = text; B3.Fill = brush; break;
                        case "B4": TextB4.Text = text; B4.Fill = brush; break;
                        case "C1": TextC1.Text = text; C1.Fill = brush; break;
                        case "C2": TextC2.Text = text; C2.Fill = brush; break;
                        case "C3": TextC3.Text = text; C3.Fill = brush; break;
                        case "C4": TextC4.Text = text; C4.Fill = brush; break;
                    }
                }
            }
            else if(ComboBoxAuswahl.SelectedIndex == 1)
            {
                messwertList = mikrotiterplatte.MesswertList;

                foreach (Messwert m in messwertList)
                {
                    int[] rgb = mikrotiterplatte.SetColor(m.FluorescenceValue, 1);
                    SolidColorBrush brush = new SolidColorBrush(Color.FromRgb((byte)rgb[0], (byte)rgb[1], (byte)rgb[2]));

                    string text = "";

                    if (m.FluorescenceValue == null)            //wenn kein Messwert --> Ausgabe NaN
                    {
                        text = "NaN";
                    }
                    else
                    {
                        text = m.FluorescenceValue.ToString();
                    }

                    switch (m.WellName)             //Zuweisung zum jeweiligen Well
                    {
                        case "A1": TextA1.Text = text; A1.Fill = brush; break;
                        case "A2": TextA2.Text = text; A2.Fill = brush; break;
                        case "A3": TextA3.Text = text; A3.Fill = brush; break;
                        case "A4": TextA4.Text = text; A4.Fill = brush; break;
                        case "B1": TextB1.Text = text; B1.Fill = brush; break;
                        case "B2": TextB2.Text = text; B2.Fill = brush; break;
                        case "B3": TextB3.Text = text; B3.Fill = brush; break;
                        case "B4": TextB4.Text = text; B4.Fill = brush; break;
                        case "C1": TextC1.Text = text; C1.Fill = brush; break;
                        case "C2": TextC2.Text = text; C2.Fill = brush; break;
                        case "C3": TextC3.Text = text; C3.Fill = brush; break;
                        case "C4": TextC4.Text = text; C4.Fill = brush; break;
                    }
                }
            }
        }



        private void ButtonConnect_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            isClicked = !isClicked;

            if(isClicked == true)
            {
                btn.Content = "Disconnect";
                btn.Background = new SolidColorBrush(Colors.Red);

                string wellinfo = "";
                string ip = TextServerIP.Text;
                string port = TextServerPort.Text;

                if (ComboBoxWell.SelectedIndex == 0)
                {
                    wellinfo = "wellData";


                }
                else if (ComboBoxWell.SelectedIndex == 1)
                {
                    wellinfo = "wellDataSingle";


                }

                mikrotiterplatte.LoadValuesMQTT(ip, port, wellinfo);


            }
            else if (isClicked == false)
            {
                btn.Content = "Connect";
                btn.Background = new SolidColorBrush(Colors.Lime);
            }
            
            

        }

        private void ComboBoxSource_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxSource.SelectedIndex == 0)
            {
                ComboBoxWell.IsEnabled = false;
                TextServerIP.IsEnabled = false;
                TextServerPort.IsEnabled = false;
                ButtonConnect.IsEnabled = false;
                ComboBoxAuswahl.IsEnabled = true;
                ButtonLoadValues.IsEnabled = true;

            }
            else if (ComboBoxSource.SelectedIndex == 1)
            {
                ComboBoxWell.IsEnabled = true;
                TextServerIP.IsEnabled = true;
                TextServerPort.IsEnabled = true;
                ButtonConnect.IsEnabled = true;
                ComboBoxAuswahl.IsEnabled = true;
                ButtonLoadValues.IsEnabled = false;

            }
        }

        private void ComboBoxWell_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}