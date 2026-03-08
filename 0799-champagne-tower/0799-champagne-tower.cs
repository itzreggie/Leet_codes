public class Solution {
    public double ChampagneTower(int poured, int query_row, int query_glass) {
        double[][] dp = new double[query_row + 2][];
        for (int i = 0; i < dp.Length; i++)
            dp[i] = new double[query_row + 2];

        dp[0][0] = poured;

        for (int i = 0; i <= query_row; i++) {
            for (int j = 0; j <= i; j++) {
                if (dp[i][j] > 1) {
                    double extra = (dp[i][j] - 1) / 2.0;
                    dp[i + 1][j] += extra;
                    dp[i + 1][j + 1] += extra;
                    dp[i][j] = 1; // cap at full
                }
            }
        }

        return Math.Min(1.0, dp[query_row][query_glass]);
    }
}
