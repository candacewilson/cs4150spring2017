using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NarrowArtGallery
{
    class NarrowArtGallery
    {
        static void Main(string[] args)
        {
            String input;
            String[] parsedInput;
            int[,] values;
            int k;

            while (true)
            {
                if (String.IsNullOrWhiteSpace(input = Console.ReadLine()))
                {
                    break;
                }
                else
                {
                    parsedInput = input.Split();
                    values = new int[int.Parse(parsedInput[0]),2];
                    k = int.Parse(parsedInput[1]);
                    
                }
            }

            Console.WriteLine();
            Console.Read();
        }
    }
}
