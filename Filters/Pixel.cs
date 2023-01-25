using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;

namespace Filters
{
	class Pixel:Filters
	{
		
		public Pixel() { }
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
