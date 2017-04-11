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
            int N = -1;
            int M;

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
                    }

                    M = int.Parse(Console.ReadLine());

                    distances = new int[M];

                    for(int i = 0; i < M; i++)
                    {
                        distances[i] = int.Parse(Console.ReadLine());
                    }

                    Console.WriteLine(WerkIt(distances));
                }
            }

            Console.Read();
        }

        public static String WerkIt(int[] distances)
        {
            String result = "";
            int current = 0;
            int total = 0;

            for(int i = 0; i < distances.Length; i++)
            {
                current = distances[i];

                if(total - current >= 0)
                {
                    result += "D";
                    total -= current;
                }
                else
                {
                    result += "U";
                    total += current;
                }
            }

            if(total != 0)
            {
                return "IMPOSSIBLE";
            }

            return result;
        }
    }
}
