using System;
using System.Collections.Generic;

public class Solution {
    const int MOD = 1_000_000_007;

    public int PossibleStringCount(string word, int k) {
        int n = word.Length;
        if (n == 0) return 0;

        // 1) Build run‐lengths of identical chars
        var runs = new List<int>();
        int i = 0;
        while (i < n) {
            int j = i + 1;
            while (j < n && word[j] == word[i]) j++;
            runs.Add(j - i);
            i = j;
        }

        int runCount = runs.Count;
        // The shortest original string is 1 per run
        if (runCount >= k) {
            // every choice yields length ≥ k already
            return runs.Aggregate(1L, (acc, w) => acc * w % MOD) is long tot 
                ? (int)tot 
                : 0;
        }

        int totalLen = 0;
        long totalComb = 1;
        foreach (int w in runs) {
            totalLen += w;
            totalComb = totalComb * w % MOD;
        }
        if (totalLen < k) return 0;

        // 2) Count how many choices produce length < k via DP
        // dp[s] = #ways (after some runs) to get sum exactly s, for s in [0..k-1]
        int maxSum = k - 1;
        var dp     = new int[maxSum + 1];
        var nextDp = new int[maxSum + 1];
        dp[0] = 1;

        foreach (int w in runs) {
            // sliding window sum of dp[ s - w .. s - 1 ]
            long window = 0;
            // zero‐out nextDp once
            Array.Clear(nextDp, 0, maxSum + 1);

            for (int s = 1; s <= maxSum; s++) {
                // include dp[s-1]
                window = (window + dp[s - 1]) % MOD;
                // exclude dp[s - 1 - w] if out of window
                if (s - 1 - w >= 0) {
                    window = (window - dp[s - 1 - w] + MOD) % MOD;
                }
                nextDp[s] = (int)window;
            }
            // swap dp and nextDp
            var tmp = dp;
            dp = nextDp;
            nextDp = tmp;
        }

        // 3) Sum dp[s] over s < k to count the “too short”
        long tooShort = 0;
        for (int s = 0; s <= maxSum; s++) {
            tooShort += dp[s];
            if (tooShort >= MOD) tooShort -= MOD;
        }

        // 4) Valid = totalComb − tooShort  (mod MOD)
        long ans = totalComb - tooShort;
        if (ans < 0) ans += MOD;
        return (int)ans;
    }
}
