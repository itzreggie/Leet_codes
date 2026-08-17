class Solution {
public:
    int stoneGameV(vector<int>& stoneValue) {
        int n = stoneValue.size();
        vector<int> prefix(n + 1, 0);
        for (int i = 0; i < n; i++)
            prefix[i + 1] = prefix[i] + stoneValue[i];

        auto sum = [&](int l, int r) {
            return prefix[r + 1] - prefix[l];
        };

        vector<vector<int>> dp(n, vector<int>(n, 0));

        for (int len = 2; len <= n; len++) {
            for (int l = 0; l + len - 1 < n; l++) {
                int r = l + len - 1;
                int best = 0;

                for (int k = l; k < r; k++) {
                    int left = sum(l, k);
                    int right = sum(k + 1, r);

                    if (left < right)
                        best = max(best, left + dp[l][k]);
                    else if (left > right)
                        best = max(best, right + dp[k + 1][r]);
                    else
                        best = max(best, left + max(dp[l][k], dp[k + 1][r]));
                }

                dp[l][r] = best;
            }
        }

        return dp[0][n - 1];
    }
};
