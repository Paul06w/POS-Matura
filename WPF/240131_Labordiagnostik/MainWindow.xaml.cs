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

namespace _240131_Labordiagnostik
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Mikrotiterplatte mikrotiterplatte = new Mikrotiterplatte();

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
    }
}