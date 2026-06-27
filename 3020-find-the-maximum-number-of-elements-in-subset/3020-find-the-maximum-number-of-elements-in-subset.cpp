class Solution {
public:
    int maximumLength(vector<int>& nums) {
        unordered_map<long long,int> freq;
        for (long long v : nums) freq[v]++;

        int ans = 1;

        // Special case: x = 1
        if (freq.count(1)) {
            int c = freq[1];
            if (c >= 1) {
                if (c % 2 == 1) ans = max(ans, c);
                else ans = max(ans, c - 1);
            }
        }

        // For x > 1, build chains of distinct powers: x, x^2, x^4, ...
        for (auto &p : freq) {
            long long x = p.first;
            if (x == 1) continue;

            // collect powers x, x^2, x^4, ... that exist in nums
            vector<long long> powers;
            long long cur = x;
            while (cur <= 1000000000LL && freq.count(cur)) {
                powers.push_back(cur);
                if (cur > 1000000000LL / cur) break; // avoid overflow
                cur = cur * cur;
            }

            int m = powers.size();
            if (m == 0) continue;

            // Try all possible lengths L using first L powers
            for (int L = 1; L <= m; ++L) {
                bool ok = true;

                if (L == 1) {
                    // pattern [x]
                    if (freq[powers[0]] < 1) ok = false;
                } else {
                    // pattern using powers[0..L-1]:
                    // powers[0..L-2] need 2 each, powers[L-1] need 1
                    if (freq[powers[L-1]] < 1) ok = false;
                    for (int i = 0; i < L - 1 && ok; ++i) {
                        if (freq[powers[i]] < 2) ok = false;
                    }
                }

                if (!ok) continue;

                int length = (L == 1 ? 1 : 2 * L - 1);
                ans = max(ans, length);
            }
        }

        return ans;
    }
};
