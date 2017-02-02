﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS3_2
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] A = { 2, 5, 7, 22 };
            int[] B = { 7, 8, 9, 14 };
            int k = 5;

            Console.WriteLine("Element stored at index " + k + " of the merged array of A = { 2, 5, 7, 22 } and B = { 7, 8, 9, 14 } is: " + select(A, B, k));
            Console.Read();
        }

        // A and B are each sorted into ascending order, and 0 <= k < |A|+|B| 
        // Returns the element that would be stored at index k if A and B were
        // combined into a single array that was sorted into ascending order.
        public static int select(int[] A, int[] B, int k)
        {
            return select(A, 0, A.Length - 1, B, 0, B.Length - 1, k);
        }

        private static int select(int[] A, int loA, int hiA, int[] B, int loB, int hiB, int k)
        {
            // A[loA..hiA] is empty
            if (hiA < loA)
                return B[k - loA];

            // B[loB..hiB] is empty
            if (hiB < loB)
                return A[k - loB];

            // Get the midpoints of A[loA..hiA] and B[loB..hiB]
            int i = (loA + hiA) / 2;
            int j = (loB + hiB) / 2;

            // Figure out which one of four cases apply
            if (A[i] > B[j])
                if (k <= i + j)
                    return select(A, loA, i-1, B, loB, hiB, k);
                else
                    return select(A, loA, hiA, B, j+1, hiB, k);
            else
                if (k <= i + j)
                    return select(A, loA, hiA, B, loB, j-1, k);
                else
                    return select(A, i+1, hiA, B, loB, hiB, k);
        }
    }
}
