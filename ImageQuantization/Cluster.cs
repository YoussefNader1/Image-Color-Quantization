using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    class Cluster
    {
        int numberOfClusters;              //O(1)
        Graph graph;              //O(1)
        Prim prim;              //O(1)
        public Cluster(Prim prim, Graph graph, int K)
        {
            this.prim = prim;              //O(1)
            this.graph = graph;              //O(1)
            numberOfClusters = K;              //O(1)
        }

        private RGBPixel[,,] GeneratePallete()
        {
            int[] parent = prim.parent;              //O(1)
            double[] values = prim.values;              //O(1)
            int removedVertices = numberOfClusters - 1;              //O(1)

            /*
             * Theory is to remove the most expensive edges to create clusters
             * as for now the graph is disconnected after being fully connected.
             */

            for (int i = 0; i < removedVertices; i++)
            {
                double maxValue = 0;              //O(1)
                int removedColor = -1;              //O(1)

                for (int j = 0; j < graph.distinctColors; j++)
                {
                    if (values[j] > maxValue)
                    {
                        maxValue = values[j];              //O(1)
                        removedColor = j;              //O(1)
                    }
                }

                parent[removedColor] = -1;              //O(1)
                values[removedColor] = 0;              //O(1)
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

            RGBPixel[,,] map = new RGBPixel[260, 260, 260];

            Dictionary<RGBPixel, RGBPixel> mappedPallete = new Dictionary<RGBPixel, RGBPixel>();

            for (int i = 0; i < graph.distinctColors; i++)
            {
                if (!visitedColors[i])
                {
                    colors.Enqueue(i);
                    List<int> cluster = new List<int>();

                    long clusterRedTotal = 0, clusterGreenTotal = 0, clusterBlueTotal = 0; //O(1)

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

                        clusterRedTotal += graph.UniqueColors[currentColor].red;              //O(1)
                        clusterGreenTotal += graph.UniqueColors[currentColor].green;              //O(1)
                        clusterBlueTotal += graph.UniqueColors[currentColor].blue;              //O(1)
                    }

                    clusterRedTotal /= cluster.Count;              //O(1)
                    clusterGreenTotal /= cluster.Count;              //O(1)
                    clusterBlueTotal /= cluster.Count;              //O(1)

                    foreach (int color in cluster)
                    {
                        RGBPixel newColor = new RGBPixel((byte)(clusterRedTotal),
                                                        (byte)(clusterGreenTotal),
                                                        (byte)(clusterBlueTotal));              //O(1)

                        map[graph.UniqueColors[color].red,
                            graph.UniqueColors[color].green,
                            graph.UniqueColors[color].blue] = newColor;              //O(1)
                    }
                }
            }
            return map;              //O(1)
        }


        public RGBPixel[,] GetQuantizedImage()
        {
            RGBPixel[,,] map = GeneratePallete();
            int h = ImageOperations.GetHeight(graph.ImageMatrix), w = ImageOperations.GetWidth(graph.ImageMatrix);
            for (int i = 0; i < h; i++) // Complexity = O(body) x #iteration = O(N) x h = O(N^2)
                for (int j = 0; j < w; j++) // Complexity = O(body) x #iteration = O(1) x w = O(w) = O(N)
                    graph.ImageMatrix[i, j] = map[graph.ImageMatrix[i, j].red,
                        graph.ImageMatrix[i, j].green,
                        graph.ImageMatrix[i, j].blue];              //O(1)

            return graph.ImageMatrix;              //O(1)
        }


    }
}
