using System;

public class Solution {
    private const int MOD = 1000000007;
    
    // Fast modular exponentiation to compute (base^exp) % mod.
    private long ModPow(long baseVal, int exp, int mod) {
        long result = 1;
        baseVal = baseVal % mod;
        while (exp > 0) {
            if ((exp & 1) == 1)
                result = (result * baseVal) % mod;
            baseVal = (baseVal * baseVal) % mod;
            exp >>= 1;
        }
        return result;
    }
    
    // Precompute factorials and inverse factorials up to n.
    private void PrecomputeFactorials(int n, out long[] fact, out long[] invFact) {
        fact = new long[n + 1];
        invFact = new long[n + 1];
        fact[0] = 1;
        for (int i = 1; i <= n; i++) {
            fact[i] = (fact[i - 1] * i) % MOD;
        }
        invFact[n] = ModPow(fact[n], MOD - 2, MOD);
        for (int i = n - 1; i >= 0; i--) {
            invFact[i] = (invFact[i + 1] * (i + 1)) % MOD;
        }
    }
    
    // Compute nCr mod MOD.
    private long NCr(int n, int r, long[] fact, long[] invFact) {
        if (r < 0 || r > n)
            return 0;
        return (((fact[n] * invFact[r]) % MOD) * invFact[n - r]) % MOD;
    }
    
    public int CountGoodArrays(int n, int m, int k) {
        // Compute segments = n - k.
        // The number of change-gap positions needed is segments - 1 = (n - k - 1).
        int segments = n - k;  // segments > 0 (assuming input is valid such that a solution exists)
        if (segments <= 0) return 0;  
        
        // Number of ways to choose which gaps are "changes".
        // There are n - 1 gaps and we choose (segments - 1) to be changes.
        int r = segments - 1; 
        int totalGaps = n - 1;
        
        // Precompute factorials up to totalGaps.
        PrecomputeFactorials(totalGaps, out long[] fact, out long[] invFact);
        long waysToPlaceChanges = NCr(totalGaps, r, fact, invFact);
        
        // Number of ways to assign numbers to segments:
        // For the first segment: m choices.
        // For each change, we have (m-1) choices.
        long waysToAssign = (m * ModPow(m - 1, r, MOD)) % MOD;
        
        long ans = (waysToPlaceChanges * waysToAssign) % MOD;
        return (int)ans;
    }
}
