using System;
using System.Collections.Generic;

public class Solution
{
    public int MaxWalls(int[] robots, int[] distance, int[] walls)
    {
        int n = robots.Length;
        int m = walls.Length;

        // Pair robots with distances and sort by position
        var robotsList = new List<(int pos, int dist)>();
        for (int i = 0; i < n; i++)
            robotsList.Add((robots[i], distance[i]));
        robotsList.Sort((a, b) => a.pos.CompareTo(b.pos));

        // Remove walls that are exactly at robot positions (always destroyable)
        var robotSet = new HashSet<int>();
        foreach (var r in robotsList) robotSet.Add(r.pos);

        int baseDestroyed = 0;
        var wallList = new List<int>();
        for (int i = 0; i < m; i++)
        {
            if (robotSet.Contains(walls[i])) baseDestroyed++;
            else wallList.Add(walls[i]);
        }

        wallList.Sort();
        int[] W = wallList.ToArray();
        int mw = W.Length;

        if (n == 0 || mw == 0) return baseDestroyed;

        int[] Rpos = new int[n];
        int[] D = new int[n];
        for (int i = 0; i < n; i++)
        {
            Rpos[i] = robotsList[i].pos;
            D[i] = robotsList[i].dist;
        }

        // Count walls in [L, R] (inclusive)
        int CountInRange(int L, int R)
        {
            if (mw == 0 || L > R) return 0;
            int left = LowerBound(W, L);      // first index >= L
            int right = UpperBound(W, R);     // first index > R
            return Math.Max(0, right - left);
        }

        // Left edge: walls < Rpos[0], reachable by robot 0 shooting left
        int LeftEdgeCount(int i)
        {
            int pos = Rpos[i];
            int L = pos - D[i];
            int R = pos - 1;
            return CountInRange(L, R);
        }

        // Right edge: walls > Rpos[last], reachable by last robot shooting right
        int RightEdgeCount(int i)
        {
            int pos = Rpos[i];
            int L = pos + 1;
            int R = pos + D[i];
            return CountInRange(L, R);
        }

        int segments = n - 1;
        int[] A = new int[segments];   // robot i shooting right into segment i
        int[] B = new int[segments];   // robot i+1 shooting left into segment i
        int[] O = new int[segments];   // overlap in segment i

        for (int i = 0; i < segments; i++)
        {
            int x = Rpos[i];
            int y = Rpos[i + 1];

            // Robot i shooting right: walls in (x, min(x + D[i], y - 1)]
            int rightReach = Math.Min(x + D[i], y - 1);
            int AL = x + 1;
            int AR = rightReach;
            if (AL <= AR) A[i] = CountInRange(AL, AR);
            else A[i] = 0;

            // Robot i+1 shooting left: walls in [max(x + 1, y - D[i+1]), y - 1]
            int leftReachStart = Math.Max(x + 1, y - D[i + 1]);
            int BL = leftReachStart;
            int BR = y - 1;
            if (BL <= BR) B[i] = CountInRange(BL, BR);
            else B[i] = 0;

            // Overlap: intersection of [AL, AR] and [BL, BR]
            int OL = Math.Max(AL, BL);
            int OR = Math.Min(AR, BR);
            if (OL <= OR) O[i] = CountInRange(OL, OR);
            else O[i] = 0;
        }

        // DP: dp[i, dir] where dir=0 (shoot left), 1 (shoot right)
        long[,] dp = new long[n, 2];
        const long NEG = long.MinValue / 4;

        for (int i = 0; i < n; i++)
        {
            dp[i, 0] = dp[i, 1] = NEG;
        }

        // Base for robot 0: only left edge contributes now
        dp[0, 0] = LeftEdgeCount(0); // shoot left
        dp[0, 1] = 0;                // shoot right (segment 0 handled when we process robot 1)

        // Transition over segments
        for (int i = 1; i < n; i++)
        {
            for (int dir = 0; dir < 2; dir++)
            {
                long best = NEG;
                for (int prev = 0; prev < 2; prev++)
                {
                    if (dp[i - 1, prev] == NEG) continue;
                    long add;

                    // Contribution from segment i-1 between robot i-1 and i
                    if (prev == 1 && dir == 0)
                        add = A[i - 1] + B[i - 1] - O[i - 1]; // both shoot into segment
                    else if (prev == 1 && dir != 0)
                        add = A[i - 1];                       // only left robot shoots right
                    else if (prev != 1 && dir == 0)
                        add = B[i - 1];                       // only right robot shoots left
                    else
                        add = 0;                              // none shoot into this segment

                    long cand = dp[i - 1, prev] + add;
                    if (cand > best) best = cand;
                }
                dp[i, dir] = best;
            }
        }

        // Add right edge contribution depending on last robot's direction
        long ans = 0;
        int last = n - 1;
        long opt0 = dp[last, 0]; // last shoots left
        long opt1 = dp[last, 1]; // last shoots right

        if (opt0 != NEG) ans = Math.Max(ans, opt0);
        if (opt1 != NEG) ans = Math.Max(ans, opt1 + RightEdgeCount(last));

        return (int)(ans + baseDestroyed);
    }

    // First index >= value
    private int LowerBound(int[] arr, int value)
    {
        int l = 0, r = arr.Length;
        while (l < r)
        {
            int mid = l + (r - l) / 2;
            if (arr[mid] < value) l = mid + 1;
            else r = mid;
        }
        return l;
    }

    // First index > value
    private int UpperBound(int[] arr, int value)
    {
        int l = 0, r = arr.Length;
        while (l < r)
        {
            int mid = l + (r - l) / 2;
            if (arr[mid] <= value) l = mid + 1;
            else r = mid;
        }
        return l;
    }
}
