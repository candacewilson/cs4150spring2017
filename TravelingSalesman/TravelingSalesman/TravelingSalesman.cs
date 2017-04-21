using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelingSalesman
{
    class TravelingSalesman
    {
        static void Main(string[] args)
        {
            String input;
            String[] parsedInput;
            int[,] matrix = null;
            int N = -1;
            int count = 0;

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
                        matrix = new int[N, N];
                        continue;
                    }

                    parsedInput = input.Split();

                    for (int i = 0; i < parsedInput.Length; i++)
                    {
                        matrix[count, i] = int.Parse(parsedInput[i]);
                    }

                    count++;
                }
            }

            Console.WriteLine("*** running genetic algorithm algorithm ***");
            Console.Read();
        }
    }
}
