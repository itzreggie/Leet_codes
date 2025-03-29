using System;
using System.Collections.Generic;
using System.Linq;

public class Solution {
    private const long MOD = 1_000_000_007;
    
    public int MaximumScore(IList<int> nums, int k) {
        int n = nums.Count;
        int[] arr = nums.ToArray();
        int[] ps = new int[n]; // prime score for each element
        for (int i = 0; i < n; i++) {
            ps[i] = PrimeScore(arr[i]);
        }
        
        // For each index, count how many subarrays would choose arr[i]
        // using the idea: left[i] = number of contiguous positions on left
        // where no element is “better” (has a higher prime score),
        // and right[i] = similarly for right.
        long[] left = new long[n];
        long[] right = new long[n];
        Stack<int> stack = new Stack<int>();
        
        // Compute left: for each i, find nearest index on the left that is "better".
        for (int i = 0; i < n; i++) {
            while (stack.Count > 0 && ps[stack.Peek()] < ps[i])
                stack.Pop();
            int prev = (stack.Count > 0 ? stack.Peek() : -1);
            left[i] = i - prev;
            stack.Push(i);
        }
        
        stack.Clear();
        // Compute right: for each i, find nearest index on the right that is strictly better.
        for (int i = n - 1; i >= 0; i--) {
            while (stack.Count > 0 && ps[stack.Peek()] <= ps[i])
                stack.Pop();
            int next = (stack.Count > 0 ? stack.Peek() : n);
            right[i] = next - i;
            stack.Push(i);
        }
        
        // Total contribution: number of subarrays in which index i is dominant.
        long[] contrib = new long[n];
        for (int i = 0; i < n; i++) {
            contrib[i] = left[i] * right[i];
        }
        
        // To maximize the product, we want to use the multiplications
        // that contribute the highest value first.
        // (Note: The actual multiplication uses arr[i].)
        var indices = Enumerable.Range(0, n).ToList();
        indices.Sort((i, j) => arr[j].CompareTo(arr[i]));  // sort descending by value
        
        long ans = 1;
        int remaining = k;
        foreach (int i in indices) {
            if (remaining <= 0)
                break;
            long times = Math.Min(contrib[i], remaining);
            ans = (ans * ModPow(arr[i], times, MOD)) % MOD;
            remaining -= (int)times;
        }
        return (int)ans;
    }
    
    // Helper to compute number of distinct prime factors (the prime score)
    private int PrimeScore(int x) {
        int cnt = 0;
        for (int i = 2; i * i <= x; i++){
            if (x % i == 0){
                cnt++;
                while (x % i == 0)
                    x /= i;
            }
        }
        if (x > 1)
            cnt++;
        return cnt;
    }
    
    // Fast modular exponentiation
    private long ModPow(long a, long b, long mod) {
        long result = 1;
        a %= mod;
        while (b > 0) {
            if ((b & 1) == 1)
                result = (result * a) % mod;
            a = (a * a) % mod;
            b >>= 1;
        }
        return result;
    }
}
