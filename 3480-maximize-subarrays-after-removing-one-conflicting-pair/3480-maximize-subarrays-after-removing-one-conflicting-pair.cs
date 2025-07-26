using System;
using System.Collections.Generic;

public class Solution {
    public long MaxSubarrays(int n, int[][] conflictingPairs) {
        int m = conflictingPairs.Length;
        
        // 1) Normalize pairs: ensure u < v, assign each an ID 0..m-1
        var pairs = new (int u, int v)[m];
        for (int i = 0; i < m; i++) {
            int a = conflictingPairs[i][0], b = conflictingPairs[i][1];
            if (a < b) pairs[i] = (a, b);
            else        pairs[i] = (b, a);
        }
        
        // 2) Group pair‐IDs by their start‐point u
        var byStart = new List<int>[n + 2];
        for (int i = 0; i <= n + 1; i++)
            byStart[i] = new List<int>();
        for (int i = 0; i < m; i++) {
            int u = pairs[i].u;
            byStart[u].Add(i);
        }
        
        // 3) Sweep ℓ = n → 1, maintain two smallest end‐points among pairs with u >= ℓ
        const int INF = int.MaxValue / 2;
        var firstEnd   = new int[n + 2];
        var firstId    = new int[n + 2];
        var secondEnd  = new int[n + 2];
        
        // Initialize at ℓ = n+1: no pairs yet
        firstEnd[n+1]  = n + 1;
        secondEnd[n+1] = n + 1;
        firstId[n+1]   = -1;
        
        // Now sweep down
        for (int l = n; l >= 1; l--) {
            // carry forward previous
            firstEnd[l]   = firstEnd[l+1];
            firstId[l]    = firstId[l+1];
            secondEnd[l]  = secondEnd[l+1];
            
            // add all pairs that start at l
            foreach (int pid in byStart[l]) {
                int end = pairs[pid].v;
                
                if (end < firstEnd[l]) {
                    // new best becomes 'first', old first drops to 'second'
                    secondEnd[l] = firstEnd[l];
                    firstEnd[l]  = end;
                    firstId[l]   = pid;
                }
                else if (end < secondEnd[l]) {
                    // better than old second
                    secondEnd[l] = end;
                }
            }
        }
        
        // 4) Compute the baseline sum = sum(firstEnd[l] - l) for l=1..n
        long baseSum = 0;
        for (int l = 1; l <= n; l++) {
            baseSum += (long)firstEnd[l] - l;
        }
        
        // 5) For each pair‐ID collect the delta if it's ever the firstEnd
        var delta = new long[m];
        for (int l = 1; l <= n; l++) {
            int pid = firstId[l];
            if (pid >= 0) {
                // removing pid lets us use secondEnd[l] instead of firstEnd[l]
                delta[pid] += (long)secondEnd[l] - firstEnd[l];
            }
        }
        
        // 6) Answer = max over i of (baseSum + delta[i])
        long answer = 0;
        for (int i = 0; i < m; i++) {
            answer = Math.Max(answer, baseSum + delta[i]);
        }
        
        return answer;
    }
}
