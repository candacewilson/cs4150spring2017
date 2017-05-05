using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetShorty
{
    class GetShorty
    {
        static void Main(string[] args)
        {
            String input;
            String[] parsedInput;
            List<double> results = new List<double>();
            int n;
            int m;
            Dictionary<int, List<KeyValuePair<int, double>>> dungeon;

            while (true)
            {
                if (String.IsNullOrWhiteSpace(input = Console.ReadLine()))
                {
                    break;
                }
                else
                {
                    parsedInput = input.Split();

                    if (parsedInput.Count() == 2)
                    {
                        n = int.Parse(parsedInput[0]);
                        m = int.Parse(parsedInput[1]);

                        if (n == 0 && m == 0)
                        {
                            break;
                        }

                        dungeon = new Dictionary<int, List<KeyValuePair<int, double>>>(n);
                        for (int i = 0; i < m; i++)
                        {
                            parsedInput = Console.ReadLine().Split();
                            int x = int.Parse(parsedInput[0]);
                            int y = int.Parse(parsedInput[1]);
                            double factor = double.Parse(parsedInput[2]);

                            if(!(dungeon.ContainsKey(x)))
                            {
                                dungeon.Add(x, new List<KeyValuePair<int, double>>());
                            }

                            if (!(dungeon.ContainsKey(y)))
                            {
                                dungeon.Add(y, new List<KeyValuePair<int, double>>());
                            }

                            dungeon[x].Add(new KeyValuePair<int, double> ( y, factor ));
                            dungeon[y].Add(new KeyValuePair<int, double> ( x, factor ));
                        }

                        double[] maxFactor = Enumerable.Repeat(double.Epsilon, n).ToArray();
                        maxFactor[0] = 1;

                        HashSet<KeyValuePair<int, double>> queue = new HashSet<KeyValuePair<int, double>>();
                        queue.Add(new KeyValuePair<int, double>(0, 1));

                        while(queue.Count != 0)
                        {
                            var x = queue.First().Key;
                            queue.Remove(new KeyValuePair<int, double>(queue.First().Key, queue.First().Value));

                            foreach(var intersectionCorridor in dungeon[x])
                            {
                                var y = intersectionCorridor.Key;
                                var fraction = maxFactor[x] * intersectionCorridor.Value;

                                if(fraction > maxFactor[y])
                                {
                                    queue.Remove(new KeyValuePair<int, double>(y, maxFactor[y]));
                                    queue.Add(new KeyValuePair<int, double>(y, fraction));
                                    maxFactor[y] = fraction;
                                }
                            }
                        }

                        results.Add(maxFactor[n-1]);
                    }
                }
            }

            foreach (double value in results)
            {
                Console.WriteLine(Math.Round((Decimal)value, 4, MidpointRounding.AwayFromZero).ToString("n4"));
            }

            Console.Read();
        }
    }
}