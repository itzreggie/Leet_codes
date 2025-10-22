using System;
using System.Collections.Generic;

public class Solution {
    public int MaxFrequency(int[] nums, int k, int numOperations) {
        int n = nums.Length;
        if (n == 0) return 0;

        var frequency = new Dictionary<long,int>();
        var rangeBoundaries = new SortedDictionary<long,int>();

        foreach (int ni in nums) {
            long num = ni;
            if (frequency.ContainsKey(num)) frequency[num] += 1;
            else frequency[num] = 1;

            long start = num - (long)k;
            long endExclusive = num + (long)k + 1L;

            if (!rangeBoundaries.ContainsKey(start)) rangeBoundaries[start] = 0;
            rangeBoundaries[start] += 1;

            if (!rangeBoundaries.ContainsKey(endExclusive)) rangeBoundaries[endExclusive] = 0;
            rangeBoundaries[endExclusive] -= 1;

            // ensure the exact position key exists with no effect on deltas
            if (!rangeBoundaries.ContainsKey(num)) rangeBoundaries[num] = 0;
        }

        int maxResult = 0;
        int activeCount = 0;

        foreach (var kv in rangeBoundaries) {
            long position = kv.Key;
            int delta = kv.Value;
            activeCount += delta;

            int freqAtPos = 0;
            if (frequency.TryGetValue(position, out int v)) freqAtPos = v;

            int candidate = Math.Min(activeCount, freqAtPos + numOperations);
            if (candidate > maxResult) maxResult = candidate;
        }

        return Math.Max(maxResult, 1);
    }
}
