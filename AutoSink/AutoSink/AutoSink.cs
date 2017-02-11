using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSink
{
    class AutoSink
    {
        static void Main(string[] args)
        {
            String input;
            Dictionary<String, int> cities = null;
            Dictionary<String, List<String>> highways = null;
            List<String> proposedTrips = null;
            String[] parsedInput;
            int initializer = 0;
            List<String> results = null;

            while (true)
            {
                if (String.IsNullOrWhiteSpace(input = Console.ReadLine()))
                {
                    break;
                }
                else if (int.TryParse(input, out initializer))
                {
                    if (cities == null)
                    {
                        cities = new Dictionary<String, int>(initializer);
                    }
                    else if (highways == null)
                    {
                        highways = new Dictionary<String, List<String>>(initializer);
                    }
                    else if (proposedTrips == null)
                    {
                        proposedTrips = new List<String>(initializer);
                        results = new List<String>(initializer);
                    }
                }
                else
                {
                    if (cities != null && highways == null && proposedTrips == null)
                    {
                        parsedInput = input.Split();
                        cities.Add(parsedInput[0], int.Parse(parsedInput[1]));
                    }
                    else if (cities != null && highways != null && proposedTrips == null)
                    {
                        parsedInput = input.Split();
                        if (highways.ContainsKey(parsedInput[0]))
                        {
                            highways[parsedInput[0]].Add(parsedInput[1]);
                        }
                        else
                        {
                            highways.Add(parsedInput[0], new List<String> { parsedInput[1] });
                        }
                    }
                    else if (cities != null && highways != null && proposedTrips != null)
                    {
                        proposedTrips.Add(input);
                    }
                }
            }

            DAG dagonia = new DAG();
            Dictionary<String, int> tolls = null;

            foreach (String city in highways.Keys)
            {
                tolls = new Dictionary<String, int>();
                foreach (String destination in highways[city])
                {
                    tolls.Add(destination, cities[destination]);
                }
                dagonia.add(city, tolls);
            }

            List<List<String>> paths = null;
            foreach (String trip in proposedTrips)
            {
                parsedInput = trip.Split();
                paths.Add(dagonia.Dijkstras(parsedInput[0], parsedInput[1]));
            }

            int cost;

            for(int i = 0; i < paths.Count; i++)
            {
                cost = 0;
                parsedInput = proposedTrips[i].Split();
                if (paths[i].Count == 0)
                {
                    if (parsedInput[0].Equals(parsedInput[1]))
                    {
                        Console.WriteLine(cost);
                    }
                    else
                    {
                        Console.WriteLine("NO");
                    }
                }
                else
                {
                    foreach(String city in paths[i])
                    {
                        cost += cities[city];
                    }
                    Console.WriteLine(cost);
                }
            }

            //DAG g = new DAG();
            //g.add("A", new Dictionary<String, int>() { { "B", 7 }, { "C", 8 } });
            //g.add("B", new Dictionary<String, int>() { { "F", 2 } });
            //g.add("C", new Dictionary<String, int>() { { "F", 6 }, { "G", 4 } });
            //g.add("D", new Dictionary<String, int>() { { "F", 8 } });
            //g.add("E", new Dictionary<String, int>() { { "H", 1 } });
            //g.add("F", new Dictionary<String, int>() { { "G", 9 }, { "H", 3 } });
            //g.add("G", new Dictionary<String, int>() { { "H", 9 } });
            //g.add("H", new Dictionary<String, int>());

            //g.Dijkstras("A", "A").ForEach(x => Console.WriteLine(x));

            Console.Read();

        }







        private class DAG
        {
            Dictionary<String, Dictionary<String, int>> cityAndHighways = new Dictionary<String, Dictionary<String, int>>();

            public void add(String city, Dictionary<String, int> highways)
            {
                cityAndHighways[city] = highways;
            }

            public List<String> Dijkstras(String firstCity, String secondCity)
            {
                var previous = new Dictionary<String, String>();
                var distances = new Dictionary<String, int>();
                var nodes = new List<String>();

                List<String> path = null;

                foreach (var city in cityAndHighways)
                {
                    if (city.Key == firstCity)
                    {
                        distances[city.Key] = 0;
                    }
                    else
                    {
                        distances[city.Key] = int.MaxValue;
                    }

                    nodes.Add(city.Key);
                }

                while (nodes.Count != 0)
                {
                    nodes.Sort((x, y) => distances[x] - distances[y]);

                    var smallest = nodes[0];
                    nodes.Remove(smallest);

                    if (smallest == secondCity)
                    {
                        path = new List<String>();
                        while (previous.ContainsKey(smallest))
                        {
                            path.Add(smallest);
                            smallest = previous[smallest];
                        }

                        break;
                    }

                    if (distances[smallest] == int.MaxValue)
                    {
                        break;
                    }

                    foreach (var neighbor in cityAndHighways[smallest])
                    {
                        var alt = distances[smallest] + neighbor.Value;
                        if (alt < distances[neighbor.Key])
                        {
                            distances[neighbor.Key] = alt;
                            previous[neighbor.Key] = smallest;
                        }
                    }
                }

                return path;
            }
        }
    }
}
