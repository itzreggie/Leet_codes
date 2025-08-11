public class Solution {
    private const int MOD = 1_000_000_007;

    public int[] ProductQueries(int n, int[][] queries) {
        // Step 1: Build the powers array from binary representation of n
        List<int> powers = new List<int>();
        for (int i = 0; i < 31; i++) {
            if ((n & (1 << i)) != 0) {
                powers.Add(1 << i);
            }
        }

        // Step 2: Precompute prefix products modulo MOD
        int[] prefixProduct = new int[powers.Count];
        prefixProduct[0] = powers[0];
        for (int i = 1; i < powers.Count; i++) {
            prefixProduct[i] = (int)((long)prefixProduct[i - 1] * powers[i] % MOD);
        }

        // Step 3: Answer each query using prefix products
        List<int> answers = new List<int>();
        foreach (var query in queries) {
            int left = query[0];
            int right = query[1];

            if (left == 0) {
                answers.Add(prefixProduct[right]);
            } else {
                long inv = ModInverse(prefixProduct[left - 1], MOD);
                long result = prefixProduct[right] * inv % MOD;
                answers.Add((int)result);
            }
        }

        return answers.ToArray(); // ✅ Fix: Convert to int[] before returning
    }

    // Modular inverse using Fermat's Little Theorem
    private long ModInverse(long a, int mod) {
        return ModPow(a, mod - 2, mod);
    }

    private long ModPow(long baseVal, int exp, int mod) {
        long result = 1;
        baseVal %= mod;
        while (exp > 0) {
            if ((exp & 1) != 0) {
                result = result * baseVal % mod;
            }
            baseVal = baseVal * baseVal % mod;
            exp >>= 1;
        }
        return result;
    }
}
