using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ImageQuantization
{
    class Prim
    {
        private readonly Graph graph;               //O(1)
        // contains MST 
        public int[] parent;               //O(1)     
        
        // = infinity will be used for relaxation 
        public double[] values;               //O(1)

        public Prim(Graph graph)
        {
            this.graph = graph;               //O(1)
        }

        /*
         *  Prim logic
         *  - All nodes weights must be initialy infinity except source node
         *  - Select node with minimum weight
         *  - include selected node in MST
         *  - Relax adjecent edges tothe selected node
         */

        private int selectMinimumVertex(double[] values, bool[] setMST, int numberOfVertex) //O(V)
        {
            double min = double.MaxValue;           //O(1)
            int vertex = 0;                         //O(1)
            for (int i = 0; i < numberOfVertex; i++)// Complexity = O(body) x #iteration = O(1) x V = O(V)
            {
                if (setMST[i] == false && values[i] < min)// Compexity = O(max body) + O(condition) = O(1) x O(1) = O(1)
                {
                    vertex = i;                     //O(1)
                    min = values[i];                //O(1)
                }
            }
            return vertex;               //O(1)
        }

        public double MST()
        {
            int V = graph.distinctColors;               //O(1)
            double minimumSpanningTreeCost = 0.0;       //O(1)
            // contains MST
            parent = new int[V];                        //O(1)
            // = infinity will be used for relaxation
            values = new double[V];                     //O(1)
            // = false this function will let us know which node is visted
            bool[] setMST = new bool[V];                //O(1)

            // Looping to set default values

            for (int i = 0; i < V; i++)                 //O(V)
            {
                values[i] = double.MaxValue;            //O(1)
                setMST[i] = false;                      //O(1)
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

            for (int i = 0; i < V - 1; i++) // Complexity = O(body) x #iteration = O(V) x V  = O(V^2)
            {
                //step1
                int u = selectMinimumVertex(values, setMST, V); //O(V)
                setMST[u] = true; // step2

                for (int j = 0; j < V; j++) // Complexity = O(body) x #iteration = O(1) x V  = O(V)
                {
                    double dis = ImageOperations.CalculateEuclideanDistance(graph.UniqueColors[u], graph.UniqueColors[j]); //O(1)
                    if (dis != 0 && setMST[j] == false && dis < values[j]) // Compexity = O(max body) + O(condition) = O(1) x O(1) = O(1)
                    {
                        values[j] = dis;                //O(1)
                        parent[j] = u;                  //O(1)
                    }
                }
            }

            foreach (double val in values) // Complexity = O(body) x #iteration = O(1) x V = O(V)
                minimumSpanningTreeCost += val;         //O(1)

            return minimumSpanningTreeCost;             //O(1)
        }
    }
}
