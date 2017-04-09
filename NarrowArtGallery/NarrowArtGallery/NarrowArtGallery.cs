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

        public static int maxValue(int r, int unclosableRoom, int k, int[,] values)
        {
            int N = values.Length / 2; // Length returns ALL elements in values (2N)
            int A, B, C = 0;

            if(r == N)
            {
                if(unclosableRoom == 0)
                {
                    return values[r-1, 0];
                }

                if (unclosableRoom == 1)
                {
                    return values[r-1, 1];
                }

                if (unclosableRoom == -1)
                {
                    return values[r-1, 0] + values[r-1, 1];
                }
            }

            if(k == N - r)
            {
                A = values[r, 0] + maxValue(r + 1, 0, k - 1, values);
                B = values[r, 1] + maxValue(r + 1, 1, k - 1, values);

                if (unclosableRoom == 0)
                {
                    return A;
                }

                if (unclosableRoom == 1)
                {
                    return B;
                }

                if (unclosableRoom == -1)
                {
                    return Math.Max(A, B); 
                }
            }

            if(k < N - r)
            {
                A = values[r, 0] + maxValue(r + 1, 0, k - 1, values);
                B = values[r, 1] + maxValue(r + 1, 1, k - 1, values);
                C = values[r, 0] + values[r, 1] + maxValue(r + 1, -1, k, values);

                if (unclosableRoom == -1)
                {
                    return Math.Max(A, Math.Max(B, C));
                }

                if (unclosableRoom == 0)
                {
                    return values[r, 0] + maxValue(r + 1, 0, k-1, values);
                }

                if (unclosableRoom == 1)
                {
                    return values[r, 1] + maxValue(r + 1, 1, k-1, values);
                }
            }

            return 0;
        }
    }
}
