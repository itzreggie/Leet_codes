public class Solution {
    public int MinimumDeleteSum(string s1, string s2) {
        int n = s1.Length;
        int m = s2.Length;

        // dp[i][j] = max ASCII sum of common subsequence of s1[0..i-1], s2[0..j-1]
        int[,] dp = new int[n + 1, m + 1];

        for (int i = 1; i <= n; i++) {
            for (int j = 1; j <= m; j++) {
                if (s1[i - 1] == s2[j - 1]) {
                    dp[i, j] = dp[i - 1, j - 1] + s1[i - 1];
                } else {
                    dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]);
                }
            }
        }

        // Total ASCII sum of both strings
        int total = 0;
        foreach (char c in s1) total += c;
        foreach (char c in s2) total += c;

        // Subtract twice the ASCII sum of the common subsequence
        return total - 2 * dp[n, m];
    }
}
