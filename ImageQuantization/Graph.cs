using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    class Graph
    {
        private RGBPixel[,] ImageMatrix;
        public HashSet<RGBPixel> UniqueColors;
        public Dictionary<int, RGBPixel> ColorsMap;
        public double[,] ConstructedGraph;

        public Graph(RGBPixel[,] ImageMatrix)
        {
            this.ImageMatrix = ImageMatrix;
            GetUniqueColors();
            MapColors();
            ConstructGraph();
        }

        private void GetUniqueColors()
        {
            UniqueColors = new HashSet<RGBPixel>();
            int rows = ImageMatrix.GetLength(0), columns = ImageMatrix.GetLength(1);

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    UniqueColors.Add(ImageMatrix[i, j]);

        }

        private void MapColors()
        {
            ColorsMap = new Dictionary<int, RGBPixel>();
            int color_index = 0;
            foreach (RGBPixel color in UniqueColors)
            {
                ColorsMap[color_index] = color;
                color_index++;
            }
        }

        private void ConstructGraph()
        {
            int UniqueColorsCount = UniqueColors.Count;
            ConstructedGraph = new double[UniqueColorsCount, UniqueColorsCount];

            for (int i = 0; i < UniqueColorsCount; i++)
                for (int j = 0; j < UniqueColorsCount; j++)
                    ConstructedGraph[i, j] = ImageOperations.CalculateEuclideanDistance(ColorsMap[i], ColorsMap[j]);

        }

    }
}
