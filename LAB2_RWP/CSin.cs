using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB2_RWP
{
    internal class CSin: IFunction
    {
        public float Calc(float x) 
        {
            double dbX=(double)x;
            return (float)Math.Sin(dbX);
        }
    }
}
