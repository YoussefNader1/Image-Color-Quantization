using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ImageQuantization
{
    class Prim 
    {
        /*
         Prim logic
         - All nodes weights must be initialy infinity except source node
         - Select node with minimum weight
         - include selected node in MST
         - Relax adjecent edges tothe selected node
         */
        int selectMinimumVertex(List<int> value ,List<bool> setMST, int numberOfVertex)
        {
            int min = 1000000000;
            int vertex = 0;
            for (int i = 0; i < numberOfVertex; i++)
            {
                if (setMST[i] == false && value[i] <min)
                {
                    vertex = i;
                    min = value[i];
                }
            }

            return vertex;
        }

        
        // Errors will be solved once graph is constructed

        public S MST(int graph[V][V] , int V)
        {
            double minimumSpanningTreeCost = 0.0;
            int parent[] = new int[V]; // contains MST  
            List<int> value[V]; // = infinity will be used for relaxation
            List<bool> setMST[V]; // = false this function will let us know which node is visted

            //Assuming starting point is node-zero

            // as first node has no parent
            parent[0] = -1; 
            /*since non all non visited and non relaxed for first time nodes
             * have value = infinity we must make souce node = 0 as we choose smallest node*/
            value[0] = 0;
            for (int i = 0; i < V - 1; i++)
            {
                int u = selectMinimumVertex(value, setMST, V); //step1
                setMST[u] = true; // step2

                for (int j = 0; j < V; j++)
                {
                    if (graph[u][j] != 0 && setMST[j] = false && graph[u][j] <value[j])
                    {
                        value[j] = graph[u][j];
                        parent[j] = u;
                        

                    }

                }
                
            }
            return minimumSpanningTreeCost; // loop on value list;

        }

     }
}
