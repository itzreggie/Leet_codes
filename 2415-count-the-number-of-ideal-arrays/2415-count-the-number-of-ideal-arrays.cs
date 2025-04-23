using System;
using System.Collections.Generic;
using System.Linq;

public class Solution {
    public int IdealArrays(int n, int maxValue) {
        int mod = 1000000007;

        // dp for chain length 1: each number v in [1, maxValue] forms one chain.
        long[] dp1 = new long[maxValue + 1];
        for (int v = 1; v <= maxValue; v++) {
            dp1[v] = 1;
        }
        
        // Precompute divisors for each value from 1 to maxValue.
        // For each v, find all divisors d (with d < v) such that d divides v.
        List<int>[] divisors = new List<int>[maxValue + 1];
        for (int v = 1; v <= maxValue; v++) {
            divisors[v] = new List<int>();
        }
        for (int v = 1; v <= maxValue; v++) {
            for (int j = 2 * v; j <= maxValue; j += v) {
                divisors[j].Add(v);
            }
        }
        
        // chainCount[k-1] will hold the sum of dp for chain of length k.
        List<long> chainCount = new List<long>(); 
        chainCount.Add(maxValue); // chain of length 1: one per number.
        
        // List to hold dp arrays for each length of chain.
        List<long[]> dpList = new List<long[]>();
        dpList.Add(dp1);
        
        // Build dp for chain lengths 2, 3, ... until no new chains occur.
        int maxK = 1;
        while (true) {
            long[] prev = dpList[dpList.Count - 1];
            long[] curr = new long[maxValue + 1];
            bool hasPositive = false;
            for (int v = 1; v <= maxValue; v++) {
                long sum = 0;
                foreach (int d in divisors[v]) {  // d divides v and d < v.
                    sum = (sum + prev[d]) % mod;
                }
                curr[v] = sum;
                if (curr[v] > 0) hasPositive = true;
            }
            if (!hasPositive) break;  // No longer possible to form a longer chain.
            dpList.Add(curr);
            long total = 0;
            for (int v = 1; v <= maxValue; v++) {
                total = (total + curr[v]) % mod;
            }
            chainCount.Add(total);
            maxK++;
        }
        
        // Combine the counts with the number of ways to fill the n-length array.
        // For a distinct chain of length k, there are C(n-1, k-1) ways to “insert repeats”.
        long ans = 0;
        for (int k = 1; k <= maxK; k++) {
            long waysToFill = Combination(n - 1, k - 1, mod);  // Choose k-1 positions out of n-1.
            ans = (ans + chainCount[k - 1] * waysToFill) % mod;
        }
        return (int)ans;
    }
    
    // Compute combination(n, r) mod mod for small r using iterative multiplication.
    private long Combination(long n, int r, int mod) {
        if (r < 0 || r > n) return 0;
        long res = 1;
        for (int i = 1; i <= r; i++) {
            res = res * (n - i + 1) % mod;
            res = res * ModInverse(i, mod) % mod;
        }
        return res;
    }
    
    // Modular inverse using Fermat's little theorem; mod must be prime.
    private long ModInverse(long a, int mod) {
        return ModPow(a, mod - 2, mod);
    }
    
    private long ModPow(long a, long b, int mod) {
        long res = 1;
        a %= mod;
        while (b > 0) {
            if ((b & 1) == 1) res = (res * a) % mod;
            a = (a * a) % mod;
            b >>= 1;
        }
        return res;
    }
}
