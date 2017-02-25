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
            LinkedList<Node>[] dungeon = null;
            String[] parsedInput;
            List<Double> results = new List<Double>();
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

                        dungeon = new LinkedList<Node>[n];

                        for (int i = 0; i < m; i++)
                        {
                            input = Console.ReadLine();
                            parsedInput = input.Split();

                            if(dungeon[int.Parse(parsedInput[0])] == null)
                            {
                                dungeon[int.Parse(parsedInput[0])] = new LinkedList<Node>();
                            }

                            if (dungeon[int.Parse(parsedInput[1])] == null)
                            {
                                dungeon[int.Parse(parsedInput[1])] = new LinkedList<Node>();
                            }

                            dungeon[int.Parse(parsedInput[0])].AddLast(new Node(int.Parse(parsedInput[1]), double.Parse(parsedInput[2])));
                            dungeon[int.Parse(parsedInput[1])].AddLast(new Node(int.Parse(parsedInput[0]), double.Parse(parsedInput[2])));
                        }

                        results.Add(Dijkstras(dungeon, 0, n-1));
                    }
                }
            }

            foreach(double r in results)
            {
                Console.WriteLine(r.ToString("N4"));
            }

            Console.Read();
        }

        public static double Dijkstras(LinkedList<Node>[] G, int start, int end)
        {
            double[] dist = new double[G.Count()];
            int?[] prev = new int?[G.Count()];

            for (int u = 0; u < G.Count(); u++)
            {
                dist[u] = 0.0;
                prev[u] = null;
            }

            dist[start] = 0;

            MaxHeap PQ = new MaxHeap();
            PQ.insertOrChange(null, new Node(start, 0));

            while (!(PQ.isEmpty()))
            {
                Node uVertex = PQ.deleteMax();
                foreach (Node n in G[uVertex.Intersection])
                {
                    int u = uVertex.Intersection;
                    int v = n.Intersection;
                    double w = n.Weight;

                    if (dist[v] > dist[u] + w)
                    {
                        Node oldNode = new Node(v, dist[v]);

                        dist[v] = Math.Round((dist[u] + w), 4);
                        prev[v] = u;
                        PQ.insertOrChange(oldNode, new Node(v, dist[v]));
                    }
                }
            }

            return Math.Round(dist.Max(), 4);
        }

        public class Node
        {
            private int intersection;
            private double weight;

            public Node(int intersection, double weight)
            {
                this.intersection = intersection;
                this.weight = weight;
            }

            public int Intersection
            {
                get { return intersection; }
                set { intersection = value; }
            }

            public double Weight
            {
                get { return weight; }
                set { weight = value; }
            }

            public int CompareTo(Node node)
            {
                if (this.Weight < node.Weight)
                    return -1;
                else if (this.Weight > node.Weight)
                    return 1;
                return 0;
            }
        }

        public class MaxHeap
        {
            private IList<Node> heap;

            public MaxHeap(Node[] elements = null)
            {
                if (elements != null)
                {
                    heap = new List<Node>(elements);
                    for (int i = elements.Length / 2; i >= 0; i--)
                    {
                        HeapifyDown(i);
                    }
                }
                else
                {
                    heap = new List<Node>();
                }
            }

            public int Count
            {
                get { return heap.Count; }
            }

            public bool isEmpty()
            {
                return heap.Count == 0;
            }

            public Node deleteMax()
            {
                var max = heap[0];
                heap[0] = heap[Count - 1];
                heap.RemoveAt(Count - 1);

                if (Count > 0)
                {
                    HeapifyDown(0);
                }

                return max;
            }

            public void insertOrChange(Node oldNode, Node newNode)
            {
                foreach(Node n in heap)
                {
                    if(n.Intersection == oldNode.Intersection)
                    {
                        if(n.Weight == oldNode.Weight)
                        {
                            oldNode = n;
                            break;
                        }
                    }
                }

                heap.Remove(oldNode);
                heap.Add(newNode);

                HeapifyUp(Count - 1);
            }

            private void HeapifyDown(int i)
            {
                var leftChild = (i * 2) + 1;
                var rightChild = (i * 2) + 2;
                var biggest = i;

                if (leftChild < Count && heap[leftChild].CompareTo(heap[biggest]) > 0)
                {
                    biggest = leftChild;
                }

                if (rightChild < Count && heap[rightChild].CompareTo(heap[biggest]) > 0)
                {
                    biggest = rightChild;
                }

                if (biggest != i)
                {
                    Node old = heap[i];
                    heap[i] = heap[biggest];
                    heap[biggest] = old;
                    HeapifyDown(biggest);
                }
            }

            private void HeapifyUp(int i)
            {
                var parent = (i - 1) / 2;
                while (i > 0 && heap[i].CompareTo(heap[parent]) > 0)
                {
                    var temp = heap[parent];
                    heap[parent] = heap[i];
                    heap[i] = temp;
                    i = parent;
                    parent = (i - 1) / 2;
                }
            }
        }

    }
}