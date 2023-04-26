using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AssetBreakdownRegisterV3.Utility
{
    public class SeverityColorList
    {
        private int severity;
        public List<Color> severityColorList;

        public SeverityColorList(int severity)
        {
            this.severity = severity;
        }

        public List<Color> GetList()
        {
            //Create a list of colors with the same index of severity, varies from green-yellow-red
            severityColorList = new List<Color>();
            
            int steps = 510 / (severity - 1);
            int red = 0;
            int green = 255;
            int blue = 0;

            while (red < 256)
            {
                severityColorList.Add(Color.FromArgb(1, red, green, blue));
                red += steps;

            }

            if (red > 255)
            {
                green -= red - 255;
                red = 255;
                severityColorList.Add(Color.FromArgb(1, red, green, blue));
            }

            while (green > 0)
            {
                green -= steps;
                severityColorList.Add(Color.FromArgb(1, red, green, blue));
            }
            
            return severityColorList;

        }
    }
}
