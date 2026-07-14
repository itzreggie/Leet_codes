class Solution {
public:
    static const int MOD = 1'000'000'007;

    int subsequencePairCount(vector<int>& nums) {
        using ll = long long;
        int n = nums.size();

        // dp[(g1,g2)] = number of ways so far where
        // gcd(seq1) = g1, gcd(seq2) = g2
        // g1 = 0 means seq1 is empty so far
        // g2 = 0 means seq2 is empty so far
        unordered_map<long long, ll> dp;
        auto pack = [](int g1, int g2) -> long long {
            return ( (long long)g1 << 32 ) | (unsigned long long)g2;
        };

        dp[pack(0, 0)] = 1;

        for (int x : nums) {
            unordered_map<long long, ll> next = dp;

            for (auto &it : dp) {
                long long key = it.first;
                ll ways = it.second;

                int g1 = (int)(key >> 32);
                int g2 = (int)(key & 0xffffffffu);

                // put x into seq1
                int ng1 = (g1 == 0 ? x : std::gcd(g1, x));
                int ng2 = g2;
                next[pack(ng1, ng2)] = (next[pack(ng1, ng2)] + ways) % MOD;

                // put x into seq2
                ng1 = g1;
                ng2 = (g2 == 0 ? x : std::gcd(g2, x));
                next[pack(ng1, ng2)] = (next[pack(ng1, ng2)] + ways) % MOD;

                // (we already handled "neither" by copying dp into next)
            }

            dp.swap(next);
        }

        ll ans = 0;
        for (auto &it : dp) {
            long long key = it.first;
            ll ways = it.second;

            int g1 = (int)(key >> 32);
            int g2 = (int)(key & 0xffffffffu);

            // both subsequences non-empty and gcds equal
            if (g1 > 0 && g2 > 0 && g1 == g2) {
                ans = (ans + ways) % MOD;
            }
        }

        return (int)ans;
    }
};
