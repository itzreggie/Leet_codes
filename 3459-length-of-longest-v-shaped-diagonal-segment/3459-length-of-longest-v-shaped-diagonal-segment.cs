using System;

public class Solution
{
    // Directions:
    // 0: ↘ (dx=+1, dy=+1)
    // 1: ↖ (dx=-1, dy=-1)
    // 2: ↙ (dx=+1, dy=-1)
    // 3: ↗ (dx=-1, dy=+1)
    private static readonly int[] DX = { 1, -1, 1, -1 };
    private static readonly int[] DY = { 1, -1, -1, 1 };

    // steps[dir, expIdx, i, j]:
    // expIdx = 0 means expected next is 2, expIdx = 1 means expected next is 0
    private int[,,,] steps;

    public int LenOfVDiagonal(int[][] grid)
    {
        int n = grid.Length;
        int m = grid[0].Length;
        steps = new int[4, 2, n, m];

        // Precompute alternating-run lengths for each direction and expected value.
        PrecomputeDir0(grid); // ↘
        PrecomputeDir1(grid); // ↖
        PrecomputeDir2(grid); // ↙
        PrecomputeDir3(grid); // ↗

        int best = 0;

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (grid[i][j] != 1) continue; // must start with 1

                for (int dir = 0; dir < 4; dir++)
                {
                    // First leg without turning
                    int lenA = steps[dir, 0, i, j]; // starting expected next = 2
                    best = Math.Max(best, 1 + lenA);

                    // Try turning clockwise at any step along the first leg
                    int x = i, y = j;
                    int expIdx = 0; // 0 => expect 2 next; 1 => expect 0 next

                    int turnDir = GetClockwise(dir);

                    for (int t = 0; t < lenA; t++)
                    {
                        // Move one step in first direction (this step is guaranteed valid by lenA)
                        x += DX[dir];
                        y += DY[dir];

                        // After taking t+1 steps, toggle expected
                        expIdx ^= 1;

                        // Second leg from current position, along turnDir, continuing sequence
                        int lenB = steps[turnDir, expIdx, x, y];

                        best = Math.Max(best, 1 + (t + 1) + lenB);
                    }
                }
            }
        }

        return best;
    }

    private static int GetClockwise(int dir)
    {
        // Clockwise mapping over diagonals:
        // 0 (↘) -> 2 (↙)
        // 2 (↙) -> 1 (↖)
        // 1 (↖) -> 3 (↗)
        // 3 (↗) -> 0 (↘)
        switch (dir)
        {
            case 0: return 2;
            case 2: return 1;
            case 1: return 3;
            case 3: return 0;
            default: return 0;
        }
    }

    // For each PrecomputeDirX: we fill steps[dir, expIdx, i, j]
    // steps[dir, 0, i, j] = (grid[next] == 2) ? 1 + steps[dir, 1, next] : 0
    // steps[dir, 1, i, j] = (grid[next] == 0) ? 1 + steps[dir, 0, next] : 0

    private void PrecomputeDir0(int[][] grid)
    {
        int n = grid.Length, m = grid[0].Length;
        int dir = 0; // ↘ next is (i+1, j+1)
        for (int i = n - 1; i >= 0; i--)
        {
            for (int j = m - 1; j >= 0; j--)
            {
                int nx = i + 1, ny = j + 1;
                if (nx < n && ny < m)
                {
                    steps[dir, 0, i, j] = (grid[nx][ny] == 2) ? 1 + steps[dir, 1, nx, ny] : 0;
                    steps[dir, 1, i, j] = (grid[nx][ny] == 0) ? 1 + steps[dir, 0, nx, ny] : 0;
                }
                else
                {
                    steps[dir, 0, i, j] = 0;
                    steps[dir, 1, i, j] = 0;
                }
            }
        }
    }

    private void PrecomputeDir1(int[][] grid)
    {
        int n = grid.Length, m = grid[0].Length;
        int dir = 1; // ↖ next is (i-1, j-1)
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                int nx = i - 1, ny = j - 1;
                if (nx >= 0 && ny >= 0)
                {
                    steps[dir, 0, i, j] = (grid[nx][ny] == 2) ? 1 + steps[dir, 1, nx, ny] : 0;
                    steps[dir, 1, i, j] = (grid[nx][ny] == 0) ? 1 + steps[dir, 0, nx, ny] : 0;
                }
                else
                {
                    steps[dir, 0, i, j] = 0;
                    steps[dir, 1, i, j] = 0;
                }
            }
        }
    }

    private void PrecomputeDir2(int[][] grid)
    {
        int n = grid.Length, m = grid[0].Length;
        int dir = 2; // ↙ next is (i+1, j-1)
        for (int i = n - 1; i >= 0; i--)
        {
            for (int j = 0; j < m; j++)
            {
                int nx = i + 1, ny = j - 1;
                if (nx < n && ny >= 0)
                {
                    steps[dir, 0, i, j] = (grid[nx][ny] == 2) ? 1 + steps[dir, 1, nx, ny] : 0;
                    steps[dir, 1, i, j] = (grid[nx][ny] == 0) ? 1 + steps[dir, 0, nx, ny] : 0;
                }
                else
                {
                    steps[dir, 0, i, j] = 0;
                    steps[dir, 1, i, j] = 0;
                }
            }
        }
    }

    private void PrecomputeDir3(int[][] grid)
    {
        int n = grid.Length, m = grid[0].Length;
        int dir = 3; // ↗ next is (i-1, j+1)
        for (int i = 0; i < n; i++)
        {
            for (int j = m - 1; j >= 0; j--)
            {
                int nx = i - 1, ny = j + 1;
                if (nx >= 0 && ny < m)
                {
                    steps[dir, 0, i, j] = (grid[nx][ny] == 2) ? 1 + steps[dir, 1, nx, ny] : 0;
                    steps[dir, 1, i, j] = (grid[nx][ny] == 0) ? 1 + steps[dir, 0, nx, ny] : 0;
                }
                else
                {
                    steps[dir, 0, i, j] = 0;
                    steps[dir, 1, i, j] = 0;
                }
            }
        }
    }
}
