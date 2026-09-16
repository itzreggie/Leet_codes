class Solution {
public:
    static const int MOD = 1000000007;

    long long modPow(long long a, long long e) {
        long long r = 1;
        while (e) {
            if (e & 1) r = r * a % MOD;
            a = a * a % MOD;
            e >>= 1;
        }
        return r;
    }

    int numberOfSets(int n, int k) {
        int maxN = n + k - 1;          // for n+k-1
        vector<long long> fact(maxN + 1), invFact(maxN + 1);

        fact[0] = 1;
        for (int i = 1; i <= maxN; i++)
            fact[i] = fact[i - 1] * i % MOD;

        invFact[maxN] = modPow(fact[maxN], MOD - 2);
        for (int i = maxN; i > 0; i--)
            invFact[i - 1] = invFact[i] * i % MOD;

        auto C = [&](int a, int b) -> long long {
            if (b < 0 || b > a) return 0;
            return fact[a] * invFact[b] % MOD * invFact[a - b] % MOD;
        };

        return (int)C(n + k - 1, 2 * k);
    }
};
