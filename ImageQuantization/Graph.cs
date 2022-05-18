using System.Collections.Generic;

namespace ImageQuantization
{

    class Graph
    {
        public int distinctColors = 0;
        private RGBPixel[,] ImageMatrix;
        public RGBPixel[] UniqueColors;

        public Graph(RGBPixel[,] ImageMatrix)
        {
            this.ImageMatrix = ImageMatrix;
            GetUniqueColors();
        }

        private void GetUniqueColors()
        {
            UniqueColors = new RGBPixel[60000];
            bool[,,] visited = new bool[260, 260, 260];

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
                        UniqueColors[distinctColors] = ImageMatrix[i, j];
                        distinctColors++;
                    }
                }
        }
    }
}
