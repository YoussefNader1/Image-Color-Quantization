using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    class Cluster
    {
        int numberOfClusters;
        Dictionary<KeyValuePair<int, int>, double> edges; 
        Prim prim;
        public Cluster(Prim prim, int K)
        {
            this.prim = prim;
            edges = new Dictionary<KeyValuePair<int, int>, double>();
            numberOfClusters = K;
        }
        public void getClusters()
        {
            int edgesCounts = prim.values.Length; 
            for (int i = 0; i < edgesCounts; i++)
            {
                int indexParent = i;
                int valIndexParent = prim.parent[i];
                double valValue = prim.values[i];

                KeyValuePair<int, int> x = new KeyValuePair<int, int>(indexParent, valIndexParent);
                edges.Add(x, valValue);
            }

            foreach (var item in edges)
            {
                Console.WriteLine(item);
            }

            //for (int i = 0; i < numberOfClusters - 1; i++)
            //{

            //}

        }

    }
}
