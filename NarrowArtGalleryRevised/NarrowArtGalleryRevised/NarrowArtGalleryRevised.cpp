// NarrowArtGalleryRevised.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <climits>
#include<queue>
#include<iostream>

using namespace std;

int main()
{
	int N, k;
	int galleryRoomStatus[200][3][200], gallery[200][2];
	int result;

	while (cin >> N >> k)
	{
		if (N == 0 && k == 0)
		{
			cout << result << endl;
			break;
		}

		for (int i = 0; i < N; i++)
		{
			for (int j = 0; j < 2; j++)
			{
				cin >> gallery[i][j];
			}
		}

		// roomStatus will contain the max possible N rows, with 3 different statuses, with each status containing k
		// rooms (<= N) already closed
		//
		// roomStatus[200][3][200] = 
		// {
		//		{{x1, x2, ..., x200}, {x1, x2, ..., x200}, {x1, x2, ..., x200}},
		//		{{x1, x2, ..., x200}, {x1, x2, ..., x200}, {x1, x2, ..., x200}},
		//		...
		//		{{x1, x2, ..., x200}, {x1, x2, ..., x200}, {x1, x2, ..., x200}}
		// };
		//
		// 3 different statuses: 0 = either may be closed if desired, 1 = close left, 2 = close right.

		for (int i = 0; i < 3; i++)
		{
			fill(galleryRoomStatus[0][i], galleryRoomStatus[0][i] + k + 1, INT_MIN);
		}

		galleryRoomStatus[0][0][0] = gallery[0][0] + gallery[0][1]; // Neither of first rooms are closed, grab both values
		galleryRoomStatus[0][1][1] = gallery[0][1]; // First left room is closed, grab value in right room
		galleryRoomStatus[0][2][1] = gallery[0][0]; // First right room is closed, grab value in left room

		for (int i = 1; i < N; i++)
		{
			for (int j = 0; j <= k; j++)
			{
				galleryRoomStatus[i][0][j] = max(galleryRoomStatus[i-1][0][j], max(galleryRoomStatus[i-1][1][j], galleryRoomStatus[i-1][2][j])) + gallery[i][0] + gallery[i][1];

				if (j == 0)
				{
					galleryRoomStatus[i][1][j] = galleryRoomStatus[i][2][j] = INT_MIN;
				}
				else
				{
					galleryRoomStatus[i][1][j] = max(galleryRoomStatus[i-1][0][j-1], galleryRoomStatus[i-1][1][j-1]) + gallery[i][1];
					galleryRoomStatus[i][2][j] = max(galleryRoomStatus[i-1][0][j-1], galleryRoomStatus[i-1][2][j-1]) + gallery[i][0];
				}
			}
		}

		result = max(galleryRoomStatus[N-1][0][k], max(galleryRoomStatus[N-1][1][k], galleryRoomStatus[N-1][2][k]));
	}

	return 0;
}