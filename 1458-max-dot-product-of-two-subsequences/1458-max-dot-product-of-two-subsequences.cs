public class Solution {
    public int MaxDotProduct(int[] nums1, int[] nums2) {
        int n = nums1.Length;
        int m = nums2.Length;

        // dp[i][j] = max dot product using nums1[0..i-1] and nums2[0..j-1]
        int[,] dp = new int[n + 1, m + 1];

        // Initialize with very small values because dot product can be negative
        for (int i = 0; i <= n; i++)
            for (int j = 0; j <= m; j++)
                dp[i, j] = int.MinValue / 2;

        for (int i = 1; i <= n; i++) {
            for (int j = 1; j <= m; j++) {
                int product = nums1[i - 1] * nums2[j - 1];

                dp[i, j] = Math.Max(dp[i, j], product); // start new subsequence
                dp[i, j] = Math.Max(dp[i, j], dp[i - 1, j - 1] + product); // extend subsequence
                dp[i, j] = Math.Max(dp[i, j], dp[i - 1, j]); // skip nums1[i-1]
                dp[i, j] = Math.Max(dp[i, j], dp[i, j - 1]); // skip nums2[j-1]
            }
        }

        return dp[n, m];
    }
}
