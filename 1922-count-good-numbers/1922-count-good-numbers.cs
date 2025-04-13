public class Solution {
    const long MOD = 1000000007;

    public int CountGoodNumbers(long n) {
        return (int)((MyPow(5, (n + 1) / 2) * MyPow(4, n / 2)) % MOD);
    }

    private long MyPow(long x, long n) {
        long res = 1;
        while(n > 0) {
            if((n & 1) == 1)
                res = (res * x) % MOD;
            x = (x * x) % MOD;
            n >>= 1;
        }
        return res;
    }
}
