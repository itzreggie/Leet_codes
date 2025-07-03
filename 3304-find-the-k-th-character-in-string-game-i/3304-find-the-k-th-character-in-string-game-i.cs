using System;
using System.Collections.Generic;

public class Solution {
    public char KthCharacter(int k) {
        List<long> lengths = new List<long> { 1 };  // length of each stage
        while (lengths[^1] < k) {
            lengths.Add(lengths[^1] * 2);
        }
        return Find(k, lengths.Count - 1);
    }

    private char Find(int k, int depth) {
        if (depth == 0) return 'a';

        long half = (long)Math.Pow(2, depth - 1);
        if (k <= half) {
            return Find(k, depth - 1);
        } else {
            char prev = Find((int)(k - half), depth - 1);
            return (char)((prev - 'a' + 1) % 26 + 'a');
        }
    }
}
