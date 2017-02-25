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

                        dungeon = new LinkedList<Node>[m];

                        for (int i = 0; i < m; i++)
                        {
                            input = Console.ReadLine();
                            parsedInput = input.Split();
                            dungeon[int.Parse(parsedInput[0])].AddLast(new Node(int.Parse(parsedInput[1]), double.Parse(parsedInput[2])));
                            dungeon[int.Parse(parsedInput[1])].AddLast(new Node(int.Parse(parsedInput[0]), double.Parse(parsedInput[2])));
                        }

                        Console.WriteLine(Dijkstras(dungeon, 0, n-1));
                    }
                }
            }

            Console.Read();
        }

        public static double Dijkstras(LinkedList<Node>[] G, int start, int end)
        {
            double[] dist = new double[G.Count()];
            int?[] prev = new int?[G.Count()];

            for (int u = 0; u < G.Count(); u++)
            {
                dist[u] = double.PositiveInfinity;
                prev[u] = null;
            }

            dist[start] = 0;

            MaxHeap PQ = new MaxHeap(G.Count());
            PQ.insertOrChange(start, 0);

            while (!(PQ.isEmpty()))
            {
                Node uVertex = PQ.deleteMax();
                foreach (Node n in G[uVertex.Intersection])
                {
                    int u = uVertex.Intersection;
                    int v = n.Intersection;
                    double w = n.Weight;

                    if (dist[v] > dist[u] * w)
                    {
                        dist[v] = dist[u] * w;
                        prev[v] = u;
                        PQ.insertOrChange(v, dist[v]);
                    }
                }
            }

            return dist.Max();
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
        }

        public class MaxHeap
        {
            private Node[] Heap;
            private int size;
            private int max;

            private const int maxValue = 1;

            public MaxHeap(int max)
            {
                this.max = max;
                this.size = 0;
                Heap = new Node[this.max + 1];
                Heap[0] = new Node(int.MaxValue, double.PositiveInfinity);
            }

            public void insertOrChange(int intersection, double weight)
            {
                Heap[intersection] = new Node(intersection, weight);
            }

            public void insert(Node element)
            {
                Heap[++size] = element;
                int current = size;

                while (Heap[current].Weight > Heap[parent(current)].Weight)
                {
                    swap(current, parent(current));
                    current = parent(current);
                }
            }

            public Node deleteMax()
            {
                Node popped = Heap[maxValue];
                Heap[maxValue] = Heap[size--];
                maxHeapify(maxValue);
                return popped;
            }

            public void maxHeap()
            {
                for (int pos = (size / 2); pos >= 1; pos--)
                {
                    maxHeapify(pos);
                }
            }

            private void maxHeapify(int pos)
            {
                if (!isLeaf(pos))
                {
                    if (Heap[pos].Weight < Heap[leftChild(pos)].Weight || Heap[pos].Weight < Heap[rightChild(pos)].Weight)
                    {
                        if (Heap[leftChild(pos)].Weight > Heap[rightChild(pos)].Weight)
                        {
                            swap(pos, leftChild(pos));
                            maxHeapify(leftChild(pos));
                        }
                        else
                        {
                            swap(pos, rightChild(pos));
                            maxHeapify(rightChild(pos));
                        }
                    }
                }
            }

            public bool isEmpty()
            {
                return Heap.Count() == 0;
            }

            private void swap(int fpos, int spos)
            {
                Node tmp;
                tmp = Heap[fpos];
                Heap[fpos] = Heap[spos];
                Heap[spos] = tmp;
            }

            private bool isLeaf(int pos)
            {
                if (pos >= (size / 2) && pos <= size)
                {
                    return true;
                }
                return false;
            }

            private int parent(int pos)
            {
                return pos / 2;
            }

            private int leftChild(int pos)
            {
                return (2 * pos);
            }

            private int rightChild(int pos)
            {
                return (2 * pos) + 1;
            }
        }

    }
}