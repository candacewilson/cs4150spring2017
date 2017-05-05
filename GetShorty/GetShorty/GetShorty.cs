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
            List<KeyValuePair<int, double>>[] graph;;

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

                        graph = new List<KeyValuePair<int, double>>[n];
                        for (int i = 0; i < m; i++)
                        {
                            parsedInput = Console.ReadLine().Split();
                            int x = int.Parse(parsedInput[0]);
                            int y = int.Parse(parsedInput[1]);
                            double factor = double.Parse(parsedInput[2]);

                            if(graph[x] == null)
                            {
                                graph[x] = new List<KeyValuePair<int, double>>();
                            }

                            if(graph[y] == null)
                            {
                                graph[y] = new List<KeyValuePair<int, double>>();
                            }

                            graph[x].Add(new KeyValuePair<int, double> ( y, factor ));
                            graph[y].Add(new KeyValuePair<int, double> ( x, factor ));

                        }

                        List<double> maxSizes = Enumerable.Repeat(double.Epsilon, n).ToList();
                        maxSizes[0] = 1;

                        IntersectionCorridorComparer comparison = new IntersectionCorridorComparer();

                        SortedSet<KeyValuePair<int, double>> q = new SortedSet<KeyValuePair<int, double>>(comparison);

                        q.Add(new KeyValuePair<int, double>(0, 1));

                        while(q.Count != 0)
                        {
                            var u = q.First().Key;
                            q.Remove(new KeyValuePair<int, double>(q.First().Key, q.First().Value));

                            foreach(var p in graph[u])
                            {
                                var v = p.Key;
                                var fraction = maxSizes[u] * p.Value;

                                if(fraction > maxSizes[v])
                                {
                                    q.Remove(new KeyValuePair<int, double>(v, maxSizes[v]));
                                    q.Add(new KeyValuePair<int, double>(v, fraction));
                                    maxSizes[v] = fraction;
                                }
                            }
                        }

                        results.Add(maxSizes[n-1]);
                    }
                }
            }

            foreach (double value in results)
            {
                Console.WriteLine(value);
            }

            Console.Read();
        } 

        public class IntersectionCorridorComparer : IComparer<KeyValuePair<int, double>>
        {
            public int Compare(KeyValuePair<int, double> x, KeyValuePair<int, double> y)
            {
                return Convert.ToInt32(x.Value > y.Value || (x.Value == y.Value && x.Key > y.Key));
            }
        }















        //static void Main(string[] args)
        //{
        //    String input;
        //    String[] parsedInput;
        //    List<String> results = new List<String>();
        //    Dungeon dungeon = new Dungeon();
        //    Dictionary<int, Intersection> intersections = new Dictionary<int, Intersection>();
        //    int n;
        //    int m;

        //    while (true)
        //    {
        //        if (String.IsNullOrWhiteSpace(input = Console.ReadLine()))
        //        {
        //            break;
        //        }
        //        else
        //        {
        //            parsedInput = input.Split();

        //            if (parsedInput.Count() == 2)
        //            {
        //                n = int.Parse(parsedInput[0]);
        //                m = int.Parse(parsedInput[1]);

        //                if (n == 0 && m == 0)
        //                {
        //                    break;
        //                }

        //                for (int i = 0; i < n; i++)
        //                {
        //                    Intersection newIntersection = new Intersection(i);
        //                    intersections[i] = newIntersection;
        //                }

        //                for (int i = 0; i < m; i++)
        //                {
        //                    input = Console.ReadLine();
        //                    parsedInput = input.Split();
        //                    intersections[int.Parse(parsedInput[0])].addCorridor(intersections, int.Parse(parsedInput[0]), int.Parse(parsedInput[1]), float.Parse(parsedInput[2]));
        //                }

        //                foreach (int key in intersections.Keys)
        //                {
        //                    dungeon.addIntersection(key, intersections[key].GetAllCorridors());
        //                }

        //                List<int> Path = dungeon.Dijkstras(0, n-1);
        //                int startingIntersection = 0;
        //                float totalFactor = 1.0000f;

        //                foreach (int corridor in Path)
        //                {
        //                    Dictionary<int, float> corridors = intersections[startingIntersection].GetAllCorridors();
        //                    totalFactor = totalFactor * corridors[corridor];
        //                    startingIntersection = corridor;
        //                }

        //                Math.Round((Decimal)totalFactor, 4, MidpointRounding.AwayFromZero);
        //                results.Add(Math.Round((Decimal)totalFactor, 4, MidpointRounding.AwayFromZero).ToString("n4"));
        //            }
        //        }
        //    }

        //    foreach (String value in results)
        //    {
        //        Console.WriteLine(value);
        //    }

        //    Console.Read();
        //}

        //public class Intersection
        //{
        //    private int intersection;
        //    private Dictionary<int, float> corridors = new Dictionary<int, float>();

        //    public Intersection(int intersection)
        //    {
        //        this.intersection = intersection;
        //    }

        //    public void addCorridor(Dictionary<int, Intersection> intersections, int source, int target, float factor)
        //    {
        //        if (corridors.ContainsKey(target) || intersections[target].corridors.ContainsKey(source))
        //        {
        //            if (corridors[target] < factor)
        //            {
        //                corridors[target] = factor;
        //                intersections[target].corridors[source] = factor;
        //            }

        //            if (intersections[target].corridors[source] < factor)
        //            {
        //                intersections[target].corridors[source] = factor;
        //            }
        //        }

        //        corridors[target] = factor;
        //        intersections[target].corridors[source] = factor;
        //    }

        //    public Dictionary<int, float> GetAllCorridors()
        //    {
        //        return corridors;
        //    }
        //}

        //public class Dungeon
        //{
        //    Dictionary<int, Dictionary<int, float>> intersectionsAndCorridors = new Dictionary<int, Dictionary<int, float>>();

        //    public void addIntersection(int intersection, Dictionary<int, float> corridors)
        //    {
        //        intersectionsAndCorridors[intersection] = corridors;
        //    }

        //    public List<int> Dijkstras(int sourceIntersection, int targetIntersection)
        //    {
        //        Dictionary<int, int> prev = new Dictionary<int, int>();
        //        Dictionary<int, float> factors = new Dictionary<int, float>();
        //        List<int> intersections = new List<int>();

        //        List<int> path = new List<int>(); 

        //        foreach (var intersection in intersectionsAndCorridors)
        //        {
        //            if (intersection.Key == sourceIntersection)
        //            {
        //                factors[intersection.Key] = 1;
        //            }
        //            else
        //            {
        //                factors[intersection.Key] = float.MinValue;
        //            }

        //            intersections.Add(intersection.Key);
        //        }

        //        while (intersections.Count != 0)
        //        {
        //            intersections.OrderBy(a => a).ToArray();

        //            int currentIntersection = intersections[0];
        //            intersections.Remove(currentIntersection);

        //            if (currentIntersection == targetIntersection)
        //            {
        //                while (prev.ContainsKey(currentIntersection))
        //                {
        //                    path.Add(currentIntersection);
        //                    currentIntersection = prev[currentIntersection];
        //                }

        //                break;
        //            }

        //            if (factors[currentIntersection] == float.MinValue)
        //            {
        //                break;
        //            }

        //            foreach (var neighbor in intersectionsAndCorridors[currentIntersection])
        //            {
        //                var alt = factors[currentIntersection] * neighbor.Value;
        //                if (alt > factors[neighbor.Key])
        //                {
        //                    factors[neighbor.Key] = alt;
        //                    prev[neighbor.Key] = currentIntersection;
        //                }
        //            }
        //        }

        //        path.Reverse();
        //        return path;
        //    }
        //}
    }
}