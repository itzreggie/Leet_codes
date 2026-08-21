class Solution {
public:
    long long lcm(long long a, long long b) {
        return a / std::gcd(a, b) * b;
    }

    long long countDistinct(long long x, const vector<int>& coins) {
        int m = coins.size();
        long long total = 0;

        // iterate over all non-empty subsets
        for (int mask = 1; mask < (1 << m); mask++) {
            long long L = 1;
            int bits = 0;
            bool ok = true;

            for (int i = 0; i < m; i++) {
                if (mask & (1 << i)) {
                    bits++;
                    L = lcm(L, coins[i]);
                    if (L > x) { // no multiples ≤ x
                        ok = false;
                        break;
                    }
                }
            }

            if (!ok) continue;

            long long cnt = x / L;
            if (bits % 2 == 1) total += cnt;  // odd subset size → add
            else total -= cnt;                // even subset size → subtract
        }

        return total;
    }

    long long findKthSmallest(vector<int>& coins, int k) {
        sort(coins.begin(), coins.end());
        long long left = 1;
        long long right = 1LL * coins.back() * k; // safe upper bound

        while (left < right) {
            long long mid = (left + right) / 2;
            if (countDistinct(mid, coins) >= k)
                right = mid;
            else
                left = mid + 1;
        }

        return left;
    }
};
