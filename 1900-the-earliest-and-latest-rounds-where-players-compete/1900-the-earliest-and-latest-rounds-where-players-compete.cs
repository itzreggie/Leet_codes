using System;
using System.Collections.Generic;

public class Solution {
    // Memoization cache: key = packed (l,r,k), value = [earliest, latest]
    private Dictionary<int,int[]> _memo;

    public int[] EarliestAndLatest(int n, int firstPlayer, int secondPlayer) {
        _memo = new Dictionary<int,int[]>();
        // Convert secondPlayer into its “r” position from the end
        int l = firstPlayer;
        int r = n - secondPlayer + 1;
        return Solve(l, r, n);
    }

    private int[] Solve(int l, int r, int k) {
        // If they meet this round
        if (l == r) 
            return new[] { 1, 1 };

        // Ensure l <= r
        if (l > r) 
            return Solve(r, l, k);

        // Pack (l, r, k) into a single int key: l<<16 | r<<8 | k
        int key = (l << 16) | (r << 8) | k;
        if (_memo.TryGetValue(key, out var cached))
            return cached;

        int pairs = k / 2;
        int nextK = (k + 1) / 2;

        int earliest = int.MaxValue;
        int latest   = int.MinValue;

        // Lower bound for i+j
        int low = l + r - pairs;

        // Enumerate how many winners come from the left (i) and right (j) sides
        for (int i = 1; i <= l; i++) {
            // j must satisfy:
            //   i + j >= low
            //   i + j <= nextK
            //   j >= l - i + 1
            //   j <= r - i
            int jMin = l - i + 1;
            int tmp   = low - i;
            if (tmp > jMin) jMin = tmp;

            int jMax = r - i;
            int tmp2 = nextK - i;
            if (tmp2 < jMax) jMax = tmp2;

            if (jMin > jMax) 
                continue;

            for (int j = jMin; j <= jMax; j++) {
                var sub = Solve(i, j, nextK);
                earliest = Math.Min(earliest, sub[0] + 1);
                latest   = Math.Max(latest,   sub[1] + 1);
            }
        }

        var result = new[] { earliest, latest };
        _memo[key] = result;
        return result;
    }
}
