using System;

public class Solution {
    public long NumberOfPowerfulInt(long start, long finish, int limit, string s) {
        // First, if any digit in s exceeds limit, no valid number exists.
        foreach (char c in s) {
            if (c - '0' > limit)
                return 0;
        }
        int m = s.Length;
        
        // Build the next state automaton for matching string s.
        // nextState[state][d] = new state if we are in state "state" and we append digit d.
        int[][] nextState = new int[m + 1][];
        for (int state = 0; state <= m; state++) {
            nextState[state] = new int[10];
            for (int d = 0; d < 10; d++) {
                string cur = "";
                if (state > 0)
                    cur = s.Substring(0, state);
                cur += (char)('0' + d);
                int newState = 0;
                for (int len = Math.Min(m, cur.Length); len >= 0; len--) {
                    if (cur.EndsWith(s.Substring(0, len))) {
                        newState = len;
                        break;
                    }
                }
                nextState[state][d] = newState;
            }
        }
        
        // Count numbers <= X that are composed solely of digits <= limit and end with s.
        long CountUpTo(long X) {
            if (X < 0) return 0;
            string digits = X.ToString();
            int n = digits.Length;
            // dp[pos, tight, lead, match] (tight:0/1, lead:0/1, match:0..m).
            // We'll use a 4D array; dimensions: [n+1, 2, 2, m+1].
            long[,,,] dp = new long[n + 1, 2, 2, m + 1];
            // Initialize with -1 (meaning not computed).
            for (int a = 0; a <= n; a++) {
                for (int b = 0; b < 2; b++) {
                    for (int c = 0; c < 2; c++) {
                        for (int d = 0; d <= m; d++) {
                            dp[a, b, c, d] = -1;
                        }
                    }
                }
            }
            
            // Recursive DP function.
            // pos: current digit index (0-indexed)
            // tight: 1 if prefix equals that of X; 0 if already lower.
            // lead: 1 if we have not yet placed a nonzero digit.
            // match: current matched length (0 <= match <= m).
            Func<int, int, int, int, long> rec = null;
            rec = (pos, tight, lead, match) => {
                if (pos == n) {
                    // We only count if we have started the number (lead==0) and the suffix matches s fully.
                    return (lead == 0 && match == m) ? 1L : 0L;
                }
                if (dp[pos, tight, lead, match] != -1) {
                    return dp[pos, tight, lead, match];
                }
                long ways = 0;
                int currentDigit = digits[pos] - '0';
                int upper = 0;
                if (tight == 1) {
                    // When tight, allowed d is from 0 to min(limit, currentDigit).
                    upper = Math.Min(limit, currentDigit);
                } else {
                    upper = limit;
                }
                for (int d = 0; d <= upper; d++) {
                    int ntight = 0;
                    if (tight == 1) {
                        // Remain tight only if d equals currentDigit.
                        ntight = (d == currentDigit) ? 1 : 0;
                    }
                    int nlead = lead;
                    int nmatch = match;
                    if (lead == 1 && d == 0) {
                        // Still in leading zeros; keep match at 0.
                        nlead = 1;
                        nmatch = 0;
                    } else {
                        nlead = 0;
                        // If we are just starting (lead==1 originally), treat previous match as 0.
                        int prevMatch = (lead == 1 ? 0 : match);
                        nmatch = nextState[prevMatch][d];
                    }
                    ways += rec(pos + 1, ntight, nlead, nmatch);
                }
                dp[pos, tight, lead, match] = ways;
                return ways;
            };
            
            return rec(0, 1, 1, 0);
        }
        
        long ans = CountUpTo(finish) - CountUpTo(start - 1);
        return ans;
    }
}
