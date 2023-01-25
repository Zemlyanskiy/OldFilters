using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;

namespace Filters
{
    class GreyWorld : Filters
    {
        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            long AvgR = 0, AvgG = 0, AvgB = 0;
            Color BufColor;
            //Суммируем среднее
            for (int i = 0; i < sourceImage.Width; i++)
            {

                worker.ReportProgress((int)((float)i / resultImage.Width * 50));
                if (worker.CancellationPending)
                    return null;

                for (int j = 0; j < sourceImage.Height; j++)
                {
                    BufColor = sourceImage.GetPixel(i, j);
                    AvgR += BufColor.R;
                    AvgG += BufColor.G;
                    AvgB += BufColor.B;
                }
            }
            //Считаем среднее
            int N = sourceImage.Height * sourceImage.Width;
            AvgR /=N;
            AvgG /=N;
            AvgB /=N;

            long AvgAll = (AvgR + AvgG + AvgB) / 3;
            //Умножаем на каждый
            for (int i = 0; i < sourceImage.Width; i++)
            {

                worker.ReportProgress(50 + (int)((float)i / resultImage.Width * 50));
                if (worker.CancellationPending)
                    return null;

                for (int j = 0; j < sourceImage.Height; j++)
                {
                    BufColor = sourceImage.GetPixel(i, j);
                    BufColor = Color.FromArgb(Clamp((int)(BufColor.R * AvgR / AvgAll), 0, 255),
                                              Clamp((int)(BufColor.G * AvgG / AvgAll), 0, 255),
                                              Clamp((int)(BufColor.B * AvgB / AvgAll), 0, 255));
                    resultImage.SetPixel(i, j, BufColor);
                }
            }


            return resultImage;
        }




        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            Color resultColor = Color.FromArgb(255,
                                              255,
                                              255);
            return resultColor;
        }
    }
}
