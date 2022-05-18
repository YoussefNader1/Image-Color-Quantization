using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ImageQuantization
{
    class Prim
    {
        private readonly Graph graph;
        public int[] parent; // contains MST

        public double[] values; // = infinity will be used for relaxation

        public Prim(Graph graph)
        {
            this.graph = graph;
        }

        /*
         *  Prim logic
         *  - All nodes weights must be initialy infinity except source node
         *  - Select node with minimum weight
         *  - include selected node in MST
         *  - Relax adjecent edges tothe selected node
         */

        private int selectMinimumVertex(double[] values, bool[] setMST, int numberOfVertex)
        {
            double min = double.MaxValue;
            int vertex = 0;
            for (int i = 0; i < numberOfVertex; i++)
            {
                if (setMST[i] == false && values[i] < min)
                {
                    vertex = i;
                    min = values[i];
                }
            }
            return vertex;
        }

        public double MST()
        {
            int V = graph.distinctCOlors;
            double minimumSpanningTreeCost = 0.0;
            parent = new int[V]; // contains MST
            values = new double[V]; // = infinity will be used for relaxation
            bool[] setMST = new bool[V]; // = false this function will let us know which node is visted

            // Looping to set default values
            
            for (int i = 0; i < V; i++)
            {
                values[i] = double.MaxValue;
                setMST[i] = false;
            }

            /*
             * Assuming starting point is node-zero
             * as first node has no parent
             */

            parent[0] = -1;

            /* 
             * since non all non visited and non relaxed for first time nodes
             * have value = infinity we must make souce node = 0 as we choose smallest node
             */

            values[0] = 0;

            for (int i = 0; i < V - 1; i++)
            {
                int u = selectMinimumVertex(values, setMST, V); //step1
                setMST[u] = true; // step2

                for (int j = 0; j < V; j++)
                {
                    double dis = ImageOperations.CalculateEuclideanDistance(graph.ColorsMap[u], graph.ColorsMap[j]);
                    if (dis != 0 && setMST[j] == false && dis < values[j])
                    {
                        values[j] = dis;
                        parent[j] = u;
                    }
                }
            }

            foreach (double val in values)
                minimumSpanningTreeCost += val;

            return minimumSpanningTreeCost;
        }
    }
}
