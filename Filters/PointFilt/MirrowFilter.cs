using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace Filters
{
    class MirrowFilter:Filters
    {
        Random rand1 = new Random();
        Random rand2 = new Random();
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            return sourceImage.GetPixel(Clamp(x + rand1.Next(10) - 5,0,sourceImage.Width-1), 
                                        Clamp(y+  rand2.Next(10) - 5,0,sourceImage.Height-1));
        }

    }
}
