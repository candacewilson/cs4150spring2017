using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace NumberTheory
{
    class NumberTheory
    {
        static void Main(string[] args)
        {
            String input;
            String[] parsedInput;
            String operation;

            while (true)
            {
                if (String.IsNullOrWhiteSpace(input = Console.ReadLine()))
                {
                    break;
                }
                else
                {
                    parsedInput = input.Split();
                    operation = parsedInput[0];

                    switch (operation)
                    {
                        case "gcd":
                            Console.WriteLine(gcd(BigInteger.Parse(parsedInput[1]), BigInteger.Parse(parsedInput[2])));
                            break;

                        case "exp":
                            Console.WriteLine(exp(BigInteger.Parse(parsedInput[1]), BigInteger.Parse(parsedInput[2]), BigInteger.Parse(parsedInput[3])));
                            break;

                        case "inverse":
                            Console.WriteLine(inverse(BigInteger.Parse(parsedInput[1]), BigInteger.Parse(parsedInput[2])));
                            break;

                        case "isprime":
                            Console.WriteLine(isPrime(BigInteger.Parse(parsedInput[1])));
                            break;

                        case "key":
                            key(BigInteger.Parse(parsedInput[1]), BigInteger.Parse(parsedInput[2]));
                            break;
                    }
                }
            }

            Console.Read();
        }

        public static BigInteger gcd(BigInteger a, BigInteger b)
        {
            if (b == 0)
            {
                return a;
            }
            else
            {
                return gcd(b, a % b);
            }
        }

        public static BigInteger exp(BigInteger x, BigInteger y, BigInteger N)
        {
            if (y == 0)
            {
                return 1;
            }
            else
            {
                BigInteger z = exp(x, y / 2, N);
                if(y % 2 == 0)
                {
                    return (z * z) % N;
                }
                else
                {
                    return (x * (z * z)) % N;
                }
            }
        }

        public static Object inverse(BigInteger a, BigInteger N)
        {
            if(gcd(a, N) != 1)
            {
                return "none";
            }
            else
            {
                a = a % N;
                for(BigInteger i = 2; i < N; i++)
                {
                    if((a*i) % N == 1)
                    {
                        return i;
                    }
                }

                return null;



                //BigInteger i = N;
                //BigInteger v = 0;
                //BigInteger d = 1;

                //while (a > 0)
                //{
                //    BigInteger t = i / a;
                //    BigInteger x = a;
                //    d = v - (t * x);
                //    v = x;
                //}

                //v = v % N;
                //if(v < 0)
                //{
                //    return (v + N) % N;
                //}
            }
        }

        public static bool isPrime(BigInteger p)
        {
            int[] list = new int[] { 2, 3, 5 };
            foreach(int a in list)
            {
                if(Math.Pow(a, (double)p-1) != 1)
                {
                    return false;
                }
            }

            return true;
        }

        public static void key(BigInteger p, BigInteger q)
        {
            BigInteger N = p * q;
            BigInteger phi = (p - 1) * (q - 1);
            BigInteger e = 0;

            for(int i = 2; i < phi; i++)
            {
                if(gcd(i, phi) == 1)
                {
                    e = i;
                    break;
                }
            }

            BigInteger d = (BigInteger)inverse(e, phi);

            Console.WriteLine(String.Join(" ", new BigInteger[] { N, e, d }));
        }
    }
}
