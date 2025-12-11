using System;
using System.Collections.Generic;

public class Solution {
    public int CountCoveredBuildings(int n, int[][] buildings) {
        // Group buildings by row and by column
        var rowMap = new Dictionary<int, SortedSet<int>>();
        var colMap = new Dictionary<int, SortedSet<int>>();

        foreach (var b in buildings) {
            int x = b[0], y = b[1];

            if (!rowMap.ContainsKey(x)) rowMap[x] = new SortedSet<int>();
            rowMap[x].Add(y);

            if (!colMap.ContainsKey(y)) colMap[y] = new SortedSet<int>();
            colMap[y].Add(x);
        }

        int coveredCount = 0;

        foreach (var b in buildings) {
            int x = b[0], y = b[1];

            // Check row (left/right)
            var rowSet = rowMap[x];
            bool hasLeft = rowSet.Min < y;   // at least one smaller y
            bool hasRight = rowSet.Max > y;  // at least one larger y

            // Check column (above/below)
            var colSet = colMap[y];
            bool hasAbove = colSet.Min < x;  // at least one smaller x
            bool hasBelow = colSet.Max > x;  // at least one larger x

            if (hasAbove && hasBelow && hasLeft && hasRight) {
                coveredCount++;
            }
        }

        return coveredCount;
    }
}
