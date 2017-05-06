// GetShortyRevised.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include <iomanip>
#include <vector>
#include <set>
#include <algorithm>

using namespace std;
typedef pair<long, double> corridor;
typedef vector<vector<corridor>> Dungeon;

int main() 
{
	long n, m;
	long x, y;
	double factor;
	vector<double> results;

	while (cin >> n >> m) 
	{
		if (n == 0 && m == 0)
		{
			for (auto value : results)
			{
				cout << fixed << setprecision(4) << value << endl;
			}

			break;
		}

		Dungeon dungeon(n);

		for (int i = 0; i < m; i++)
		{
			cin >> x >> y >> factor;

			dungeon[x].push_back(make_pair(y, factor));
			dungeon[y].push_back(make_pair(x, factor));
		}

		vector<double> maxFactor(n, numeric_limits<double>::min());
		maxFactor[0] = 1;

		auto intersectionCorridorComparator = [](const pair<long, double>& x, const pair<long, double>& y) {
			return x.second > y.second || (x.second == y.second && x.first > y.first);
		};

		set<pair<long, double>, decltype(intersectionCorridorComparator)> queue(intersectionCorridorComparator);
		queue.insert(make_pair(0, 1));

		while (!queue.empty()) 
		{
			auto x = queue.begin()->first;
			queue.erase(queue.begin());

			for (auto intersectionCorridor : dungeon[x]) 
			{
				auto y = intersectionCorridor.first;
				auto fraction = maxFactor[x] * intersectionCorridor.second;

				if (fraction > maxFactor[y]) 
				{
					queue.erase(make_pair(y, maxFactor[y]));
					queue.insert(make_pair(y, fraction));
					maxFactor[y] = fraction;
				}
			}
		}

		results.push_back(maxFactor[n - 1]);
	}

	return 0;
}