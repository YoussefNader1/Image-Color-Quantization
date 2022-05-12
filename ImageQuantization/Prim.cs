using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ImageQuantization
{
    class Prim
    {

        struct Edge
        {
            int from, to, w;

            

            Edge(int from, int to, int w) /*: from(from), to(to), W(w)*/ {

                this.from = from;
                this.to = to;
                this.w = w;
            }
            //public bool operator <(in Edge e)
            //{
            //    return w > e.w;
            //}
        };


     }
}
