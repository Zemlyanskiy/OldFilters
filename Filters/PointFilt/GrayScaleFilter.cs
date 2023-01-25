using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Filters
{
    class GrayScaleFilter :Filters
    {
       
        
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {   
        
            Color sourceColor = sourceImage.GetPixel(x, y);
            int Intensity = (int)(calculateIntensity(sourceColor.R,sourceColor.G, sourceColor.B));

            Color resultColor = Color.FromArgb(Intensity, Intensity, Intensity);
            return resultColor;
        }
    }
}
