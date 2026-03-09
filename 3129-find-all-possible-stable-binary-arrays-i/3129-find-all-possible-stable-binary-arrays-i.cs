public class Solution {
    const int MOD = 1_000_000_007;

    public int NumberOfStableArrays(int zero, int one, int limit) {
        long[,] dp0 = new long[zero + 1, one + 1]; // end with 0
        long[,] dp1 = new long[zero + 1, one + 1]; // end with 1

        // prefix over i for dp1 (for dp0 transitions)
        long[,] pref1OverI = new long[zero + 1, one + 1];
        // prefix over j for dp0 (for dp1 transitions)
        long[,] pref0OverJ = new long[zero + 1, one + 1];

        for (int i = 0; i <= zero; i++) {
            for (int j = 0; j <= one; j++) {
                if (i == 0 && j == 0) {
                    // empty array: no ways
                }
                else if (j == 0) {
                    // only zeros: valid if i <= limit, and must end with 0
                    if (i <= limit) dp0[i, 0] = 1;
                }
                else if (i == 0) {
                    // only ones: valid if j <= limit, and must end with 1
                    if (j <= limit) dp1[0, j] = 1;
                }
                else {
                    // dp0[i,j] = sum_{k=1..min(limit,i)} dp1[i-k, j]
                    int fromI = Math.Max(0, i - limit);
                    long sum1 = pref1OverI[i - 1, j];
                    long sub1 = fromI - 1 >= 0 ? pref1OverI[fromI - 1, j] : 0;
                    dp0[i, j] = (sum1 - sub1 + MOD) % MOD;

                    // dp1[i,j] = sum_{k=1..min(limit,j)} dp0[i, j-k]
                    int fromJ = Math.Max(0, j - limit);
                    long sum0 = pref0OverJ[i, j - 1];
                    long sub0 = fromJ - 1 >= 0 ? pref0OverJ[i, fromJ - 1] : 0;
                    dp1[i, j] = (sum0 - sub0 + MOD) % MOD;
                }

                // update prefixes
                long prevPref1 = i > 0 ? pref1OverI[i - 1, j] : 0;
                pref1OverI[i, j] = (prevPref1 + dp1[i, j]) % MOD;

                long prevPref0 = j > 0 ? pref0OverJ[i, j - 1] : 0;
                pref0OverJ[i, j] = (prevPref0 + dp0[i, j]) % MOD;
            }
        }

        long ans = (dp0[zero, one] + dp1[zero, one]) % MOD;
        return (int)ans;
    }
}
