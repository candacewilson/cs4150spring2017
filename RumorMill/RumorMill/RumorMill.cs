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
            int count = 0;
            int numOfStudents = 0;
            int numOfFriendships = 0;
            int studentCount = 0;
            int friendshipCount = 0;

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
                        numOfStudents = initializer;
                    }
                    else if(!gotFriendships && numOfStudents == studentCount)
                    {
                        gotFriendships = true;
                        numOfFriendships = initializer;
                    }
                    else if (rumorStarters == null && numOfFriendships == friendshipCount)
                    {
                        rumorStarters = new String[initializer];
                    }
                }
                else
                {
                    if (studentsAndFriends != null && !gotFriendships && rumorStarters == null)
                    {
                        studentsAndFriends[input] = new LinkedList<String>();
                        studentCount++;
                    }
                    else if (studentsAndFriends != null && gotFriendships && rumorStarters == null)
                    {
                        parsedInput = input.Split();
                        studentsAndFriends[parsedInput[0]].AddLast(parsedInput[1]);
                        studentsAndFriends[parsedInput[1]].AddLast(parsedInput[0]);
                        friendshipCount++;
                    }
                    else if (studentsAndFriends != null && gotFriendships && rumorStarters != null)
                    {
                        rumorStarters[count] = input;
                        count++;
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
            Queue<String> Q = new Queue<String>();
            Q.enq(0, rumorStarter);

            while(!(Q.isEmpty))
            {
                String student = Q.deq();
                foreach(String friend in studentBody[student])
                {
                    if(dayHeard[friend] == Double.PositiveInfinity)
                    {
                        Q.enq(0, friend);
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

        class Queue<String>
        {
            internal class Node : IComparable<Node>
            {
                public int minPriority;
                public String min;

                public int CompareTo(Node rhs)
                {
                    return minPriority.CompareTo(rhs.minPriority);
                }
            }

            private MinHeap<Node> minHeap = new MinHeap<Node>();

            public void enq(int priority, String item)
            {
                minHeap.Add(new Node() { minPriority = priority, min = item });
            }

            public String deq()
            {
                return minHeap.deleteMin().min;
            }

            public bool isEmpty
            {
                get { return minHeap.Count == 0; }
            }
        }

        class MinHeap<String> where String : IComparable<String>
        {
            private List<String> heap = new List<String>();

            public void Add(String item)
            {
                heap.Add(item);
                int c = heap.Count - 1;

                while (c > 0 && heap[c].CompareTo(heap[c/2]) == -1)
                {
                    String tmp = heap[c];
                    heap[c] = heap[c/2];
                    heap[c/2] = tmp;
                    c = c/2;
                }
            }

            public String deleteMin()
            {
                String minValue = heap[0];
                heap[0] = heap[heap.Count - 1];
                heap.RemoveAt(heap.Count - 1);

                int c = 0;
                while (c < heap.Count)
                {
                    int min = c;

                    if ( ((2 * c + 1) < heap.Count) && (heap[2 * c + 1].CompareTo(heap[min]) == -1) )
                    {
                        min = 2 * c + 1;
                    }

                    if ( ((2 * c + 2) < heap.Count) && (heap[2 * c + 2].CompareTo(heap[min]) == -1) )
                    {
                        min = 2 * c + 2;
                    }

                    if (min == c)
                    {
                        break;
                    }
                    else
                    {
                        String temp = heap[c];
                        heap[c] = heap[min];
                        heap[min] = temp;
                        c = min;
                    }
                }

                return minValue;
            }

            public int Count
            {
                get { return heap.Count; }
            }
        }
    }
}
