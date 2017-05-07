// NarrowArtGalleryRevised.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include<queue>
#include<iostream>

using namespace std;

int main()
{
	int N, k;
	int roomStatus[200][3][200], gallery[200][2];
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

		// roomStatus will contain the max possible N rows, with 3 different statuses, with each status contain the max
		// possible N rows (to simulate the gallery for each of the 3 statuses).
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
			fill(roomStatus[0][i], roomStatus[0][i] + k + 1, INT_MIN);
		}

		roomStatus[0][0][0] = gallery[0][0] + gallery[0][1];
		roomStatus[0][1][1] = gallery[0][1];
		roomStatus[0][2][1] = gallery[0][0];

		for (int i = 1; i < N; i++)
		{
			for (int j = 0; j <= k; j++)
			{
				roomStatus[i][0][j] = max(roomStatus[i - 1][0][j], max(roomStatus[i - 1][1][j], roomStatus[i - 1][2][j])) + gallery[i][0] + gallery[i][1];

				if (j == 0)
				{
					roomStatus[i][1][j] = roomStatus[i][2][j] = INT_MIN;
				}
				else
				{
					roomStatus[i][1][j] = max(roomStatus[i - 1][0][j - 1], roomStatus[i - 1][1][j - 1]) + gallery[i][1];
					roomStatus[i][2][j] = max(roomStatus[i - 1][0][j - 1], roomStatus[i - 1][2][j - 1]) + gallery[i][0];
				}
			}
		}

		result = max(roomStatus[N - 1][0][k], max(roomStatus[N - 1][1][k], roomStatus[N - 1][2][k]));
	}

	return 0;
}