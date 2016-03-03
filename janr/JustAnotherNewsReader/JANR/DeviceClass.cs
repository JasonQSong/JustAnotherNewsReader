using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JANR
{
    public class DeviceClass
    {
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public double PixelRatio { get; set; }
        public string UserAgent { get; set; }
        public int Margin_Top { get; set; }
        public int Margin_Bottom { get; set; }
        public int Margin_Left { get; set; }
        public int Margin_Right { get; set; }
        public string ScreenImage { get; set; }
        public DeviceClass()
        {
            this.Name = "Google Nexus 5";
            this.Width = 1080;
            this.Height = 1920;
            this.PixelRatio = 3;
        }
        public override string ToString()
        {
            return String.Format("{0} ({1}*{2})",this.Name,this.Width.ToString(),this.Height.ToString());
        }
    }
}
