using System;
using System.Collections.Generic;

public class Solution
{
    public bool CanPartitionGrid(int[][] grid)
    {
        int m = grid.Length;
        int n = grid[0].Length;

        // 1D cases: single row or single column
        if (m == 1 && n == 1) return false;
        if (m == 1) return CanPartitionSingleRow(grid[0]);
        if (n == 1) return CanPartitionSingleColumn(grid);

        long totalSum = 0;
        var globalFreq = new Dictionary<int, int>();

        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                int v = grid[i][j];
                totalSum += v;
                if (!globalFreq.ContainsKey(v)) globalFreq[v] = 0;
                globalFreq[v]++;
            }
        }

        // Horizontal cuts
        {
            long sumTop = 0;
            var freqTop = new Dictionary<int, int>();

            for (int r = 0; r < m - 1; r++)
            {
                // add row r to top
                for (int j = 0; j < n; j++)
                {
                    int v = grid[r][j];
                    sumTop += v;
                    if (!freqTop.ContainsKey(v)) freqTop[v] = 0;
                    freqTop[v]++;
                }

                long sumBottom = totalSum - sumTop;
                if (sumTop == sumBottom) return true;

                long diff = Math.Abs(sumTop - sumBottom);
                if (diff == 0) continue;

                // diff must match some cell value; if too large, skip
                if (diff > int.MaxValue) continue;
                int val = (int)diff;

                if (sumTop > sumBottom)
                {
                    int topRows = r + 1;
                    if (topRows >= 2)
                    {
                        if (freqTop.TryGetValue(val, out int cnt) && cnt > 0)
                            return true;
                    }
                    else // topRows == 1: only row 0, endpoints only
                    {
                        if (grid[0][0] == val || grid[0][n - 1] == val)
                            return true;
                    }
                }
                else
                {
                    int bottomRows = m - (r + 1);
                    globalFreq.TryGetValue(val, out int globalCnt);
                    freqTop.TryGetValue(val, out int topCnt);
                    int bottomCnt = globalCnt - topCnt;

                    if (bottomRows >= 2)
                    {
                        if (bottomCnt > 0) return true;
                    }
                    else // bottomRows == 1: only row m-1, endpoints only
                    {
                        if (grid[m - 1][0] == val || grid[m - 1][n - 1] == val)
                            return true;
                    }
                }
            }
        }

        // Vertical cuts
        {
            long sumLeft = 0;
            var freqLeft = new Dictionary<int, int>();

            for (int c = 0; c < n - 1; c++)
            {
                // add column c to left
                for (int i = 0; i < m; i++)
                {
                    int v = grid[i][c];
                    sumLeft += v;
                    if (!freqLeft.ContainsKey(v)) freqLeft[v] = 0;
                    freqLeft[v]++;
                }

                long sumRight = totalSum - sumLeft;
                if (sumLeft == sumRight) return true;

                long diff = Math.Abs(sumLeft - sumRight);
                if (diff == 0) continue;

                if (diff > int.MaxValue) continue;
                int val = (int)diff;

                if (sumLeft > sumRight)
                {
                    int leftCols = c + 1;
                    if (leftCols >= 2)
                    {
                        if (freqLeft.TryGetValue(val, out int cnt) && cnt > 0)
                            return true;
                    }
                    else // leftCols == 1: only column 0, endpoints only
                    {
                        if (grid[0][0] == val || grid[m - 1][0] == val)
                            return true;
                    }
                }
                else
                {
                    int rightCols = n - (c + 1);
                    globalFreq.TryGetValue(val, out int globalCnt);
                    freqLeft.TryGetValue(val, out int leftCnt);
                    int rightCnt = globalCnt - leftCnt;

                    if (rightCols >= 2)
                    {
                        if (rightCnt > 0) return true;
                    }
                    else // rightCols == 1: only column n-1, endpoints only
                    {
                        if (grid[0][n - 1] == val || grid[m - 1][n - 1] == val)
                            return true;
                    }
                }
            }
        }

        return false;
    }

    private bool CanPartitionSingleRow(int[] row)
    {
        int n = row.Length;
        long total = 0;
        for (int i = 0; i < n; i++) total += row[i];

        long prefix = 0;
        for (int c = 0; c < n - 1; c++)
        {
            prefix += row[c];
            long leftSum = prefix;
            long rightSum = total - prefix;

            if (leftSum == rightSum) return true;

            long diff = Math.Abs(leftSum - rightSum);
            if (diff == 0) continue;
            if (diff > int.MaxValue) continue;
            int val = (int)diff;

            if (leftSum > rightSum)
            {
                // heavier side is left: allowed endpoints are index 0 and c
                if (row[0] == val || row[c] == val) return true;
            }
            else
            {
                // heavier side is right: allowed endpoints are index c+1 and n-1
                if (row[c + 1] == val || row[n - 1] == val) return true;
            }
        }
        return false;
    }

    private bool CanPartitionSingleColumn(int[][] grid)
    {
        int m = grid.Length;
        long total = 0;
        for (int i = 0; i < m; i++) total += grid[i][0];

        long prefix = 0;
        for (int r = 0; r < m - 1; r++)
        {
            prefix += grid[r][0];
            long topSum = prefix;
            long bottomSum = total - prefix;

            if (topSum == bottomSum) return true;

            long diff = Math.Abs(topSum - bottomSum);
            if (diff == 0) continue;
            if (diff > int.MaxValue) continue;
            int val = (int)diff;

            if (topSum > bottomSum)
            {
                // heavier side is top: endpoints are row 0 and r
                if (grid[0][0] == val || grid[r][0] == val) return true;
            }
            else
            {
                // heavier side is bottom: endpoints are row r+1 and m-1
                if (grid[r + 1][0] == val || grid[m - 1][0] == val) return true;
            }
        }
        return false;
    }
}
