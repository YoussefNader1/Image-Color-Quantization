using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    class Cluster
    {
        int numberOfClusters;
        Graph graph;
        Prim prim;
        public Cluster(Prim prim, Graph graph, int K)
        {
            this.prim = prim;
            this.graph = graph;
            numberOfClusters = K;
        }

        public Dictionary<RGBPixel, RGBPixel> GeneratePallete()
        {
            int[] parent = prim.parent;
            double[] values = prim.values;
            int removedVertices = numberOfClusters - 1;

            /*
             * Theory is to remove the most expensive edges to create clusters
             * as for now the graph is disconnected after being fully connected.
             */

            for (int i = 0; i < removedVertices; i++)
            {
                double maxValue = 0;
                int removedColor = -1;

                for (int j = 0; j < graph.distinctColors; j++)
                {
                    if (values[j] > maxValue)
                    {
                        maxValue = values[j];
                        removedColor = j;
                    }
                }

                parent[removedColor] = -1;
                values[removedColor] = 0;
            }


            Dictionary<int, List<int>> adjacencyList = new Dictionary<int, List<int>>();

            for (int i = 0; i < graph.distinctColors; i++)
            {
                if (!adjacencyList.ContainsKey(i))
                    adjacencyList.Add(i, new List<int>());

                if (parent[i] != -1 && !adjacencyList.ContainsKey(parent[i]))
                    adjacencyList.Add(parent[i], new List<int>());

                if (parent[i] != -1)
                {
                    adjacencyList[parent[i]].Add(i);
                    adjacencyList[i].Add(parent[i]);
                }
            }

            bool[] visitedColors = new bool[graph.distinctColors];
            Queue<int> colors = new Queue<int>();

            Dictionary<RGBPixel, RGBPixel> mappedPallete = new Dictionary<RGBPixel, RGBPixel>();

            for (int i = 0; i < graph.distinctColors; i++)
            {
                if (!visitedColors[i])
                {
                    colors.Enqueue(i);
                    List<int> cluster = new List<int>();

                    long clusterRedTotal = 0, clusterGreenTotal = 0, clusterBlueTotal = 0;

                    while (colors.Count != 0)
                    {
                        int currentColor = colors.First();
                        colors.Dequeue();

                        cluster.Add(currentColor);
                        visitedColors[currentColor] = true;

                        foreach (int child in adjacencyList[currentColor])
                        {
                            if (!visitedColors[child])
                                colors.Enqueue(child);
                        }

                        clusterRedTotal += graph.UniqueColors[currentColor].red;
                        clusterGreenTotal += graph.UniqueColors[currentColor].green;
                        clusterBlueTotal += graph.UniqueColors[currentColor].blue;
                    }

                    clusterRedTotal /= cluster.Count;
                    clusterGreenTotal /= cluster.Count;
                    clusterBlueTotal /= cluster.Count;

                    foreach (int color in cluster) 
                    {
                        RGBPixel newColor = new RGBPixel((byte)(clusterRedTotal),
                                                        (byte)(clusterGreenTotal),
                                                        (byte)(clusterBlueTotal));

                        mappedPallete[graph.UniqueColors[color]] = newColor;
                    }
                }
            }

            return mappedPallete;
        }

    }
}
