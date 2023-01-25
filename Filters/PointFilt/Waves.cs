using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace Filters
{
    class Waves:Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            return sourceImage.GetPixel(Clamp((int)(x+20*Math.Sin(2*Math.PI*y/60)),0,sourceImage.Width-1),y);
        }
    }
}
