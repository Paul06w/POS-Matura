using Microsoft.Win32;
using System.Collections.ObjectModel;
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

enum ReadingSource
{
    JSON,
    MQTT
}

enum SavingDestination
{
    JSON
}

enum Showing
{
    OD,
    RFU
}

namespace Labordiagnostik
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Ellipse[,] _ellipses = new Ellipse[4, 3];
        private Label[,] _labels = new Label[4, 3];
        private ObservableList<Well> _wells = new ObservableList<Well>();
        private MicroPlateReader mpr = new MicroPlateReader(new JSONReadingStrategy(), new JSONSavingStrategy());
        private readonly object _lock = new object();

        public MainWindow()
        {
            InitializeComponent();

            readingSource.Items.Add(ReadingSource.JSON);
            readingSource.Items.Add(ReadingSource.MQTT);
            readingSource.SelectedIndex = 0;

            savingDestination.Items.Add(SavingDestination.JSON);
            savingDestination.SelectedIndex = 0;

            showing.Items.Add(Showing.OD);
            showing.Items.Add(Showing.RFU);
            showing.SelectedIndex = 0;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Grid grid = new();
                    grid.HorizontalAlignment = HorizontalAlignment.Stretch;
                    grid.VerticalAlignment = VerticalAlignment.Stretch;

                    Ellipse ellipse = new();
                    ellipse.Stroke = Brushes.Black;
                    ellipse.Margin = new Thickness(5);
                    ellipse.StrokeThickness = 3;
                    ellipse.Stretch = Stretch.UniformToFill;
                    ellipse.VerticalAlignment = VerticalAlignment.Center;
                    ellipse.HorizontalAlignment = HorizontalAlignment.Center;

                    Label label = new();

                    _ellipses[i, j] = ellipse;
                    _labels[i, j] = label;

                    uniGrid.Children.Add(grid);
                    grid.Children.Add(ellipse);
                    grid.Children.Add(label);
                }
            }
        }

        private async void readData(object sender, RoutedEventArgs e)
        {
            lock (_lock)
            {
                try
                {
                    this.mpr.ReadMesswerte(this._wells);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There has been an error!\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                foreach (var well in _wells)
                {
                    well.calcCoordinates();
                    well.Ellipse = _ellipses[well.Coordinates.X, well.Coordinates.Y];
                    well.Label = _labels[well.Coordinates.X, well.Coordinates.Y];
                }

                this.colourWells();
                _wells.ListChanged += OnListChanged;
            }
        }

        private void colourWells()
        {
            Showing? show = null;

            Application.Current.Dispatcher.Invoke(() =>
            {
                show = (Showing)showing.SelectedItem;

                foreach (var well in _wells)
                {
                    double interpolation;
                    SolidColorBrush interpolatedColor;
                    switch (show)
                    {
                        case Showing.OD:
                            if (well.Absorbance is null)
                            {
                                colorWell(well, new SolidColorBrush(Colors.White), "NaN");
                                continue;
                            }

                            interpolation = (double)well.Absorbance / 5.0;
                            interpolatedColor = new SolidColorBrush(InterpolateColor(Colors.White, Colors.Blue, interpolation));
                            colorWell(well, interpolatedColor, well.Absorbance + "");
                            break;

                        case Showing.RFU:
                            if (well.Fluorescence is null)
                            {
                                colorWell(well, new SolidColorBrush(Colors.White), "NaN");
                                continue;
                            }

                            interpolation = (double)well.Fluorescence / 65535.0;
                            interpolatedColor = new SolidColorBrush(InterpolateColor(Colors.White, Colors.Red, interpolation));
                            colorWell(well, interpolatedColor, well.Fluorescence + "");
                            break;
                    }
                }
            });
        }

        public void OnListChanged(object sender, EventArgs e)
        {
            foreach (var well in _wells)
            {
                well.calcCoordinates();
                well.Ellipse = _ellipses[well.Coordinates.X, well.Coordinates.Y];
                well.Label = _labels[well.Coordinates.X, well.Coordinates.Y];
            }

            this.colourWells();
        }

        private void colorWell(Well well, Brush color, string text)
        {
            well.Ellipse.Fill = color;
            well.Label.Content = text;
        }

        private Color InterpolateColor(Color startColor, Color endColor, double factor)
        {
            byte r = (byte)(startColor.R + (endColor.R - startColor.R) * factor);
            byte g = (byte)(startColor.G + (endColor.G - startColor.G) * factor);
            byte b = (byte)(startColor.B + (endColor.B - startColor.B) * factor);
            return Color.FromArgb(255, r, g, b);
        }

        private void switchData(object sender, SelectionChangedEventArgs e)
        {
            this.colourWells();
        }

        private void save(object sender, RoutedEventArgs e)
        {
            mpr.SaveMesswerte(_wells);
        }

        private void ChangeSize(object sender, SizeChangedEventArgs e)
        {
            double aspectRatio = 800.0 / 700.0;

            // Preserve aspect ratio
            double newWidth = e.NewSize.Height * aspectRatio;
            double newHeight = e.NewSize.Width / aspectRatio;

            // Adjust width or height based on which dimension is being resized
            if (newWidth > e.NewSize.Width)
                this.Width = newWidth;
            else
                this.Height = newHeight;
        }

        private void switchReadingStrategy(object sender, SelectionChangedEventArgs e)
        {
            switch (readingSource.SelectedItem)
            {
                case ReadingSource.JSON:
                    mpr.ReadingStrategy = new JSONReadingStrategy();
                    break;

                case ReadingSource.MQTT:
                    mpr.ReadingStrategy = new MQTTReadingStrategy();
                    break;
            }
        }
    }
}