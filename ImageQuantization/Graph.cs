using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    
    class Graph
    {
        public int disCOlors = 0;
        private RGBPixel[,] ImageMatrix;
        //public HashSet<RGBPixel> UniqueColors;
        RGBPixel[] UniqueColors;
        public Dictionary<int, RGBPixel> ColorsMap;
        //public double[,] ConstructedGraph;

        public Graph(RGBPixel[,] ImageMatrix)
        {
            this.ImageMatrix = ImageMatrix;
            GetUniqueColors();
            MapColors();
            //ConstructGraph();
        }

        private void GetUniqueColors()
        {
            UniqueColors = new RGBPixel[60000];
            bool[,,] vis = new bool[260, 260, 260];
            for (int i = 0; i < 260; i++)
                for (int j = 0; j < 260; j++)
                    for (int k = 0; k < 260; k++)
                        vis[i, j, k] = false;
            int rows = ImageMatrix.GetLength(0), columns = ImageMatrix.GetLength(1);
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                {
                    int r = ImageMatrix[i, j].red, g = ImageMatrix[i, j].green, b = ImageMatrix[i, j].blue;
                    if(!vis[r,g,b])
                    {
                        vis[r, g, b] = true;
                        UniqueColors[disCOlors++] = ImageMatrix[i, j];
                    }

                }

            Console.WriteLine(disCOlors);

        }

        private void MapColors()
        {
            ColorsMap = new Dictionary<int, RGBPixel>();
            int color_index = 0;
            for (int i = 0; i < disCOlors; i++)
            {
                ColorsMap[color_index] = UniqueColors[i];
                color_index++;
            }
        }

        //private void ConstructGraph()
        //{



        //    //int UniqueColorsCount = UniqueColors.Count;
        //    //ConstructedGraph = new double[UniqueColorsCount, UniqueColorsCount];

        //    //for (int i = 0; i < UniqueColorsCount; i++)
        //    //    for (int j = 0; j < UniqueColorsCount; j++)
        //    //        ConstructedGraph[i, j] = ImageOperations.CalculateEuclideanDistance(ColorsMap[i], ColorsMap[j]);

        //}

    }
}
