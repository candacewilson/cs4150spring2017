// GetShortyRevised.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include <iomanip>
#include <vector>
#include <set>
#include <algorithm>

using namespace std;
typedef pair<int, double> edge;
typedef vector<vector<edge>> Dungeon;

int main() 
{
	int n, m;
	int x, y;
	double factor;
	vector<double> results;

	while (cin >> n >> m) 
	{
		if (n == 0 && m == 0)
		{
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

		auto comparator = [](const pair<int, double>& x, const pair<int, double>& y) {
			return x.second > y.second || (x.second == y.second && x.first > y.first);
		};

		set<pair<int, double>, decltype(comparator)> queue(comparator);
		queue.insert(make_pair(0, 1));

		while (!(queue.empty())) 
		{
			auto y = queue.begin()->first;
			queue.erase(queue.begin());

			for (auto intersectionCorridor : dungeon[y]) 
			{
				auto y = intersectionCorridor.first;
				auto fraction = maxFactor[y] * intersectionCorridor.second;

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

	for (auto value : results)
	{
		cout << fixed << setprecision(4) << value << endl;
	}

	return 0;
}

