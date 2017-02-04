using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyQuest
{
    class GalaxyQuest
    {
        static void Main(string[] args)
        {
            String input;
            String[] initializers = null;
            Point[] stars = null;
            String[] parsedInput;
            long d = 0;
            int count = 0;

            while (true)
            {
                if (String.IsNullOrWhiteSpace(input = Console.ReadLine()))
                {
                    break;
                }
                else if (initializers == null)
                {
                    initializers = input.Split();
                    d = (long)Math.Pow(int.Parse(initializers[0]), 2);
                    stars = new Point[int.Parse(initializers[1])];
                }
                else
                {
                    parsedInput = input.Split();
                    stars[count] = new Point(int.Parse(parsedInput[0]), int.Parse(parsedInput[1]));
                    count++;
                }
            }

            Point result = findMajority(stars, d);
            if (result == null)
            {
                Console.WriteLine("NO");
            }
            else if (result.X == null)
            {
                if(result.Y > stars.Length / 2)
                {
                    Console.WriteLine(result.Y);
                }
                else
                {
                    Console.WriteLine("NO");
                }
            }
            else if (result.Y == null)
            {
                if (result.X > stars.Length / 2)
                {
                    Console.WriteLine(result.X);
                }
                else
                {
                    Console.WriteLine("NO");
                }
            }
            else
            {
                if (stars.Length / 2 < 1)
                {
                    Console.WriteLine(1);
                }
                else
                {
                    Console.WriteLine("NO");
                }
            }

            Console.Read();
        }

        public static Point findMajority(Point[] A, long d)
        {
            if (A.Length == 0)
            {
                return null;
            }
            else if (A.Length == 1)
            {
                return A[0];
            }
            else
            {
                Point[] A1, A2;
                A1 = A.Take(A.Length / 2).ToArray();
                A2 = A.Skip(A.Length / 2).ToArray();

                Point x = findMajority(A1, d);
                Point y = findMajority(A2, d);

                if (x == null && y == null)
                {
                    return null;
                }
                else if (x == null)
                {
                    int yCount = 0;
                    foreach (Point p in A)
                    {
                        if (distance(y, p) <= d)
                        {
                            yCount++;
                        }
                    }

                    if ((yCount > 0) && (yCount > A.Length / 2))
                    {
                        return new Point(null, yCount);
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (y == null)
                {
                    int xCount = 0;
                    foreach (Point p in A)
                    {
                        if (distance(x, p) <= d)
                        {
                            xCount++;
                        }
                    }

                    if ((xCount > 0) && (xCount > A.Length / 2))
                    {
                        return new Point(xCount, null);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    int xCount = 0;
                    int yCount = 0;

                    foreach (Point p in A)
                    {
                        if (distance(x, p) <= d)
                        {
                            xCount++;
                        }
                        if (distance(y, p) <= d)
                        {
                            yCount++;
                        }
                    }

                    if ((xCount > yCount) && (xCount > A.Length / 2))
                    {
                        return new Point(xCount, null);
                    }
                    else if ((yCount > xCount) && (yCount > A.Length / 2))
                    {
                        return new Point(null, yCount);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Result returned is squared distance
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static long distance(Point s1, Point s2)
        {
            return (long)Math.Pow(((s1.X ?? default(int)) - (s2.X ?? default(int))), 2) 
                + (long)Math.Pow(((s1.Y ?? default(int)) - (s2.Y ?? default(int))), 2);
        }

        public class Point
        {
            private int? x, y;

            public Point(int? x, int? y)
            {
                this.x = x;
                this.y = y;
            }

            public int? X
            {
                get { return x; }
                set { x = value; }
            }

            public int? Y
            {
                get { return y; }
                set { y = value; }
            }
        }
    }
}