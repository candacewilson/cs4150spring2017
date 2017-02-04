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
            if(result == null)
            {
                Console.WriteLine("NO");
            }
            else if(result.X == 0)
            {
                Console.WriteLine(result.Y);
            }
            else if(result.Y == 0)
            {
                Console.WriteLine(result.X);
            }
            else
            {
                Console.WriteLine(1);
            }

            Console.Read();
        }

        public static Point findMajority(Point[] A, long d)
        {
            if(A.Length == 0)
            {
                return null;
            }
            else if(A.Length == 1)
            {
                return A[0];
            }
            else
            {
                Point[] A1, A2;
                A1 = A.Take(A.Length / 2).ToArray();
                A2 = A.Skip(A.Length / 2).ToArray();

                //Point y = new Point(0,0);
                //if (A1.Length > A2.Length)
                //{
                //    y = new Point(A1[A1.Length - 1].X, A1[A1.Length - 1].Y);
                //}
                //else if (A1.Length < A2.Length)
                //{
                //    y = new Point(A2[A2.Length - 1].X, A2[A2.Length - 1].Y);
                //}

                //Dictionary<Point, Point> pairs = new Dictionary<Point, Point>();

                //for(int i = 0; i < A1.Length; i++)
                //{
                //    if (A1[i] == null)
                //    {
                //        if(A2[i] == null)
                //        {
                //            pairs.Add(new Point(0, 0), new Point(0, 0));
                //        }
                //        else
                //        {
                //            pairs.Add(new Point(0, 0), A2[i]);
                //        }
                //    }
                //    else if(A1[i] != null)
                //    {
                //        if (A2[i] == null)
                //        {
                //            pairs.Add(A1[i], new Point(0, 0));
                //        }
                //        else
                //        {
                //            pairs.Add(A1[i], A2[i]);
                //        }
                //    }
                //}

                //Point[] APrime = new Point[A2.Length];
                //int count = 0;

                //foreach(KeyValuePair<Point, Point> pair in pairs)
                //{
                //    Point p1 = pair.Key;
                //    Point p2 = pair.Value;

                //    if((p1 == null && p2 == null) || (p1 != null && p2 != null))
                //    {
                //        if (distance(p1, p2) <= d)
                //        {
                //            APrime[count] = p2;
                //            count++;
                //        }
                //    }
                //}

                //Point x = findMajority(APrime, d);
                //if(x == null)
                //{
                //    if (A.Length % 2 != 0)
                //    {
                //        int yCount = 0;
                //        foreach (Point p in A)
                //        {
                //            if (distance(y, p) <= d)
                //            {
                //                yCount++;
                //            }
                //        }

                //        if (yCount > 0)
                //        {
                //            return new Point(0, yCount);
                //        }
                //        else
                //        {
                //            return null;
                //        }
                //    }
                //    else
                //    {
                //        return null;
                //    }
                //}
                //else
                //{
                //    int xCount = 0;
                //    foreach (Point p in A)
                //    {
                //        if (distance(x, p) <= d)
                //        {
                //            xCount++;
                //        }
                //    }

                //    if (xCount > 0)
                //    {
                //        return new Point(xCount, 0);
                //    }
                //    else
                //    {
                //        return null;
                //    }
                //}
















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

                    if (yCount > 0)
                    {
                        return new Point(0, yCount);
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

                    if (xCount > 0)
                    {
                        return new Point(xCount, 0);
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

                    if (xCount > yCount)
                    {
                        return new Point(xCount, 0);
                    }
                    else if (yCount > xCount)
                    {
                        return new Point(0, yCount);
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
            if (s1 == null)
            {
                return (long)Math.Pow((0 - s2.X), 2) + (long)Math.Pow((0 - s2.Y), 2);
            }

            if (s2 == null)
            {
                return (long)Math.Pow((s1.X - 0), 2) + (long)Math.Pow((s1.Y - 0), 2);
            }

            return (long)Math.Pow((s1.X - s2.X), 2) + (long)Math.Pow((s1.Y - s2.Y), 2);
        }

        public class Point
        {
            private int x, y;

            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public int X
            {
                get { return x; }
                set { x = value; }
            }

            public int Y
            {
                get { return y; }
                set { y = value; }
            }
        }
    }
}
