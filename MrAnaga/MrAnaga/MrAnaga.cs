using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrAnaga
{
    /// <summary>
    /// Candace Wilson
    /// u0838375
    /// CS 4150
    /// Professor Zacahry
    /// Mr. Anaga
    /// 
    /// Your boss, Mr. Anaga, is a constructor of word puzzles who is always giving you unusual problems to solve. 
    /// Today he has given you a list of words and asked you to determine how many of the words are not anagrams of 
    /// any other words on the list.
    /// 
    /// For example, suppose that the list he gives you is:
    ///    me
    ///    em
    ///    to
    /// You would need to tell him “1”, because “me” and “em” are anagrams of one another, leaving only “to”. 
    /// 
    /// Or suppose that the list he gives you is:
    ///    tape
    ///    rate
    ///    seat
    ///    pate
    ///    east
    ///    pest
    /// You would need to tell him “2”, because “tape”/“pate” and “seat”/“east” are anagrams, leaving only “rate” and “pest”.
    /// 
    /// Input:
    /// The first line contains integers n and k separated by a space, where 1 ≤ n ≤ 10000 and 1 ≤ k ≤ 1000. 
    /// The n words, one to a line, follow. Each word contains exactly k lower case letters. 
    /// (The words are not necessarily in any dictionary you’ve ever seen.)
    /// 
    /// Output:
    /// Produce a single line of output that contains the number of words on the list that are not anagrams of any other 
    /// words on the list. This number, of course, should be between 0 and n inclusive.
    /// </summary>
    class MrAnaga
    {
        public const int DURATION = 1000;
        public const int k = 5;
        public const int n = 2000;

        static void Main(string[] args)
        {
            // Report the average time required to do a linear search for various sizes
            // of arrays.
            int size = 32;
            Console.WriteLine("\nSize\tTime (msec)\tRatio (msec)");
            double previousTime = 0;
            for (int i = 0; i <= 7; i++)
            {
                size = size * 2;
                double currentTime = AnagaTiming(size - 1);
                Console.Write((size - 1) + "\t" + currentTime.ToString("G3"));
                if (i > 0)
                {
                    Console.WriteLine("   \t" + (currentTime / previousTime).ToString("G3"));
                }
                else
                {
                    Console.WriteLine();
                }
                previousTime = currentTime;
            }

            Console.WriteLine("\nDONE");
            Console.Read();
        }

        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static double AnagaTiming(int size)
        {
            // Construct array
            String[] data = new String[n];
            for (int i = 0; i < n; i++)
            {
                data[i] = RandomString(size);
            }

            // Get the process
            Process p = Process.GetCurrentProcess();

            List<String> anagrams = new List<String>();
            List<String> notAnagrams = new List<String>();
            String sorted = "";

            // Keep increasing the number of repetitions until one second elapses.
            double elapsed = 0;
            long repetitions = 1;
            do
            {
                repetitions *= 2;
                TimeSpan start = p.TotalProcessorTime;
                for (int i = 0; i < repetitions; i++)
                {
                    for (int d = 0; d < size; d++)
                    {
                        Anaga(data, anagrams, notAnagrams, sorted);
                    }
                }
                TimeSpan stop = p.TotalProcessorTime;
                elapsed = stop.TotalMilliseconds - start.TotalMilliseconds;
            } while (elapsed < DURATION);
            double totalAverage = elapsed / repetitions / size;

            // Keep increasing the number of repetitions until one second elapses.
            elapsed = 0;
            repetitions = 1;
            do
            {
                repetitions *= 2;
                TimeSpan start = p.TotalProcessorTime;
                for (int i = 0; i < repetitions; i++)
                {
                    for (int d = 0; d < size; d++)
                    {
                        //Anaga(data);
                    }
                }
                TimeSpan stop = p.TotalProcessorTime;
                elapsed = stop.TotalMilliseconds - start.TotalMilliseconds;
            } while (elapsed < DURATION);
            double overheadAverage = elapsed / repetitions / size;

            // Return the difference
            return totalAverage - overheadAverage;
        }

        public static void Anaga(String[] data, List<String> anagrams, List<String> notAnagrams, String sorted)
        {
            for (int i = 0; i < data.Length; i++)
            {
                sorted = sortWord(data[i].ToArray());
                if (notAnagrams.Contains(sorted))
                {
                    notAnagrams.Remove(sorted);
                    anagrams.Add(sorted);
                }
                else if (!(anagrams.Contains(sorted)))
                {
                    notAnagrams.Add(sorted);
                }
            }
        }

        private static String sortWord(Char[] charArray)
        {
            Array.Sort(charArray);
            return new String(charArray);
        }
    }
}
