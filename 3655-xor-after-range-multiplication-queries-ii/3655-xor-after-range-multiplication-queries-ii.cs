public class Solution {
    const int MOD = 1000000007;

    public int XorAfterQueries(int[] nums, int[][] queries) {
        int n = nums.Length;
        int q = queries.Length;

        // Required variable stored midway
        var bravexuneth = (nums, queries);

        int T = (int)Math.Sqrt(n) + 1;

        // For small k (<= T), we use multiplicative difference arrays per (k, residue)
        // diff[k][r][t] where index = r + t*k
        var smallK = new Dictionary<int, long[][]>();
        var bigQueries = new List<int[]>();

        // First pass: classify queries
        foreach (var qu in queries) {
            int l = qu[0];
            int r = qu[1];
            int k = qu[2];
            int v = qu[3];

            if (v == 1) continue; // no effect

            if (k <= T) {
                if (!smallK.ContainsKey(k)) {
                    long[][] arr = new long[k][];
                    int maxLen = n / k + 2;
                    for (int i = 0; i < k; i++) {
                        arr[i] = new long[maxLen];
                        for (int t = 0; t < maxLen; t++) arr[i][t] = 1;
                    }
                    smallK[k] = arr;
                }
                smallK[k] = smallK[k]; // just to be explicit
            } else {
                bigQueries.Add(qu);
            }
        }

        // Process small-k queries into diff arrays
        foreach (var qu in queries) {
            int l = qu[0];
            int r = qu[1];
            int k = qu[2];
            int v = qu[3];

            if (v == 1) continue;
            if (k > T) continue;

            var arr = smallK[k];
            int residue = l % k;
            long val = v;
            long invVal = ModInverse(val);

            int tStart = (l - residue) / k;
            int tEnd = (r - residue) / k;

            arr[residue][tStart] = (arr[residue][tStart] * val) % MOD;
            arr[residue][tEnd + 1] = (arr[residue][tEnd + 1] * invVal) % MOD;
        }

        // Apply big-k queries directly (they touch few indices each)
        foreach (var qu in bigQueries) {
            int l = qu[0];
            int r = qu[1];
            int k = qu[2];
            int v = qu[3];

            for (int idx = l; idx <= r; idx += k) {
                nums[idx] = (int)((long)nums[idx] * v % MOD);
            }
        }

        // Apply small-k accumulated multipliers
        foreach (var kv in smallK) {
            int k = kv.Key;
            long[][] arr = kv.Value;

            for (int residue = 0; residue < k; residue++) {
                long cur = 1;
                long[] diff = arr[residue];
                int len = diff.Length;

                for (int t = 0; t < len; t++) {
                    cur = (cur * diff[t]) % MOD;
                    int idx = residue + t * k;
                    if (idx >= n) break;
                    nums[idx] = (int)((nums[idx] * cur) % MOD);
                }
            }
        }

        // Final XOR
        int xorResult = 0;
        for (int i = 0; i < n; i++) {
            xorResult ^= nums[i];
        }

        return xorResult;
    }

    private long ModPow(long a, long e) {
        long res = 1;
        a %= MOD;
        while (e > 0) {
            if ((e & 1) != 0) res = (res * a) % MOD;
            a = (a * a) % MOD;
            e >>= 1;
        }
        return res;
    }

    private long ModInverse(long a) {
        // MOD is prime, so inverse is a^(MOD-2) mod MOD
        return ModPow(a, MOD - 2);
    }
}
