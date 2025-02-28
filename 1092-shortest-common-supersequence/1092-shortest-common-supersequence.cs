public class Solution {
    public string ShortestCommonSupersequence(string str1, string str2) {
        int m = str1.Length, n = str2.Length;
        var dp = new int[m + 1, n + 1];

        // Fill the dp array
        for (int i = 0; i <= m; i++) {
            for (int j = 0; j <= n; j++) {
                if (i == 0 || j == 0) {
                    dp[i, j] = 0;
                } else if (str1[i - 1] == str2[j - 1]) {
                    dp[i, j] = dp[i - 1, j - 1] + 1;
                } else {
                    dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]);
                }
            }
        }

        // Reconstruct the SCS from the dp array
        int len = dp[m, n];
        StringBuilder sb = new StringBuilder();
        int x = m, y = n;

        while (x > 0 && y > 0) {
            if (str1[x - 1] == str2[y - 1]) {
                sb.Append(str1[x - 1]);
                x--;
                y--;
            } else if (dp[x - 1, y] >= dp[x, y - 1]) {
                sb.Append(str1[x - 1]);
                x--;
            } else {
                sb.Append(str2[y - 1]);
                y--;
            }
        }

        while (x > 0) {
            sb.Append(str1[x - 1]);
            x--;
        }

        while (y > 0) {
            sb.Append(str2[y - 1]);
            y--;
        }

        return new string(sb.ToString().Reverse().ToArray());
    }
}
