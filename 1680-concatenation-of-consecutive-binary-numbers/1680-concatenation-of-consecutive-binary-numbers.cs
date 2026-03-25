public class Solution {
    public int ConcatenatedBinary(int n) {
        const int MOD = 1000000007;
        long result = 0;
        int bitLength = 0;

        for (int i = 1; i <= n; i++) {
            // If i is a power of two, increase bit length
            if ((i & (i - 1)) == 0) {
                bitLength++;
            }

            result = ((result << bitLength) + i) % MOD;
        }

        return (int)result;
    }
}
