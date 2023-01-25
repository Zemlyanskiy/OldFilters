using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;

namespace Filters
{
    class ReferenceColor : Filters
    {
		public ReferenceColor() { }
        public Color sourceColor;
        public ReferenceColor(Color _sourceColor) {
			sourceColor = _sourceColor;
        }
        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
			Color newColor;
			newColor = Color.FromArgb(133, 133, 133);
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            long sourceR = 0, sourceG = 0, sourceB = 0;
            

          /*for (int i = 0; i < sourceImage.Width;i++)
			{

				worker.ReportProgress((int)((float)i / resultImage.Width * 50));
				if (worker.CancellationPending)
					return null;

                for (int j = 0; j < sourceImage.Height;j++ )
                {
                    sourceColor = sourceImage.GetPixel(i, j);
                    sourceR = sourceColor.R;
                    sourceG = sourceColor.G;
                    sourceB = sourceColor.B;
                }
			}*/
            //sourceColor = sourceImage.GetPixel(sourceImage.Width-1, sourceImage.Height-1);
            sourceR = sourceColor.R;
            sourceG = sourceColor.G;
            sourceB = sourceColor.B;

			for (int i = 0; i < sourceImage.Width; i++)
				{

					worker.ReportProgress(50 + (int)((float)i / resultImage.Width * 50));
					if (worker.CancellationPending)
						return null;

					for (int j = 0; j < sourceImage.Height; j++)
					{
						sourceColor = sourceImage.GetPixel(i, j);
						sourceColor = Color.FromArgb(Clamp((int)(sourceColor.R * sourceR/ newColor.R), 0, 255),
												  Clamp((int)(sourceColor.G * sourceG/ newColor.G), 0, 255),
												  Clamp((int)(sourceColor.B * sourceB/newColor.B), 0, 255));
						resultImage.SetPixel(i, j, sourceColor);
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