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
            List<String> results = new List<String>();
            Dungeon dungeon = new Dungeon();
            Dictionary<String, Intersection> intersections = new Dictionary<String, Intersection>();
            int n;
            int m;

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

                        for (int i = 0; i < n; i++)
                        {
                            Intersection newIntersection = new Intersection(i);
                            intersections[i.ToString()] = newIntersection;
                        }

                        for (int i = 0; i < m; i++)
                        {
                            input = Console.ReadLine();
                            parsedInput = input.Split();
                            intersections[parsedInput[0]].addCorridor(intersections, parsedInput[0], parsedInput[1], float.Parse(parsedInput[2]));
                        }

                        foreach (String key in intersections.Keys)
                        {
                            dungeon.addIntersection(key, intersections[key].GetAllCorridors());
                        }

                        List<String> Path = dungeon.Dijkstras("0", (n - 1).ToString());
                        String startingIntersection = "0";
                        float totalFactor = 1.0000f;

                        foreach (String corridor in Path)
                        {
                            Dictionary<String, float> corridors = intersections[startingIntersection].GetAllCorridors();
                            totalFactor = totalFactor * corridors[corridor];
                            startingIntersection = corridor;
                        }

                        Math.Round((Decimal)totalFactor, 4, MidpointRounding.AwayFromZero);
                        results.Add(Math.Round((Decimal)totalFactor, 4, MidpointRounding.AwayFromZero).ToString("n4"));
                    }
                }
            }

            foreach (String value in results)
            {
                Console.WriteLine(value);
            }

            Console.Read();
        }

        public class Intersection
        {
            private int intersection;
            private Dictionary<String, float> corridors = new Dictionary<String, float>();

            public Intersection(int intersection)
            {
                this.intersection = intersection;
            }

            public void addCorridor(Dictionary<String, Intersection> intersections, String source, String target, float factor)
            {
                if (corridors.ContainsKey(target) || intersections[target].corridors.ContainsKey(source))
                {
                    if (corridors[target] < factor)
                    {
                        corridors[target] = factor;
                        intersections[target].corridors[source] = factor;
                    }

                    if (intersections[target].corridors[source] < factor)
                    {
                        intersections[target].corridors[source] = factor;
                    }
                }

                corridors[target] = factor;
                intersections[target].corridors[source] = factor;
            }

            public Dictionary<String, float> GetAllCorridors()
            {
                return corridors;
            }
        }

        public class Dungeon
        {
            Dictionary<String, Dictionary<String, float>> intersectionsAndCorridors = new Dictionary<String, Dictionary<String, float>>();

            public void addIntersection(String intersection, Dictionary<String, float> corridors)
            {
                intersectionsAndCorridors[intersection] = corridors;
            }

            public List<String> Dijkstras(String sourceIntersection, String targetIntersection)
            {
                var prev = new Dictionary<String, String>();
                var factors = new Dictionary<String, float>();
                var intersections = new List<String>();

                List<String> path = null;

                foreach (var intersection in intersectionsAndCorridors)
                {
                    if (intersection.Key == sourceIntersection)
                    {
                        factors[intersection.Key] = 1;
                    }
                    else
                    {
                        factors[intersection.Key] = float.MinValue;
                    }

                    intersections.Add(intersection.Key);
                }

                while (intersections.Count != 0)
                {
                    intersections.OrderBy(a => a).ToArray();

                    var nextIntersection = intersections[0];
                    intersections.Remove(nextIntersection);

                    if (nextIntersection == targetIntersection)
                    {
                        path = new List<String>();
                        while (prev.ContainsKey(nextIntersection))
                        {
                            path.Add(nextIntersection);
                            nextIntersection = prev[nextIntersection];
                        }

                        break;
                    }

                    if (factors[nextIntersection] == float.MinValue)
                    {
                        break;
                    }

                    foreach (var neighbor in intersectionsAndCorridors[nextIntersection])
                    {
                        var alt = factors[nextIntersection] * neighbor.Value;
                        if (alt > factors[neighbor.Key])
                        {
                            factors[neighbor.Key] = alt;
                            prev[neighbor.Key] = nextIntersection;
                        }
                    }
                }

                path.Reverse();
                return path;
            }
        }
    }
}