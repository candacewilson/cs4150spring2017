using System;
using System.Collections.Generic;
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
        static void Main(string[] args)
        {
            String input;
            int numOfWords;
            int i = 0;
            String[] words = null;

            do
            {
                input = Console.ReadLine();

                if(int.TryParse(input, out numOfWords))
                {
                    if(words == null)
                    {
                        words = new String[numOfWords];
                    }
                }
                else
                {
                    words[i] = input;
                    i++;
                }
            } while (input != null);

            HashSet<String> notAnagrams = new HashSet<String>();
            foreach(String word in words)
            {
                notAnagrams.Add(sortWord(word));
            }

            Console.WriteLine(notAnagrams.Count);
        }

        private static String sortWord(String s)
        {
            Char[] charArray = s.ToArray();
            Array.Sort(charArray);
            return new String(charArray);
        }
    }
}
