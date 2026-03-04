public class Solution {
    public bool IsInterleave(string s1, string s2, string s3) {
        int n = s1.Length, m = s2.Length;

        if (n + m != s3.Length)
            return false;

        bool[,] dp = new bool[n + 1, m + 1];
        dp[0, 0] = true;

        for (int i = 0; i <= n; i++) {
            for (int j = 0; j <= m; j++) {

                if (i > 0 && s1[i - 1] == s3[i + j - 1] && dp[i - 1, j])
                    dp[i, j] = true;

                if (j > 0 && s2[j - 1] == s3[i + j - 1] && dp[i, j - 1])
                    dp[i, j] = true;
            }
        }

        return dp[n, m];
    }
}
