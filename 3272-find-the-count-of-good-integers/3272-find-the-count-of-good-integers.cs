using System;
using System.Collections.Generic;
using System.Numerics; // for BigInteger if needed

public class Solution {
    // We'll precompute factorials up to n (n is small in our expected use case).
    BigInteger[] fact;

    public long CountGoodIntegers(int n, int k) {
        // Precompute factorials up to n.
        fact = new BigInteger[n + 1];
        fact[0] = 1;
        for (int i = 1; i <= n; i++) {
            fact[i] = fact[i - 1] * i;
        }
                
        long ans = 0;
        // Enumerate frequency vectors f[0..9] with sum = n.
        List<int[]> freqList = new List<int[]>();
        int[] cur = new int[10];
        GenerateFrequencies(0, n, cur, freqList);
        
        foreach (int[] f in freqList) {
            // Must be possible to form a palindrome.
            if (!CanFormPalindrome(f)) continue;
            // Check existence of some palindrome (with no leading zero) divisible by k.
            if (!ExistsPalDivisible(f, k)) continue;
            // Count how many n-digit numbers yield frequency vector f (ensuring no leading zero).
            BigInteger ways = CountNumbersWithFreq(f, n);
            ans = (ans + (long)ways) ;
        }
        return ans;
    }
    
    // Recursively generate all frequency vectors for 10 digits summing to rem.
    private void GenerateFrequencies(int pos, int rem, int[] cur, List<int[]> res) {
        if (pos == 10) {
            if (rem == 0) {
                int[] copy = new int[10];
                Array.Copy(cur, copy, 10);
                res.Add(copy);
            }
            return;
        }
        for (int i = 0; i <= rem; i++) {
            cur[pos] = i;
            GenerateFrequencies(pos + 1, rem - i, cur, res);
        }
    }
    
    // Check if a frequency vector can be rearranged into a palindrome.
    private bool CanFormPalindrome(int[] f) {
        int oddCount = 0;
        for (int d = 0; d < 10; d++) {
            if (f[d] % 2 != 0) oddCount++;
        }
        return oddCount <= 1;
    }
    
    // Count number of n-digit numbers with frequency vector f.
    // Numbers must have no leading zero.
    private BigInteger CountNumbersWithFreq(int[] f, int n) {
        BigInteger total = fact[n];
        for (int d = 0; d < 10; d++) {
            total /= fact[f[d]];
        }
        // Subtract those with a leading zero if f[0] > 0.
        if (f[0] > 0) {
            BigInteger withZero = fact[n - 1];
            withZero /= fact[f[0] - 1];
            for (int d = 1; d < 10; d++) {
                withZero /= fact[f[d]];
            }
            total -= withZero;
        }
        return total;
    }
    
    // Check whether, given a frequency vector f, there is some palindrome (with no leading zero) 
    // that can be formed (by rearranging f) which is divisible by k.
    private bool ExistsPalDivisible(int[] f, int k) {
        int n = 0;
        for (int d = 0; d < 10; d++) n += f[d];
        // Precompute powers of 10 mod k.
        int[] pow10 = new int[n + 1];
        pow10[0] = 1 % k;
        for (int i = 1; i <= n; i++) {
            pow10[i] = (int)((long)pow10[i - 1] * 10 % k);
        }
        
        int L = n / 2;  // length of half
        int fullLen = n;
        
        // Determine center digit for odd n.
        int centerDigit = -1;
        if (n % 2 == 1) {
            for (int d = 0; d < 10; d++) {
                if (f[d] % 2 == 1) {
                    centerDigit = d;
                    break;
                }
            }
        }
        
        // Build half frequencies.
        int[] half = new int[10];
        for (int d = 0; d < 10; d++) {
            half[d] = f[d] / 2;
        }
        
        // Precompute an array P for half positions: 
        // For even n, for position i in half, contribution is: d * (10^(n-1-i) + 10^(i)) mod k.
        // For odd n, same for half plus the center contributes centerDigit * 10^(L) mod k.
        int[] P = new int[L];
        for (int i = 0; i < L; i++) {
            int p1 = pow10[fullLen - 1 - i]; // left half place
            int p2 = pow10[i];               // mirror symmetric position
            P[i] = (p1 + p2) % k;
        }
        int centerContribution = (n % 2 == 1) ? pow10[L] % k : 0;
                
        // Use DP to check if we can order the half to reach a remainder that, when combined 
        // with the (optional) center, yields 0 mod k.
        var memo = new Dictionary<string, bool>();
        bool Dfs(int pos, int rem, int[] counts) {
            if (pos == L) {
                int totalRem = rem;
                if (n % 2 == 1) {
                    totalRem = (totalRem + centerDigit * centerContribution) % k;
                }
                return totalRem % k == 0;
            }
            // Build a key for memo from pos, rem and counts.
            string key = pos + "_" + rem + "_" + string.Join(",", counts);
            if (memo.ContainsKey(key)) return memo[key];
            for (int d = 0; d < 10; d++) {
                if (counts[d] > 0) {
                    // For the very first digit, ensure d != 0 (to avoid leading zero in the full palindrome).
                    if (pos == 0 && d == 0) continue;
                    counts[d]--;
                    int newRem = (rem + d * P[pos]) % k;
                    if (Dfs(pos + 1, newRem, counts)) {
                        memo[key] = true;
                        counts[d]++;
                        return true;
                    }
                    counts[d]++;
                }
            }
            memo[key] = false;
            return false;
        }
        return Dfs(0, 0, half);
    }
}
