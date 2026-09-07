
class Solution {
public:
    int distinctSubseqII(string s) {
        const int MOD = 1000000007;
        int n = s.size();

        vector<long long> dp(n + 1, 0);
        dp[0] = 1;  // empty subsequence

        vector<int> last(256, -1);

        for (int i = 1; i <= n; i++) {
            char c = s[i - 1];

            dp[i] = (dp[i - 1] * 2) % MOD;

            if (last[c] != -1) {
                dp[i] = (dp[i] - dp[last[c] - 1] + MOD) % MOD;
            }

            last[c] = i;
        }

        // subtract empty subsequence
        return (dp[n] - 1 + MOD) % MOD;
    }
};
