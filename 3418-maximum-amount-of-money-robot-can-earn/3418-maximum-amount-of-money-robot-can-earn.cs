public class Solution {
    public int MaximumAmount(int[][] coins) {
        int m = coins.Length;
        int n = coins[0].Length;

        // dp[i][j][k] = max coins at cell (i,j) using k neutralizations
        int[,,] dp = new int[m, n, 3];

        // Initialize with very negative values
        for (int i = 0; i < m; i++)
            for (int j = 0; j < n; j++)
                for (int k = 0; k < 3; k++)
                    dp[i, j, k] = int.MinValue / 4;

        // Starting point
        for (int k = 0; k < 3; k++)
            dp[0, 0, k] = coins[0][0];

        // If start is a robber cell, we can neutralize it
        if (coins[0][0] < 0) {
            dp[0, 0, 1] = 0; // neutralize
        }

        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                if (i == 0 && j == 0) continue;

                for (int k = 0; k < 3; k++) {
                    int bestPrev = int.MinValue / 4;

                    // From top
                    if (i > 0)
                        bestPrev = Math.Max(bestPrev, dp[i - 1, j, k]);

                    // From left
                    if (j > 0)
                        bestPrev = Math.Max(bestPrev, dp[i, j - 1, k]);

                    if (bestPrev == int.MinValue / 4) continue;

                    int cell = coins[i][j];

                    // Case 1: Do NOT neutralize
                    int gain = cell >= 0 ? cell : cell; // negative means losing coins
                    dp[i, j, k] = Math.Max(dp[i, j, k], bestPrev + gain);

                    // Case 2: Neutralize robber (if negative and k > 0)
                    if (cell < 0 && k > 0) {
                        dp[i, j, k] = Math.Max(dp[i, j, k], 
                            (i > 0 ? dp[i - 1, j, k - 1] : int.MinValue / 4));
                        dp[i, j, k] = Math.Max(dp[i, j, k], 
                            (j > 0 ? dp[i, j - 1, k - 1] : int.MinValue / 4));
                    }
                }
            }
        }

        // Best among using 0, 1, or 2 neutralizations
        return Math.Max(dp[m - 1, n - 1, 0],
               Math.Max(dp[m - 1, n - 1, 1], dp[m - 1, n - 1, 2]));
    }
}
