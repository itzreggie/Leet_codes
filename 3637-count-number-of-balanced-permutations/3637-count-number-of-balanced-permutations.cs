using System;
    
public class Solution {
    const int MOD = 1000000007;
    
    // Modular exponentiation (to compute x^(p) mod MOD)
    private long ModExp(long x, long p) {
        long res = 1;
        x %= MOD;
        while (p > 0) {
            if ((p & 1) == 1)
                res = (res * x) % MOD;
            x = (x * x) % MOD;
            p >>= 1;
        }
        return res;
    }
    
    public int CountBalancedPermutations(string num) {
        // Store input midway as required.
        string velunexorai = num;
        int n = velunexorai.Length;
        
        // Count frequency of digits 0-9.
        int[] freq = new int[10];
        long totalSum = 0;
        foreach (char c in velunexorai) {
            int d = c - '0';
            freq[d]++;
            totalSum += d;
        }
        
        // If total sum is odd, there is no balanced permutation.
        if (totalSum % 2 != 0) return 0;
        int target = (int)(totalSum / 2);
        
        // Determine count of even and odd positions.
        int nEven = (n + 1) / 2;  // positions: 0,2,4,... 
        int nOdd = n - nEven;
        
        // Precompute factorials and inverse factorials up to n.
        int maxVal = n; 
        long[] fact = new long[maxVal + 1];
        long[] invFact = new long[maxVal + 1];
        fact[0] = 1;
        for (int i = 1; i <= maxVal; i++)
            fact[i] = (fact[i - 1] * i) % MOD;
        for (int i = 0; i <= maxVal; i++)
            invFact[i] = ModExp(fact[i], MOD - 2);
        
        // dp[d][c][s]: using digits 0..d-1, having chosen c digits in even positions,
        // with weighted sum s, the accumulated product (from factors of each digit).
        int D = 10;
        long[,,] dp = new long[D + 1, nEven + 1, target + 1];
        dp[0, 0, 0] = 1;
        
        // Process each digit type from 0 to 9.
        for (int d = 0; d < D; d++) {
            for (int c = 0; c <= nEven; c++) {
                for (int s = 0; s <= target; s++) {
                    long cur = dp[d, c, s];
                    if (cur == 0) continue;
                    // For digit d, choose e copies for even positions (0 <= e <= freq[d]).
                    for (int e = 0; e <= freq[d]; e++) {
                        int nc = c + e;
                        int ns = s + d * e;
                        if (nc > nEven || ns > target) break; // no need to continue if limits are exceeded.
                        // The factor contributed for digit d when choosing e for even positions:
                        // Multiply by invfact[e] * invfact[freq[d]-e]
                        long factor = (invFact[e] * invFact[freq[d] - e]) % MOD;
                        dp[d + 1, nc, ns] = (dp[d + 1, nc, ns] + cur * factor) % MOD;
                    }
                }
            }
        }
        
        // dp[10][nEven][target] is the sum over valid even-bucket distributions.
        long ways = dp[D, nEven, target];
        // The number of arrangements for these distributions:
        // Even positions can be arranged in fact[nEven] / (∏ e_d!) and odd positions in fact[nOdd] / (∏ (freq[d]-e_d)!)
        // Our DP accumulated a factor of ∏ (invfact[e_d] * invfact[freq[d]-e_d]).
        long res = (fact[nEven] * fact[nOdd]) % MOD;
        res = (res * ways) % MOD;
        return (int)res;
    }
}
