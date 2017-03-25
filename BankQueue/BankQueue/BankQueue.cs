using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankQueue
{
    class BankQueue
    {
        static void Main(string[] args)
        {
            String input;
            String[] parsedInput;
            Dictionary<int, List<int>> timeAndMoney = new Dictionary<int, List<int>>();
            int N;
            int T;
            int SwedishCrowns;
            int waitTime;
            int longestWait;
            int MAX;
            int result = 0;
            int[] sameWaitTime;

            while (true)
            {
                if (String.IsNullOrWhiteSpace(input = Console.ReadLine()))
                {
                    break;
                }
                else
                {
                    parsedInput = input.Split();
                    N = int.Parse(parsedInput[0]);
                    T = int.Parse(parsedInput[1]);

                    for (int i = 0; i < N; i++)
                    {
                        parsedInput = Console.ReadLine().Split();
                        SwedishCrowns = int.Parse(parsedInput[0]);
                        waitTime = int.Parse(parsedInput[1]);

                        if (!(timeAndMoney.ContainsKey(waitTime)))
                        {
                            timeAndMoney[waitTime] = new List<int>(SwedishCrowns);
                        }
                        else
                        {
                            timeAndMoney[waitTime].Add(SwedishCrowns);
                        }
                    }

                    longestWait = timeAndMoney.Keys.Max();
                    MAX = timeAndMoney.Keys.Max();

                    for (int i = 0; i <= MAX; i++)
                    {
                        if (longestWait != 0)
                        {
                            result += timeAndMoney[longestWait].Max();
                            if (timeAndMoney[longestWait].Count > 1)
                            {
                                timeAndMoney[longestWait].Remove(timeAndMoney[longestWait].Max());
                                sameWaitTime = timeAndMoney[longestWait].ToArray();
                            }
                            else
                            {
                                sameWaitTime = new int[] { 0 };
                            }

                            if (!(timeAndMoney.ContainsKey(longestWait - 1)))
                            {
                                timeAndMoney[longestWait - 1] = sameWaitTime.ToList();
                            }

                            timeAndMoney[longestWait - 1].AddRange(sameWaitTime.ToArray());
                            longestWait--;
                        }
                        else
                        {
                            result += timeAndMoney[0].Max();
                            timeAndMoney.Remove(timeAndMoney[0].Max());
                        }
                    }
                }
            }

            Console.WriteLine(result);
            Console.Read();
        }
    }
}
