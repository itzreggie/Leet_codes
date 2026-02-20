public class Solution {
    public int UniquePaths(int m, int n) {
        int[] dp = new int[n];

        // Initialize first row with 1s
        for (int j = 0; j < n; j++) {
            dp[j] = 1;
        }

        // Fill the DP array row by row
        for (int i = 1; i < m; i++) {
            for (int j = 1; j < n; j++) {
                dp[j] += dp[j - 1];
            }
        }

        return dp[n - 1];
    }
}
