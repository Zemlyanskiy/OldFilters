using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace Filters
{
    class BrightFilter:Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int k = 30;
            Color sourceColor = sourceImage.GetPixel(x, y);
            int resultR = sourceColor.R+k;
            int resultG = sourceColor.G+k;
            int resultB = sourceColor.B+k;
            Color resultColor = Color.FromArgb(Clamp(resultR, 0, 255), Clamp(resultG, 0, 255), Clamp(resultB, 0, 255));

            return resultColor;
        }
    }
}
