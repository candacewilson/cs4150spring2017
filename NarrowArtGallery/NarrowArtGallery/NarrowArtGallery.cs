using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NarrowArtGallery
{
    class NarrowArtGallery
    {
        static void Main(string[] args)
        {
            String input;
            String[] parsedInput;
            int[,] values = null;
            int N = 0;
            int k = 0;

            while (true)
            {
                if (String.IsNullOrWhiteSpace(input = Console.ReadLine()))
                {
                    break;
                }
                else
                {
                    parsedInput = input.Split();
                    N = int.Parse(parsedInput[0]);
                    k = int.Parse(parsedInput[1]);
                    values = new int[N, 2];
                    
                    for(int i = 0; i < N; i++)
                    {
                        parsedInput = Console.ReadLine().Split();

                        values[i, 0] = int.Parse(parsedInput[0]);
                        values[i, 1] = int.Parse(parsedInput[1]);
                    }

                    break;
                }
            }

            Console.WriteLine(maxValue(0, -1, k, values));
            Console.Read();
        }

        /// <summary>
        /// 
        /// Requires the existence of an N x 2 values array.
        /// Requires that k ≤ N - r.
        /// Requires that 0 ≤ r ≤ N
        /// Requires that uncloseableRoom = -1, 0, or 1
        /// Returns the maximum value that can be obtained from rows r through N-1
        /// when k rooms are closed, subject to this restriction:
        ///     If uncloseableRoom is 0, the room in column 0 of row r cannot be closed;
        ///     If uncloseableRoom is 1, the room in column 1 of row r cannot be closed;
        ///     If uncloseableRoom is -1, either room of row i may be closed if desired.
        /// 
        /// </summary>
        /// <param name="r"> Current Row </param>
        /// <param name="unclosableRoom"> Room must be open </param>
        /// <param name="k"> Rooms to close </param>
        /// <returns></returns>
        public static int maxValue(int r, int unclosableRoom, int k, int[,] values)
        {
            int N = values.Length / 2;

            if(k == N - r)
            {
                if (unclosableRoom == -1)
                {
                    // # 3 & 4
                }
                else if (unclosableRoom == 0)
                {
                    return values[r, 0] + maxValue(r+1, 0, k-1, values);
                }
                else if (unclosableRoom == 1)
                {
                    return values[r, 1] + maxValue(r+1, 1, k-1, values);
                }
            }

            if(k < N - r)
            {
                if (unclosableRoom == -1)
                {

                }
                else if (unclosableRoom == 0)
                {

                }
                else if (unclosableRoom == 1)
                {

                }
            }
        }
    }
}
