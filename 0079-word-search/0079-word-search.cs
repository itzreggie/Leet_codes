using System;
using System.Collections.Generic;

public class Solution
{
    public int MinCost(int[][] grid, int k)
    {
        int m = grid.Length;
        int n = grid[0].Length;

        // dist[row][col][usedTeleports]
        int[,,] dist = new int[m, n, k + 1];
        for (int i = 0; i < m; i++)
            for (int j = 0; j < n; j++)
                for (int t = 0; t <= k; t++)
                    dist[i, j, t] = int.MaxValue;

        // Min-heap: (cost, row, col, usedTeleports)
        var pq = new PriorityQueue<(int cost, int r, int c, int t), int>();

        dist[0, 0, 0] = 0;
        pq.Enqueue((0, 0, 0, 0), 0);

        int[] dr = { 0, 1 };
        int[] dc = { 1, 0 };

        while (pq.Count > 0)
        {
            var (cost, r, c, t) = pq.Dequeue();

            if (cost > dist[r, c, t]) continue;

            // Reached destination
            if (r == m - 1 && c == n - 1)
                return cost;

            // Normal moves: right & down
            for (int d = 0; d < 2; d++)
            {
                int nr = r + dr[d];
                int nc = c + dc[d];

                if (nr < m && nc < n)
                {
                    int newCost = cost + grid[nr][nc];
                    if (newCost < dist[nr, nc, t])
                    {
                        dist[nr, nc, t] = newCost;
                        pq.Enqueue((newCost, nr, nc, t), newCost);
                    }
                }
            }

            // Teleport
            if (t < k)
            {
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (grid[i][j] <= grid[r][c])
                        {
                            if (cost < dist[i, j, t + 1])
                            {
                                dist[i, j, t + 1] = cost;
                                pq.Enqueue((cost, i, j, t + 1), cost);
                            }
                        }
                    }
                }
            }
        }

        return -1;
    }
}
