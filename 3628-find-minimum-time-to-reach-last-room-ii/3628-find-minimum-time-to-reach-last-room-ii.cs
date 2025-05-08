using System;
using System.Collections.Generic;

public class Solution {
    public int MinTimeToReach(int[][] moveTime) {
        int n = moveTime.Length, m = moveTime[0].Length;
        // dp[x, y, p] = minimum time to reach (x,y) when the next move costs (if p==0 then 1, if p==1 then 2).
        int[,,] dp = new int[n, m, 2];
        for (int i = 0; i < n; i++){
            for (int j = 0; j < m; j++){
                dp[i, j, 0] = int.MaxValue;
                dp[i, j, 1] = int.MaxValue;
            }
        }
        dp[0, 0, 0] = 0;  // start at (0,0) at time 0, next move cost is 1 second
        
        // Directions: down, up, right, left.
        int[,] dirs = new int[,] { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };
        
        // We'll use a priority queue state: (time, x, y, p)
        var comparer = Comparer<(int time, int x, int y, int p)>.Create((a, b) => {
            int cmp = a.time.CompareTo(b.time);
            if (cmp == 0) {
                cmp = a.x.CompareTo(b.x);
                if (cmp == 0) {
                    cmp = a.y.CompareTo(b.y);
                    if (cmp == 0)
                        cmp = a.p.CompareTo(b.p);
                }
            }
            return cmp;
        });
        var pq = new SortedSet<(int time, int x, int y, int p)>(comparer);
        pq.Add((0, 0, 0, 0));
        
        while (pq.Count > 0) {
            var cur = pq.Min;
            pq.Remove(cur);
            int curTime = cur.time, x = cur.x, y = cur.y, p = cur.p;
            
            if (x == n - 1 && y == m - 1)
                return curTime;
            
            // Skip if we already found a better time for this state.
            if (curTime > dp[x, y, p])
                continue;
            
            // For each adjacent room.
            for (int d = 0; d < 4; d++) {
                int nx = x + dirs[d, 0], ny = y + dirs[d, 1];
                if (nx >= 0 && ny >= 0 && nx < n && ny < m) {
                    // Determine move cost based on current parity.
                    int moveCost = (p == 0) ? 1 : 2;
                    // We can only start moving to (nx, ny) after moveTime[nx][ny].
                    int newTime = Math.Max(curTime, moveTime[nx][ny]) + moveCost;
                    int np = 1 - p; // toggle move cost for the next step.
                    if (newTime < dp[nx, ny, np]) {
                        var oldState = (dp[nx, ny, np], nx, ny, np);
                        pq.Remove(oldState);
                        dp[nx, ny, np] = newTime;
                        pq.Add((newTime, nx, ny, np));
                    }
                }
            }
        }
        
        return -1; // unreachable in valid inputs.
    }
}
