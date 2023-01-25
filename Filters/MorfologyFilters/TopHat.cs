using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;

namespace Filters
{

    class TopHat : MatrixFilter
    {

        public TopHat(float[,] _matrix, int _koef)
        {
            kernel = _matrix;
            koef = _koef;
        }

        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);//Для расширения

            //ЗАКРЫТИЕ
            //Расширение
            for (int i = 0; i < sourceImage.Width; i++)
            {

                worker.ReportProgress((int)((float)i / resultImage.Width * 33));
                if (worker.CancellationPending)
                    return null;

                for (int j = 0; j < sourceImage.Height; j++)
                {
                    resultImage.SetPixel(i, j, DilationFunc(sourceImage, i, j));
                }
            }
            Bitmap bufImage = new Bitmap(resultImage.Width, resultImage.Height);//Для сужения

            //Сужение
            for (int i = 0; i < resultImage.Width; i++)
            {

                worker.ReportProgress(33 + (int)((float)i / bufImage.Width * 33));
                if (worker.CancellationPending)
                    return null;

                for (int j = 0; j < resultImage.Height; j++)
                {
                    bufImage.SetPixel(i, j, ErosionFunc(resultImage, i, j));
                }
            }

            Bitmap subImage = new Bitmap(bufImage.Width, bufImage.Height);//В ней результат вычитания
           
            
            //ВЫЧИТАНИЕ ИЗ НАЧАЛА
            for (int i = 0; i < bufImage.Width; i++)
            {

                worker.ReportProgress(66 + (int)((float)i / subImage.Width * 33));
                if (worker.CancellationPending)
                    return null;

                for (int j = 0; j < bufImage.Height; j++)
                {
                    Color FirstColor = sourceImage.GetPixel(i, j);
                    Color SecondColor = bufImage.GetPixel(i, j);
                    subImage.SetPixel(i, j, Substraction(FirstColor,SecondColor));
                }
            }
            
            
            return subImage;
        }


        Color Substraction(Color First, Color Second)
        {
            //Console.Write("{0} , {1} , {2}\n",First.R,First.G,First.B);
            Color Result = Color.FromArgb(Clamp(Second.R - First.R, 0, 255),
                                          Clamp(Second.G - First.G, 0, 255),
                                          Clamp(Second.B - First.B, 0, 255));
            return Result;
        }
    }
}


