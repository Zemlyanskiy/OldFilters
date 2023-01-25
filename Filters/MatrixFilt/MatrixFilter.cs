using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace Filters
{
    class MatrixFilter : Filters
    {
        protected int koef;
           //коефицент отхода от границ
        public float[,] kernel = null;
        protected MatrixFilter() { }
        public MatrixFilter(float[,] kernel)
        {
            this.kernel = kernel;
        }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int radiusX = kernel.GetLength(0)/2;
            int radiusY = kernel.GetLength(1)/2;
            
            float resultR = 0;
            float resultG = 0;
            float resultB = 0;

            for (int l = -radiusY; l<=radiusY;l++)
                for(int k = -radiusX; k<=radiusX;k++)
                {
                    int idX = Clamp(x+k,0, sourceImage.Width - 1);
                    int idY = Clamp(y+l,0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX,idY);
                    resultR += neighborColor.R*kernel[k+radiusX,l+radiusX];
                    resultG += neighborColor.G*kernel[k+radiusX,l+radiusX];
                    resultB += neighborColor.B*kernel[k+radiusX,l+radiusX];
                }

        return Color.FromArgb(Clamp((int)resultR,0,255),
                              Clamp((int)resultG,0,255),
                              Clamp((int)resultB,0,255));
        
        }

        //Записал функции сужения и расширения для реализации открытия, закрытия и top hat
        //Функция для расширения
        protected Color DilationFunc(Bitmap sourceImage, int x, int y)
        {
            if ((x < koef) || (y < koef) || (x >= sourceImage.Width - koef) || (y >= sourceImage.Height - koef))
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
                        BufColor = sourceImage.GetPixel(x - koef + k, y - koef + m);
                        R = BufColor.R;
                        G = BufColor.G;
                        B = BufColor.B;
                        median[k * kernel.GetLength(0) + m] = (int)calculateIntensity(R, G, B);

                    }
                //Сортируем
                Array.Sort(median);
                int med = median.GetLength(0) - 1;//по сравнению с матрицей здесь не серединный, а максимальный элемент
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

                return Color.FromArgb(255, 255, 255);
            }
        }
        //Функция для сужения
        protected Color ErosionFunc(Bitmap sourceImage, int x, int y)
        {
            if ((x < koef) || (y < koef) || (x >= sourceImage.Width - koef) || (y >= sourceImage.Height - koef))
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
                        BufColor = sourceImage.GetPixel(x - koef + k, y - koef + m);
                        R = BufColor.R;
                        G = BufColor.G;
                        B = BufColor.B;
                        median[k * kernel.GetLength(0) + m] = (int)calculateIntensity(R, G, B);

                    }
                //Сортируем
                Array.Sort(median);
                int med = 0;//минимальный элемент
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

                return Color.FromArgb(255, 255, 255);
            }
        }
    }
}
