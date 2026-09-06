class Solution {
public:
    int numDistinct(string s, string t) {
        int n = s.size(), m = t.size();
        if (m > n) return 0;

        // dp[j] = number of ways to form t[0..j-1]
        vector<long long> dp(m + 1, 0);
        dp[0] = 1; // empty t

        for (int i = 1; i <= n; i++) {
            for (int j = m; j >= 1; j--) {
                if (s[i - 1] == t[j - 1]) {
                    dp[j] += dp[j - 1];
                    if (dp[j] > INT_MAX) dp[j] = INT_MAX;  // clamp to avoid overflow
                }
            }
        }

        return (int)dp[m];
    }
};
