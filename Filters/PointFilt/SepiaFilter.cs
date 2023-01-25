using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace Filters
{
    class SepiaFilter: Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int k = 30;
            Color sourceColor = sourceImage.GetPixel(x, y);
            int Intensity= (int)(calculateIntensity(sourceColor.R, sourceColor.G, sourceColor.B));
            int resultR=Intensity + 2*k;
            int resultG = (int)(Intensity + 0.5*k);
            int resultB = Intensity-k;
            Color resultColor = Color.FromArgb(Clamp(resultR, 0, 255), Clamp(resultG, 0, 255), Clamp(resultB, 0, 255));
            
            return resultColor;
        }

    }
}
