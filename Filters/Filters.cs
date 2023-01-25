using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;   //Вообще этого инклюда как бы нет в методичке
using System.Drawing;
using System.ComponentModel;



namespace Filters
{
    abstract class Filters
    {
        protected double calculateIntensity(int R, int G, int B)
        {
            return 0.36 * R + 0.53 * G + 0.11 * B;
        }

        protected abstract Color calculateNewPixelColor(Bitmap sourceImage, int x, int y);

        public virtual Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);

            for (int i = 0; i < sourceImage.Width; i++)
            {   
                
                     worker.ReportProgress((int)((float)i / resultImage.Width * 100));
                     if (worker.CancellationPending)
                         return null;
                   
                    for (int j = 0; j < sourceImage.Height; j++)
                {
                    resultImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));
                }
            }
            
            
            return resultImage;
        }

        public int Clamp(int value, int min, int max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
    }
}
