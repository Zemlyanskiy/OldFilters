using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filters
{
    class PriuttFilter:MatrixFilter
    {
        public PriuttFilter()
        {
            kernel = new float[3, 3] {{-1, -1, -1},
                                      { 0,  0,  0},
                                      { 1,  1,  1}};
        }
    }
}
