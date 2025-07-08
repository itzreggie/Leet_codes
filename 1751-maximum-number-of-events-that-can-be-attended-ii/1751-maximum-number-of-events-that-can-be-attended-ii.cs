using System;
using System.Collections.Generic;

public class Solution {
    public int MaxValue(int[][] events, int k) {
        // Sort events by start day
        Array.Sort(events, (a, b) => a[0].CompareTo(b[0]));
        int n = events.Length;

        // Memoization table: dp[eventIndex][k]
        var dp = new int[n + 1, k + 1];

        // Precompute next possible event index (non-overlapping)
        int[] nextIdx = new int[n];
        for (int i = 0; i < n; i++) {
            int lo = i + 1, hi = n;
            while (lo < hi) {
                int mid = (lo + hi) / 2;
                if (events[mid][0] > events[i][1])
                    hi = mid;
                else
                    lo = mid + 1;
            }
            nextIdx[i] = lo;
        }

        // Bottom-up DP
        for (int i = n - 1; i >= 0; i--) {
            for (int j = 1; j <= k; j++) {
                // Option 1: Skip current event
                int skip = dp[i + 1, j];

                // Option 2: Take current event
                int take = events[i][2] + dp[nextIdx[i], j - 1];

                dp[i, j] = Math.Max(skip, take);
            }
        }

        return dp[0, k];
    }
}
