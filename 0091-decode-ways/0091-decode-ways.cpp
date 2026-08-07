class Solution {
public:
    int numDecodings(string s) {
        int n = s.size();
        vector<int> dp(n + 1, 0);
        dp[n] = 1;  // base case

        for (int i = n - 1; i >= 0; i--) {
            if (s[i] == '0') {
                dp[i] = 0;
                continue;
            }

            // Single digit
            dp[i] = dp[i + 1];

            // Two digits
            if (i + 1 < n) {
                int val = (s[i] - '0') * 10 + (s[i + 1] - '0');
                if (val >= 10 && val <= 26) {
                    dp[i] += dp[i + 2];
                }
            }
        }

        return dp[0];
    }
};
