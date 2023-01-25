using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace Filters
{
    class MedianFilter:MatrixFilter
    {

        public MedianFilter(float[,] _matrix, int _koef)
        {
            kernel = _matrix;
            koef = _koef;
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            if ((x <koef) || (y <koef)||(x>=sourceImage.Width - koef)||(y>=sourceImage.Height - koef))
            {
                return sourceImage.GetPixel(x, y);
            }
            else
            {
                int[] median = new int[kernel.GetLength(0) * kernel.GetLength(1)];
                int R, G, B;
                Color BufColor;
               //Заполнение массива значениями интенсивности
                for (int k = 0; k < kernel.GetLength(0); k++)
                    for (int m = 0; m < kernel.GetLength(1); m++)
                    {
                        BufColor = sourceImage.GetPixel(x - koef + k,y - koef + m);
                        R = BufColor.R;
                        G = BufColor.G;
                        B = BufColor.B;
                        median[k * kernel.GetLength(0) + m] = (int)calculateIntensity(R, G, B);
                       
                    }
                //Сортируем
                Array.Sort(median);
                int med = median.GetLength(0) / 2;//серединный элемент
                //Ищем сравнения
                
                for (int k = 0; k < kernel.GetLength(0); k++)
                    for (int m = 0; m < kernel.GetLength(1); m++)
                    {
                        BufColor = sourceImage.GetPixel(x - koef + k, y - koef + m);
                        R = BufColor.R;
                        G = BufColor.G;
                        B = BufColor.B;
                        if (median[med] == (int)calculateIntensity(R, G, B))
                            return sourceImage.GetPixel(x - koef + k, y - koef + m);
                    }
               
                return Color.FromArgb(255, 255,255);
            }
        }
    }
}
