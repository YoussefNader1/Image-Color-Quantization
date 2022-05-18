using System.Collections.Generic;

namespace ImageQuantization
{

    class Graph
    {
        public int distinctCOlors = 0;
        private RGBPixel[,] ImageMatrix;
        RGBPixel[] UniqueColors;
        public Dictionary<int, RGBPixel> ColorsMap;

        public Graph(RGBPixel[,] ImageMatrix)
        {
            this.ImageMatrix = ImageMatrix;
            GetUniqueColors();
            MapColors();
        }

        private void GetUniqueColors()
        {
            UniqueColors = new RGBPixel[60000];
            bool[,,] visited = new bool[260, 260, 260];
            for (int i = 0; i < 260; i++)
                for (int j = 0; j < 260; j++)
                    for (int k = 0; k < 260; k++)
                        visited[i, j, k] = false;
            int rows = ImageMatrix.GetLength(0), columns = ImageMatrix.GetLength(1);
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                {
                    int red = ImageMatrix[i, j].red;
                    int green = ImageMatrix[i, j].green;
                    int blue = ImageMatrix[i, j].blue;
                    if(!visited[red,green,blue])
                    {
                        visited[red, green, blue] = true;
                        UniqueColors[distinctCOlors] = ImageMatrix[i, j];
                        distinctCOlors++;
                    }
                }
        }

        private void MapColors()
        {
            ColorsMap = new Dictionary<int, RGBPixel>();
            int color_index = 0;
            for (int i = 0; i < distinctCOlors; i++)
            {
                ColorsMap[color_index] = UniqueColors[i];
                color_index++;
            }
        }
    }
}
