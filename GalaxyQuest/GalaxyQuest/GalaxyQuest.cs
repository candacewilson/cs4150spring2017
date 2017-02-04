﻿using System;
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
            int d = 0;
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
                    d = (int)Math.Pow(int.Parse(initializers[0]), 2);
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

        public static Point findMajority(Point[] A, int d)
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

                Point x = findMajority(A1, d);
                Point y = findMajority(A2, d);

                if(x == null && y == null)
                {
                    return null;
                }
                else if(x == null)
                {
                    int yCount = 0;
                    foreach(Point p in A)
                    {
                        if(distance(y, p) <= d)
                        {
                            yCount++;
                        }
                    }

                    if(yCount > 0)
                    {
                        return new Point(0, yCount);
                    }
                    else
                    {
                        return null;
                    }
                }
                else if(y == null)
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

                    if(xCount > yCount)
                    {
                        return new Point(xCount, 0);
                    }
                    else if(yCount > xCount)
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
            return (int)Math.Pow((s1.X - s2.X), 2) + (int)Math.Pow((s1.Y - s2.Y), 2);
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
