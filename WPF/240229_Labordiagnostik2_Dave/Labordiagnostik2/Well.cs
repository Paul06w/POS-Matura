using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace Labordiagnostik
{
    internal class Well
    {
        [JsonIgnore]
        public Coordinates Coordinates { get; set; }
        public string WellName { get; set; }
        public int WellIndex { get; set; }
        [JsonPropertyName("AbsorbanceValue")]
        public double? Absorbance { get; set; }
        [JsonPropertyName("FluorescenceValue")]
        public int? Fluorescence { get; set; }
        [JsonIgnore]
        public Ellipse Ellipse { get; set; }
        [JsonIgnore]
        public Label Label { get; set; }

        public Well()  
        {

        }

        public Well(string wellName, int wellIndex, int absorbance, int fluorescence)
        {
            this.WellName = wellName;
            this.WellIndex = wellIndex;
            this.Absorbance = absorbance;
            this.Fluorescence = fluorescence;

            this.calcCoordinates();
        }

        public void calcCoordinates()
        {
            int x = this.WellName[1] - '0';

            int y;

            //Returning the numeric
            //value of the x-coordinate
            switch (WellName[0])
            {
                case 'A':
                    y = 0;
                    break;

                case 'B':
                    y = 1;
                    break;

                case 'C':
                    y = 2;
                    break;

                default:
                    throw new Exception("auslese fehler");
            }

            this.Coordinates = new Coordinates(x - 1, y);
        }

        public override string ToString()
        {
            return WellName + ":" + WellIndex;
        }

        //Returning the letter
        //value of the y-coordinate
        public string getYCoor(int y)
        {
            switch (y)
            {
                case 0:
                    return "A";

                case 1:
                    return "B";

                case 2:
                    return "C";

                case 3:
                    return "D";

                case 4:
                    return "E";

                case 5:
                    return "F";

                case 6:
                    return "G";

                case 7:
                    return "H";

                default:
                    return null;
            }
        }
    }
}