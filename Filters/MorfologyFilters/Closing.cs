using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;


namespace Filters
{
    class Closing : MatrixFilter
    {
        public Closing(float[,] _matrix, int _koef)
        {
            kernel = _matrix;
            koef = _koef;
        }

        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);


            //Расширение
            for (int i = 0; i < sourceImage.Width; i++)
            {

                worker.ReportProgress((int)((float)i / resultImage.Width * 50));
                if (worker.CancellationPending)
                    return null;

                for (int j = 0; j < sourceImage.Height; j++)
                {
                    resultImage.SetPixel(i, j, DilationFunc(sourceImage, i, j));
                }
            }
            Bitmap bufImage = new Bitmap(resultImage.Width, resultImage.Height);

            //Сужение
            for (int i = 0; i < resultImage.Width; i++)
            {

                worker.ReportProgress(50 + (int)((float)i / bufImage.Width * 50));
                if (worker.CancellationPending)
                    return null;

                for (int j = 0; j < resultImage.Height; j++)
                {
                    bufImage.SetPixel(i, j, ErosionFunc(resultImage, i, j));
                }
            }


            return bufImage;
        }
    }
}
