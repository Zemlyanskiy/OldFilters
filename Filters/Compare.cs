using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing;

namespace Filters
{
    
    class ColorCompare:IComparable
    {
        ColorCompare() { }
        protected int R, G, B;
        public ColorCompare(Color source) {
            R = source.R;
            G = source.G;
            B = source.B;
        }
        public Color getColor()
        {
            return Color.FromArgb(R, G, B);
        }

        public int CompareTo(Object obj)
        {
            double intens_1 = 0.36 * R + 0.53 * G + 0.11 * B;

            double intens_2 = 0.36 * ((ColorCompare)obj).R + 0.53 * ((ColorCompare)obj).G + 0.11 * ((ColorCompare)obj).B;
            if (obj != null)
            {
                if (intens_1 == intens_2)
                    return 0;
                else if (intens_1 > intens_2)
                    return 1;
                else
                    return -1;
            }
            else
                throw new ArgumentException("error");
        }
    }
}
