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
            List<String> results = new List<String>();

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
                            results.Add(gcd(BigInteger.Parse(parsedInput[1]), BigInteger.Parse(parsedInput[2])).ToString());
                            break;

                        case "exp":
                            results.Add(exp(BigInteger.Parse(parsedInput[1]), BigInteger.Parse(parsedInput[2]), BigInteger.Parse(parsedInput[3])).ToString());
                            break;

                        case "inverse":
                            BigInteger? r = inverse(BigInteger.Parse(parsedInput[1]), BigInteger.Parse(parsedInput[2]));
                            if(r == null)
                            {
                                results.Add("none");
                            }
                            else
                            {
                                results.Add(r.ToString());
                            }
                            break;

                        case "isprime":
                            results.Add(isPrime(BigInteger.Parse(parsedInput[1])));
                            break;

                        case "key":
                            results.Add(key(BigInteger.Parse(parsedInput[1]), BigInteger.Parse(parsedInput[2])));
                            break;
                    }
                }
            }

            foreach(String r in results)
            {
                Console.WriteLine(r);
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

        public static BigInteger? inverse(BigInteger a, BigInteger N)
        {
            if(gcd(a, N) != 1)
            {
                return null;
            }
            else
            {
                //return exp(a, N - 2, N);






                a = a % N;
                for (BigInteger i = 2; i < N; i++)
                {
                    if ((a * i) % N == 1)
                    {
                        return i;
                    }
                }

                return null;
            }
        }

        public static String isPrime(BigInteger p)
        {
            int[] list = new int[] { 2, 3, 5 };
            foreach(int a in list)
            {
                if(inverse((BigInteger)Math.Pow(a, (double)(p-1)), p) != 1)
                {
                    return "no";
                }
            }

            return "yes";
        }

        public static String key(BigInteger p, BigInteger q)
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

            return N.ToString() + " " + e.ToString() + " " + d.ToString();
        }
    }
}
