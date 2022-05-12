using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ImageQuantization
{
    class Prim
    {


        public double CalculateEuclideanDistance(RGBPixel x, RGBPixel y)
        {
            double distance = 0.0;
            distance += Math.Abs(x.red - y.red) * Math.Abs(x.red - y.red);
            distance += Math.Abs(x.green - y.green) * Math.Abs(x.green - y.green);
            distance += Math.Abs(x.blue - y.blue) * Math.Abs(x.blue - y.blue);
            return Math.Sqrt(distance);
        }

        





     }
}
