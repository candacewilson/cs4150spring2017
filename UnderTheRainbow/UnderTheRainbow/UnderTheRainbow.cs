using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnderTheRainbow
{
    class UnderTheRainbow
    {
        static void Main(string[] args)
        {
            String input;
            int[] distance = null;

            while (true)
            {
                if (String.IsNullOrWhiteSpace(input = Console.ReadLine()))
                {
                    break;
                }
                else
                {
                    distance = new int[int.Parse(input) + 1];

                    for(int i = 0; i <= int.Parse(input); i++)
                    {
                        distance[i] = int.Parse(Console.ReadLine());
                    }
                }
            }

            Console.WriteLine(MinimumPenalty(distance, 0, new Dictionary<int, int>()));
            Console.Read();
        }

        public static int MinimumPenalty(int[] hotels, int start, Dictionary<int, int> cache)
        {
            if(cache.ContainsKey(start))
            {
                return cache[start];
            }

            if(start == hotels.Length - 1)
            {
                return 0;
            }

            double penalty = Double.PositiveInfinity;
            int calculatedPenalty;

            for(int i = hotels.Length - 1; i > start; i--)
            {
                calculatedPenalty = (int)Math.Pow(hotels[i] - hotels[start] - 400, 2) + MinimumPenalty(hotels, i, cache);
                penalty = Math.Min(penalty, calculatedPenalty);
            }

            cache[start] = (int)penalty;
            return (int)penalty;
        }
    }
}
