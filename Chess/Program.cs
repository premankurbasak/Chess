using System;
using System.Linq;

namespace Chess
{
    class Program
    {
        /// <summary>
        /// Main function
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // Chess matrix
            string[,] mat = new string[8, 8];
            int c = 65;
            for (int i = 0; i < 8; i++)
            {
                var ch = Convert.ToChar(c + i);
                for (int j = 0; j < 8; j++)
                {
                    mat[i, j] = ch + (j + 1).ToString();
                }
            }

            var input = Console.ReadLine();
            var split = input.Split(' ');

            switch (split[0])
            {
                case "King":
                    int[] X1 = {-1, 0, 1, -1, 1, -1,  0,  1 };
                    int[] Y1 = { 1, 1, 1,  0, 0, -1, -1, -1 };
                    var t = GetCoordinate(mat, split[1]);
                    Console.WriteLine(PredictionFunc1(mat, t.Item1, t.Item2, X1, Y1));
                    break;
                case "Queen":
                    t = GetCoordinate(mat, split[1]);
                    Console.WriteLine(PredictionFunc2(mat, t.Item1, t.Item2, true, true));
                    break;
                case "Bishop":
                    t = GetCoordinate(mat, split[1]);
                    Console.WriteLine(PredictionFunc2(mat, t.Item1, t.Item2, false, true));
                    break;
                case "Horse":
                    int[] X2 = { 2, 1, -1, -2, -2, -1, 1, 2 };
                    int[] Y2 = { 1, 2, 2, 1, -1, -2, -2, -1 };
                    t = GetCoordinate(mat,split[1]);
                    Console.WriteLine(PredictionFunc1(mat,t.Item1,t.Item2,X2,Y2));
                    break;
                case "Rook":
                    t = GetCoordinate(mat, split[1]);
                    Console.WriteLine(PredictionFunc2(mat, t.Item1, t.Item2, true, false));
                    break;
                case "Pawn":
                    int [] X3 = { 1, 1, 1 };
                    int [] Y3 = { -1, 0, 1};
                    t = GetCoordinate(mat, split[1]);
                    Console.WriteLine(PredictionFunc1(mat, t.Item1, t.Item2, X3, Y3));
                    break;
                default:
                    Console.WriteLine("Wrong Input, Try Again.");
                    break;
            }

            Console.ReadKey();
        }
        /// <summary>
        /// Function to get the co-ordinate
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="matrix"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Tuple<int, int> GetCoordinate<T>(T[,] matrix, T value)
        {
            for (int x = 0; x < 8; ++x)
            {
                for (int y = 0; y < 8; ++y)
                {
                    if (matrix[x, y].Equals(value))
                        return Tuple.Create(x, y);
                }
            }

            return Tuple.Create(-1, -1);
        }
        /// <summary>
        /// Function for prediction 
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        public static string PredictionFunc1(string[,] matrix, int row, int col, int[] X, int[] Y)
        {
            var output = string.Empty;

            for (int i = 0; i < X.Count(); i++)
            {
                int x = row + X[i];
                int y = col + Y[i];

                if (x >= 0 && x <= 7 && y >= 0 && y <= 7)
                {
                    var val = matrix[x, y];
                    output = output+ val + ",";
                }
            }
            return output.Trim(',');
        }
        /// <summary>
        /// Function for prediction 
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="square"></param>
        /// <param name="diagonal"></param>
        /// <returns></returns>
        public static string PredictionFunc2(string[,] matrix, int row, int col, bool square, bool diagonal)
        {
            var output = string.Empty;

            int leftX = 0, rightX = 0, topX = 0, bottomX = 0, topleftX = 0, bottomleftX = 0, toprightX = 0, bottomrightX = 0;
            int leftY = 0, rightY = 0, topY = 0, bottomY = 0, topleftY = 0, bottomleftY = 0, toprightY = 0, bottomrightY = 0;


            for (int i=0;i<8;i++)
            {
                if (square)
                {
                    leftX = row - (i + 1);
                    leftY = col;

                    if (leftX >= 0 && leftX <= 7 && leftY >= 0 && leftY <= 7)
                    {
                        var val = matrix[leftX, leftY];
                        output = output + val + ",";
                    }

                    rightX = row + (i + 1);
                    rightY = col;

                    if (rightX >= 0 && rightX <= 7 && rightY >= 0 && rightY <= 7)
                    {
                        var val = matrix[rightX, rightY];
                        output = output + val + ",";
                    }

                    topX = row ;
                    topY = col + (i + 1);

                    if (topX >= 0 && topX <= 7 && topY >= 0 && topY <= 7)
                    {
                        var val = matrix[topX, topY];
                        output = output + val + ",";
                    }

                    bottomX = row ;
                    bottomY = col - (i + 1);

                    if (bottomX >= 0 && bottomX <= 7 && bottomY >= 0 && bottomY <= 7)
                    {
                        var val = matrix[bottomX, bottomY];
                        output = output + val + ",";
                    }
                }

                if(diagonal)
                {
                    topleftX = row - (i + 1);
                    topleftY = col + (i + 1);

                    if (topleftX >= 0 && topleftX <= 7 && topleftY >= 0 && topleftY <= 7)
                    {
                        var val = matrix[topleftX, topleftY];
                        output = output + val + ",";
                    }

                    bottomrightX = row + (i + 1);
                    bottomrightY = col - (i + 1);

                    if (bottomrightX >= 0 && bottomrightX <= 7 && bottomrightY >= 0 && bottomrightY <= 7)
                    {
                        var val = matrix[bottomrightX, bottomrightY];
                        output = output + val + ",";
                    }

                    toprightX = row + (i + 1);
                    toprightY = col + (i + 1);

                    if (toprightX >= 0 && toprightX <= 7 && toprightY >= 0 && toprightY <= 7)
                    {
                        var val = matrix[toprightX, toprightY];
                        output = output + val + ",";
                    }

                    bottomleftX = row - (i + 1);
                    bottomleftY = col - (i + 1);

                    if (bottomleftX >= 0 && bottomleftX <= 7 && bottomleftY >= 0 && bottomleftY <= 7)
                    {
                        var val = matrix[bottomleftX, bottomleftY];
                        output = output + val + ",";
                    }
                }
            }
            return output.Trim(',');
        }
    }
}
