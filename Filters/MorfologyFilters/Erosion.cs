using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Filters

{
    //Сужение
    class Erosion:MatrixFilter
    {
        

        public Erosion(float[,] _matrix, int _koef)
        {
            kernel = _matrix;
            koef = _koef;
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            return ErosionFunc(sourceImage, x, y);
        }
    }
}
