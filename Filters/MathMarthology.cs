using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;

namespace Filters
{
    class MathMarthology: Filters
    {
        //public Color sourceImage;
        public const int DILATION = 0;
        public const int EROSION = 1;
        public const int MEDIAN = -1;
		static int type;
        List<ColorCompare> circle = new List<ColorCompare>();
        protected int koef;
        //коефицент отхода от границ
        public float[,] kernel = null;
        protected MathMarthology() { }
        public MathMarthology(float[,] kernel,int _type)
        {
			type = _type;
            this.kernel = kernel;
        }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;

            float resultR = 0;
            float resultG = 0;
            float resultB = 0;

            for (int l = -radiusY; l <= radiusY; l++)
                for (int k = -radiusX; k <= radiusX; k++)
                {
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);
                    resultR += neighborColor.R * kernel[k + radiusX, l + radiusX];
                    resultG += neighborColor.G * kernel[k + radiusX, l + radiusX];
                    resultB += neighborColor.B * kernel[k + radiusX, l + radiusX];
                }

            return Color.FromArgb(Clamp((int)resultR, 0, 255),
                                  Clamp((int)resultG, 0, 255),
                                  Clamp((int)resultB, 0, 255));

        }
        protected Color MorfologyFunc(Bitmap sourceImage, int x, int y)
        {
            if ((x < koef) || (y < koef) || (x >= sourceImage.Width - koef) || (y >= sourceImage.Height - koef))
            {
                return sourceImage.GetPixel(x, y);
            }
            else
            {
              
                Color BufColor;
                
                //Заполнение массива значениями интенсивности
                for (int k = 0; k < kernel.GetLength(0); k++)
                    for (int m = 0; m < kernel.GetLength(1); m++)
                    {
                        BufColor = sourceImage.GetPixel(x - koef + k, y - koef + m);
                        
                        circle.Add(new ColorCompare(BufColor));

                    }
                //Сортируем
                circle.Sort();
                ColorCompare c = (ColorCompare)circle[0];
                c.getColor();

                
                switch (type)
                {
					case DILATION:
                        c = (ColorCompare)circle[0];
                        return c.getColor();
                    case EROSION:
                        c = (ColorCompare)circle[8];
                        return c.getColor();
                    case MEDIAN:
                        c = (ColorCompare)circle[5];
                        return c.getColor();
				}
				
                return Color.FromArgb(255, 255, 255);
            }
        }

    }
}
