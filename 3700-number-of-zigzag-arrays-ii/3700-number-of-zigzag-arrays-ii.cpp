class Solution {
public:
    static const int mod = 1'000'000'007;

    using Mat = vector<vector<long long>>;

    Mat mul(const Mat &A, const Mat &B) {
        int n = A.size();
        Mat C(n, vector<long long>(n, 0));
        for (int i = 0; i < n; ++i)
            for (int k = 0; k < n; ++k) if (A[i][k])
                for (int j = 0; j < n; ++j)
                    C[i][j] = (C[i][j] + A[i][k] * B[k][j]) % mod;
        return C;
    }

    Mat mpow(Mat base, long long exp) {
        int n = base.size();
        Mat res(n, vector<long long>(n, 0));
        for (int i = 0; i < n; ++i) res[i][i] = 1;

        while (exp > 0) {
            if (exp & 1) res = mul(res, base);
            base = mul(base, base);
            exp >>= 1;
        }
        return res;
    }

    int zigZagArrays(int n, int l, int r) {
        int m = r - l + 1;

        if (n == 1) return m;

        // 2m states: (value, direction)
        // direction: 0 = UP, 1 = DOWN
        int S = 2 * m;

        Mat M(S, vector<long long>(S, 0));

        // Build transition matrix
        for (int v = 0; v < m; ++v) {
            int up = v;         // state index for (v, UP)
            int down = v + m;   // state index for (v, DOWN)

            // From UP → must go DOWN → next value < v
            for (int nv = 0; nv < v; ++nv) {
                M[up][nv + m] = 1;
            }

            // From DOWN → must go UP → next value > v
            for (int nv = v + 1; nv < m; ++nv) {
                M[down][nv] = 1;
            }
        }

        // Initial dp(1): all states = 1
        vector<long long> dp1(S, 1);

        // Compute M^(n-1)
        Mat Mn = mpow(M, n - 1);

        // Multiply Mn * dp1
        long long ans = 0;
        for (int i = 0; i < S; ++i) {
            long long sum = 0;
            for (int j = 0; j < S; ++j)
                sum = (sum + Mn[i][j] * dp1[j]) % mod;
            ans = (ans + sum) % mod;
        }

        return ans;
    }
};
