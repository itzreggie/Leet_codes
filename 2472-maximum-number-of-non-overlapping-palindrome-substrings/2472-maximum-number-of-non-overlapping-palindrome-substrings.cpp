class Solution {
public:
    int maxPalindromes(string s, int k) {
        int n = s.size();

        // Step 1: Precompute palindrome table
        vector<vector<bool>> pal(n, vector<bool>(n, false));

        // odd-length palindromes
        for (int center = 0; center < n; center++) {
            int l = center, r = center;
            while (l >= 0 && r < n && s[l] == s[r]) {
                pal[l][r] = true;
                l--; r++;
            }
        }

        // even-length palindromes
        for (int center = 0; center < n - 1; center++) {
            int l = center, r = center + 1;
            while (l >= 0 && r < n && s[l] == s[r]) {
                pal[l][r] = true;
                l--; r++;
            }
        }

        // Step 2: DP
        vector<int> dp(n + 1, 0);

        for (int i = n - 1; i >= 0; i--) {
            dp[i] = dp[i + 1];  // skip position i

            for (int j = i + k - 1; j < n; j++) {
                if (pal[i][j]) {
                    dp[i] = max(dp[i], 1 + dp[j + 1]);
                }
            }
        }

        return dp[0];
    }
};
