using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpidermansWorkout
{
    class SpidermansWorkout
    {
        static void Main(string[] args)
        {
            String input;
            String[] parsedInput;
            int[] distances = null;
            String[] results = null;
            int N = -1;
            int M, count = 0;

            while (true)
            {
                if (String.IsNullOrWhiteSpace(input = Console.ReadLine()))
                {
                    break;
                }
                else
                {
                    if (N == -1)
                    {
                        N = int.Parse(input);
                        results = new String[N];
                        continue;
                    }

                    M = int.Parse(input);
                    distances = new int[(M+1)];
                    parsedInput = Console.ReadLine().Split();

                    for (int i = 1; i <= M; i++)
                    {
                        distances[i] = int.Parse(parsedInput[(i-1)]);
                    }

                    results[count] = WerkIt(distances, M);
                    count++;
                }
            }

            foreach(String result in results)
            {
                Console.WriteLine(result);
            }

            Console.Read();
        }

        public static String WerkIt(int[] distances, int M)
        {
            int[,] height = new int[41, 1001];
            int[,] direction = new int[41, 1001];
            int climbUp, climbDown;

            for(int i = 0; i < 41; i++)
            {
                for(int j = 0; j < 1001; j++)
                {
                    height[i, j] = -1;
                }
            }

            height[0, 0] = 0;

            for(int i = 1; i <= M; i++)
            {
                for(int j = 0; j <= (1000 - distances[i]); j++)
                {
                    climbUp = j + distances[i];
                    climbDown = j - distances[i];

                    if(climbDown >= 0)
                    {
                        if(height[(i-1), climbUp] != -1 && height[(i-1), climbDown] != -1)
                        {
                            height[i, j] = Math.Min(height[(i-1), climbUp], Math.Max(j, height[(i-1), climbDown]));
                            direction[i, j] = (height[(i-1), climbUp] > Math.Max(j, height[(i-1), climbDown]) ? 1 : -1);
                        }
                        else if (height[(i-1), climbUp] != -1)
                        {
                            height[i, j] = height[(i-1), climbUp];
                            direction[i, j] = -1;
                        }
                        else if (height[(i-1), climbDown] != -1)
                        {
                            height[i, j] = Math.Max(j, height[(i-1), climbDown]);
                            direction[i, j] = 1;
                        }
                    }
                    else if (height[(i-1), climbUp] != -1)
                    {
                        height[i, j] = height[(i-1), climbUp];
                        direction[i, j] = -1;
                    }
                }
            }

            int[] legalSolution = new int[(M+1)];
            int position = 0;

            for(int i = M; i >= 1; i--)
            {
                legalSolution[i] = direction[i, position];
                position = position - direction[i, position] * distances[i];
            }

            String result = "";

            if(height[M, 0] != -1)
            {
                for(int i = 1; i <= M; i++)
                {
                    result += (legalSolution[i] == 1) ? "U" : "D";
                }
                return result;
            }

            return "IMPOSSIBLE";
        }
    }
}