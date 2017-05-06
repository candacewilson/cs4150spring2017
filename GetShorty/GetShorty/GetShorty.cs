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
            HashSet<IntersectionAndFactor>[] dungeon;

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

                        dungeon = new HashSet<IntersectionAndFactor>[n];
                        for (int i = 0; i < m; i++)
                        {
                            parsedInput = Console.ReadLine().Split();
                            int x = int.Parse(parsedInput[0]);
                            int y = int.Parse(parsedInput[1]);
                            double factor = double.Parse(parsedInput[2]);

                            if(dungeon[x] == null)
                            {
                                dungeon[x] = new HashSet<IntersectionAndFactor>();
                            }

                            if (dungeon[y] == null)
                            {
                                dungeon[y] = new HashSet<IntersectionAndFactor>();
                            }

                            dungeon[x].Add(new IntersectionAndFactor( y, factor ));
                            dungeon[y].Add(new IntersectionAndFactor( x, factor ));
                        }

                        double[] maxFactor = Enumerable.Repeat(double.Epsilon, n).ToArray();
                        maxFactor[0] = 1;

                        HashSet<IntersectionAndFactor> queue = new HashSet<IntersectionAndFactor>();
                        queue.Add(new IntersectionAndFactor(0, 1));

                        while(queue.Count != 0)
                        {
                            var x = queue.First().Intersection;
                            queue.Remove(new IntersectionAndFactor(queue.First().Intersection, queue.First().Factor));

                            //queue.RemoveWhere(s => s == new IntersectionAndFactor(queue.First().Intersection, queue.First().Factor));

                            foreach (var intersectionCorridor in dungeon[x])
                            {
                                var y = intersectionCorridor.Intersection;
                                var fraction = maxFactor[x] * intersectionCorridor.Factor;

                                if(fraction > maxFactor[y])
                                {
                                    //queue.RemoveWhere(s => s == new IntersectionAndFactor(y, maxFactor[y]));

                                    queue.Remove(new IntersectionAndFactor(y, maxFactor[y]));
                                    queue.Add(new IntersectionAndFactor(y, fraction));
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

        public class IntersectionAndFactor
        {
            private int intersection;
            private double factor;

            public IntersectionAndFactor(int intersection, double factor)
            {
                this.intersection = intersection;
                this.factor = factor;
            }

            public int Intersection
            {
                get { return intersection; }
            }

            public double Factor
            {
                get { return factor; }
            }
        }
    }
}