using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

public enum MQTT
{
    Entire,
    Single,
    Cancel
}

namespace Labordiagnostik
{
    /// <summary>
    /// Interaktionslogik für MQTTSelectionWindow.xaml
    /// </summary>
    public partial class MQTTSelectionWindow : Window
    {
        public MQTT Selection { get; set; } = MQTT.Cancel;

        public MQTTSelectionWindow()
        {
            InitializeComponent();
        }

        private void entireClick(object sender, RoutedEventArgs e)
        {
            this.Selection = MQTT.Entire;
            this.DialogResult = true;
            this.Close();
        }

        private void singleClick(object sender, RoutedEventArgs e)
        {
            this.Selection = MQTT.Single;
            this.DialogResult = true;
            this.Close();
        }
    }
}
