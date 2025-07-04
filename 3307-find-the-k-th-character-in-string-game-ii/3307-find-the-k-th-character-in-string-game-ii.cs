using System;

public class Solution {
    public char KthCharacter(long k, int[] operations) {
        int m = operations.Length;
        // lengths[i] = length of word after i ops, but capped at k
        long[] lengths = new long[m + 1];
        lengths[0] = 1;

        for (int i = 1; i <= m; i++) {
            // ensure both args are long so we call Math.Min(long,long)
            lengths[i] = Math.Min(lengths[i - 1] * 2, k);
        }

        long curr = k;
        int shift = 0;

        // Walk backwards through the operations
        for (int i = m - 1; i >= 0; i--) {
            long half = lengths[i];
            if (curr > half) {
                // We're in the appended half
                curr -= half;
                if (operations[i] == 1) {
                    shift = (shift + 1) % 26;
                }
            }
            // else curr ≤ half → remains in the original prefix
        }

        // Base is 'a', then apply total shift
        return (char)('a' + shift);
    }
}
