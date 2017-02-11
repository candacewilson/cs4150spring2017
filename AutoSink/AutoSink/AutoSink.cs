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
            String[] proposedTrips = null;
            String[] parsedInput;
            int initializer = 0;
            int count = 0;
            int numOfCities = 0;
            int numOfHighways = 0;
            int numOfProposedTrips = 0;

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
                        if (1 <= initializer && initializer <= 2000)
                        {
                            cities = new Dictionary<String, int>(initializer);
                            numOfCities = initializer;
                        }
                    }
                    else if (highways == null)
                    {
                        if (0 <= initializer && initializer <= 10000)
                        {
                            highways = new Dictionary<String, List<String>>(initializer);
                            numOfHighways = initializer;
                        }
                    }
                    else if (proposedTrips == null)
                    {
                        if (1 <= initializer && initializer <= 8000)
                        {
                            proposedTrips = new String[initializer];
                            numOfProposedTrips = initializer;
                        }
                    }
                }
                else
                {
                    if (cities != null && highways == null && proposedTrips == null && cities.Count <= numOfCities)
                    {
                        parsedInput = input.Split();
                        cities.Add(parsedInput[0], int.Parse(parsedInput[1]));
                    }
                    else if (cities != null && highways != null && proposedTrips == null && highways.Count <= numOfHighways)
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
                    else if (cities != null && highways != null && proposedTrips != null && proposedTrips.Length <= numOfProposedTrips)
                    {
                        proposedTrips[count] = input;
                        count++;
                    }
                }
            }

            DAG dagonia = new DAG();
            Dictionary<String, int> tolls = null;

            foreach (String city in cities.Keys)
            {
                tolls = new Dictionary<String, int>();
                if (highways.Keys.Contains(city))
                {
                    foreach (String destination in highways[city])
                    {
                        tolls.Add(destination, cities[destination]);
                    }
                }
                dagonia.connectedHighways(city, tolls);
            }

            List<List<String>> paths = new List<List<String>>();
            foreach (String trip in proposedTrips)
            {
                parsedInput = trip.Split();
                paths.Add(dagonia.Dijkstras(parsedInput[0], parsedInput[1]));
            }

            int cost;

            for (int i = 0; i < paths.Count; i++)
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
                    foreach (String city in paths[i])
                    {
                        cost += cities[city];
                    }
                    Console.WriteLine(cost);
                }
            }

            Console.Read();
        }

        private class DAG
        {
            private Dictionary<String, Dictionary<String, int>> cityAndHighways = new Dictionary<String, Dictionary<String, int>>();

            public void connectedHighways(String city, Dictionary<String, int> highways)
            {
                cityAndHighways[city] = highways;
            }

            public List<String> Dijkstras(String departureCity, String destinationCity)
            {
                Dictionary<String, String> cheapestVisited = new Dictionary<String, String>();
                Dictionary<String, int> allTolls = new Dictionary<String, int>();
                List<String> cities = new List<String>();
                List<String> trip = new List<String>();
                String cheapestCity;
                int cityToll;

                foreach (var city in cityAndHighways)
                {
                    if (city.Key == departureCity)
                    {
                        allTolls[city.Key] = 0;
                    }
                    else
                    {
                        allTolls[city.Key] = int.MaxValue;
                    }

                    cities.Add(city.Key);
                }

                while (cities.Count != 0)
                {
                    cities.Sort((x, y) => allTolls[x] - allTolls[y]);

                    cheapestCity = cities[0];
                    cities.Remove(cheapestCity);

                    if (cheapestCity == destinationCity)
                    {
                        trip = new List<String>();
                        while (cheapestVisited.ContainsKey(cheapestCity))
                        {
                            trip.Add(cheapestCity);
                            cheapestCity = cheapestVisited[cheapestCity];
                        }

                        break;
                    }

                    if (allTolls[cheapestCity] == int.MaxValue)
                    {
                        break;
                    }

                    foreach (var city in cityAndHighways[cheapestCity])
                    {
                        cityToll = allTolls[cheapestCity] + city.Value;

                        if (cityToll < allTolls[city.Key])
                        {
                            allTolls[city.Key] = cityToll;
                            cheapestVisited[city.Key] = cheapestCity;
                        }
                    }
                }

                return trip;
            }
        }
    }
}