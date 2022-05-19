using System.Collections.Generic;

namespace ImageQuantization
{

    class Graph
    {
        public int distinctColors = 0;                                 //O(1)
        public RGBPixel[,] ImageMatrix;                                //O(1)
        public RGBPixel[] UniqueColors;                                //O(1)

        public Graph(RGBPixel[,] ImageMatrix)                          //O(1)
        {
            this.ImageMatrix = ImageMatrix;                            //O(1)
            GetUniqueColors();                                         //O(1)
        }
        
        private void GetUniqueColors() // Function overall compexity = O(N^2)
        {
            UniqueColors = new RGBPixel[6000000];                         //O(1)
            bool[,,] visited = new bool[260, 260, 260];                   //O(1)
            int rows = ImageMatrix.GetLength(0);                          //O(1)
            int columns = ImageMatrix.GetLength(1);                       //O(1)
            for (int i = 0; i < rows; i++) // Complexity = O(body) x #iteration = O(columns) x rows = O(N^2)
            {
                for (int j = 0; j < columns; j++) // Complexity = O(body) x #iteration = O(1) x columns = O(columns) = O(N)
                {
                    int red = ImageMatrix[i, j].red;                       //O(1)
                    int green = ImageMatrix[i, j].green;                   //O(1)
                    int blue = ImageMatrix[i, j].blue;                     //O(1)
                    if (!visited[red, green, blue]) // Compexity = O(max body) + O(condition) = O(1) x O(1) = O(1) 
                    {
                        visited[red, green, blue] = true;                  //O(1)
                        UniqueColors[distinctColors] = 
                            ImageMatrix[i, j];                             //O(1)
                        distinctColors++;                                  //O(1)
                    }
                }
            }
        }
    }
}
