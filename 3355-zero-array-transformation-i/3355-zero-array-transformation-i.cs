
using System;

public class Solution {
    public bool IsZeroArray(int[] nums, int[][] queries) {
        int n = nums.Length;

        foreach (var query in queries) {
            int li = query[0], ri = query[1];

            // Apply decrements
            for (int i = li; i <= ri; i++) {
                nums[i]--;
            }
        }

        // Check if all elements are zero
        foreach (var num in nums) {
            if (num != 0) return false;
        }

        return true;
    }
}
