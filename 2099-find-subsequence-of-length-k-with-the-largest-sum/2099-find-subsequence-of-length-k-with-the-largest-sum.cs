using System;
using System.Collections.Generic;
using System.Linq;

public class Solution {
    public int[] MaxSubsequence(int[] nums, int k) {
        // Step 1: Pair each value with its index
        var indexedNums = nums.Select((num, index) => new { num, index });

        // Step 2: Find the top-k elements by value (preserve original index)
        var topK = indexedNums
            .OrderByDescending(x => x.num)
            .Take(k)
            .OrderBy(x => x.index); // Restore original order

        // Step 3: Extract the values in correct order
        return topK.Select(x => x.num).ToArray();
    }
}
