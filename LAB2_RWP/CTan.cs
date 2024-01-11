using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB2_RWP
{
    internal class CTan:IFunction
    {
        public float Calc(float x)
        {
            return (float)Math.Tan((double)x);
        }
    }
}
