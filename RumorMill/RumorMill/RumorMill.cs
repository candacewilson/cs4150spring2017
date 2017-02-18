using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumorMill
{
    class RumorMill
    {
        static void Main(string[] args)
        {
            String input;
            Dictionary<String, LinkedList<String>> studentsAndFriends = null;
            String[] rumorStarters = null;
            String[] parsedInput;
            bool gotFriendships = false;
            int initializer = 0;

            while (true)
            {
                if (String.IsNullOrWhiteSpace(input = Console.ReadLine()))
                {
                    break;
                }
                else if (int.TryParse(input, out initializer))
                {
                    if (studentsAndFriends == null)
                    {
                        studentsAndFriends = new Dictionary<String, LinkedList<String>>(initializer);
                        for(int i = 0; i < initializer; i++)
                        {
                            input = Console.ReadLine();
                            studentsAndFriends[input] = new LinkedList<String>();
                        }
                    }
                    else if(!gotFriendships)
                    {
                        gotFriendships = true;
                        for (int i = 0; i < initializer; i++)
                        {
                            input = Console.ReadLine();
                            parsedInput = input.Split();
                            studentsAndFriends[parsedInput[0]].AddLast(parsedInput[1]);
                            studentsAndFriends[parsedInput[1]].AddLast(parsedInput[0]);
                        }
                    }
                    else if (rumorStarters == null)
                    {
                        rumorStarters = new String[initializer];
                        for (int i = 0; i < initializer; i++)
                        {
                            input = Console.ReadLine();
                            rumorStarters[i] = input;
                        }
                    }
                }
            }

            foreach (String gossiper in rumorStarters)
            {
                Console.WriteLine(String.Join(" ", BFS(studentsAndFriends, gossiper)));
            }

            Console.Read();
        }

        private static String[] BFS(Dictionary<String, LinkedList<String>> studentBody, String rumorStarter)
        {
            Dictionary<String, Double> dayHeard = new Dictionary<String, Double>();
            Dictionary<String, String> heardFrom = new Dictionary<String, String>();
            
            foreach(String student in studentBody.Keys)
            {
                dayHeard[student] = Double.PositiveInfinity;
                heardFrom[student] = null;
            }

            dayHeard[rumorStarter] = 0;

            LinkedList<String> Q = new LinkedList<String>();
            Q.AddLast(rumorStarter);

            while(Q.Count > 0)
            {
                String student = Q.First.Value;
                Q.RemoveFirst();

                foreach(String friend in studentBody[student])
                {
                    if(dayHeard[friend] == Double.PositiveInfinity)
                    {
                        Q.AddLast(friend);
                        dayHeard[friend] = dayHeard[student] + 1;
                        heardFrom[friend] = student;
                    }
                }
            }

            String[] sortedNames = new String[studentBody.Keys.Count];
            int count = 0;

            foreach (var student in dayHeard.OrderBy(pair => pair.Value).ThenBy(pair => pair.Key))
            {
                sortedNames[count] = student.Key;
                count++;
            }

            return sortedNames;
        }
    }
}