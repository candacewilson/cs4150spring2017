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
            int[,] gallery = null;
            double[,,] galleryRoomStatus = null;
            int N = 0;
            int k = 0;
            int result = 0;

            while (true)
            {
                if (String.IsNullOrWhiteSpace(input = Console.ReadLine()))
                {
                    break;
                }
                else
                {
                    parsedInput = input.Split();
                    N = int.Parse(parsedInput[0]);
                    k = int.Parse(parsedInput[1]);

                    if(N == 0 && k == 0)
                    {
                        break;
                    }

                    gallery = new int[N, 2];
                    galleryRoomStatus = new double[N, k + 1, 3];

                    for (int i = 0; i < N; i++)
                    {
                        parsedInput = Console.ReadLine().Split();

                        gallery[i, 0] = int.Parse(parsedInput[0]);
                        gallery[i, 1] = int.Parse(parsedInput[1]);
                    }

                    result = maxValue(gallery, galleryRoomStatus, N, k);
                }
            }

            Console.WriteLine(result);
            Console.Read();
        }

        public static int maxValue(int[,] gallery, double[,,] galleryRoomStatus, int N, int k)
        {
            double minValue = double.NegativeInfinity;

            galleryRoomStatus[0, 0, 0] = minValue;
            galleryRoomStatus[0, 0, 1] = minValue;
            galleryRoomStatus[0, 0, 2] = gallery[0, 0] + gallery[0, 1];

            for (int i = 1; i < N; i++)
            {
                galleryRoomStatus[i, 0, 0] = minValue;
                galleryRoomStatus[i, 0, 1] = minValue;
                galleryRoomStatus[i, 0, 2] = galleryRoomStatus[i-1, 0, 2] + gallery[i, 0] + gallery[i, 1];
            }

            if (k > 0)
            {
                galleryRoomStatus[0, 1, 0] = gallery[0, 1];
                galleryRoomStatus[0, 1, 1] = gallery[0, 0];
                galleryRoomStatus[0, 1, 2] = minValue;
            }

            for (int i = 1; i < N; i++)
            {
                for (int j = 1; j <= Math.Min(i + 1, k); j++)
                {
                    galleryRoomStatus[i, j, 0] = gallery[i, 1] + Math.Max(galleryRoomStatus[i-1, j-1, 2], galleryRoomStatus[i-1, j-1, 0]);
                    galleryRoomStatus[i, j, 1] = gallery[i, 0] + Math.Max(galleryRoomStatus[i-1, j-1, 2], galleryRoomStatus[i-1, j-1, 1]);
                    galleryRoomStatus[i, j, 2] = gallery[i, 0] + gallery[i, 1] + Math.Max(galleryRoomStatus[i-1, j, 0], Math.Max(galleryRoomStatus[i-1, j, 1], galleryRoomStatus[i-1, j, 2]));
                }
            }

            return (int)Math.Max(galleryRoomStatus[N-1, k, 0], Math.Max(galleryRoomStatus[N-1, k, 1], galleryRoomStatus[N-1, k, 2]));
        }
    }
}
