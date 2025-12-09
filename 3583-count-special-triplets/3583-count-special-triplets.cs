using System;
using System.Collections.Generic;

public class Solution {
    public int SpecialTriplets(int[] nums) {
        const int MOD = 1000000007;
        int n = nums.Length;
        long result = 0;

        // Frequency map for right side
        var rightCount = new Dictionary<int, int>();
        foreach (var num in nums) {
            if (!rightCount.ContainsKey(num)) rightCount[num] = 0;
            rightCount[num]++;
        }

        var leftCount = new Dictionary<int, int>();

        for (int j = 0; j < n; j++) {
            // Remove current element from right side
            rightCount[nums[j]]--;

            // Count left matches
            int countLeft = 0;
            if (leftCount.ContainsKey(nums[j] * 2)) {
                countLeft = leftCount[nums[j] * 2];
            }

            // Count right matches
            int countRight = 0;
            if (rightCount.ContainsKey(nums[j] * 2)) {
                countRight = rightCount[nums[j] * 2];
            }

            result = (result + (long)countLeft * countRight) % MOD;

            // Add current element to left side
            if (!leftCount.ContainsKey(nums[j])) leftCount[nums[j]] = 0;
            leftCount[nums[j]]++;
        }

        return (int)result;
    }
}
