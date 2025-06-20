using System;

public class Solution {
    public int MaxDistance(string s, int k) {
        int n = s.Length;
        int maxDistance = 0;
        // counts for moves seen so far.
        int countN = 0, countS = 0, countE = 0, countW = 0;

        // Process each prefix of the string.
        for (int i = 0; i < n; i++) {
            char c = s[i];
            switch (c) {
                case 'N': countN++; break;
                case 'S': countS++; break;
                case 'E': countE++; break;
                case 'W': countW++; break;
            }
            
            int prefixLen = i + 1;
            // Consider four target-direction choices.
            // For target (N, E): we want vertical moves to be N and horizontal moves to be E.
            // That means we can flip any S (vertical “wrong” moves) and any W (horizontal “wrong” moves).
            int cand1 = (countN - countS) + (countE - countW) + 2 * Math.Min(k, countS + countW);
            // For target (N, W): vertical moves turned into N; horizontal moves turned into W.
            int cand2 = (countN - countS) + (countW - countE) + 2 * Math.Min(k, countS + countE);
            // For target (S, E): vertical moves turned into S; horizontal moves turned into E.
            int cand3 = (countS - countN) + (countE - countW) + 2 * Math.Min(k, countN + countW);
            // For target (S, W): vertical moves turned into S; horizontal moves turned into W.
            int cand4 = (countS - countN) + (countW - countE) + 2 * Math.Min(k, countN + countE);

            int bestForPrefix = Math.Max(Math.Max(cand1, cand2), Math.Max(cand3, cand4));
            // The Manhattan distance cannot exceed the number of moves.
            bestForPrefix = Math.Min(bestForPrefix, prefixLen);
            // (Also ensure nonnegative result.)
            bestForPrefix = Math.Max(bestForPrefix, 0);
            
            maxDistance = Math.Max(maxDistance, bestForPrefix);
        }
        return maxDistance;
    }
}
