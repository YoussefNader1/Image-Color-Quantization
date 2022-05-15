using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ImageQuantization
{
    class Prim
    {
        private readonly Graph graph;

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

        private int selectMinimumVertex(List<double> values, List<bool> setMST, int numberOfVertex)
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
            double[,] matrix = graph.ConstructedGraph;
            int V = graph.UniqueColors.Count;
            
            double minimumSpanningTreeCost = 0.0;
            int[] parent = new int[V]; // contains MST

            List<double> values = new List<double>(V); // = infinity will be used for relaxation
            List<bool> setMST = new List<bool>(V); // = false this function will let us know which node is visted

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
                    if (matrix[u, j] != 0 && setMST[j] == false && matrix[u, j] < values[j])
                    {
                        values[j] = matrix[u, j];
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
