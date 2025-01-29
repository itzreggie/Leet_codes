public class Solution {
    public string LongestPalindrome(string s) {
        if (string.IsNullOrEmpty(s)) return "";
        int n = s.Length;
        bool[,] dp = new bool[n, n];
        int maxLength = 1;
        int start = 0;

        for (int i = 0; i < n; i++) {
            dp[i, i] = true; 
        }

        for (int i = 0; i < n - 1; i++) {
            if (s[i] == s[i + 1]) {
                dp[i, i + 1] = true;
                start = i;
                maxLength = 2;
            }
        }

        for (int length = 3; length <= n; length++) {
            for (int i = 0; i < n - length + 1; i++) {
                int j = i + length - 1;
                if (dp[i + 1, j - 1] && s[i] == s[j]) {
                    dp[i, j] = true;
                    start = i;
                    maxLength = length;
                }
            }
        }

        return s.Substring(start, maxLength);
    }
}
