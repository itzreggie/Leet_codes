public class Solution {
    public int MaximumDifference(int[] nums) {
        if (nums == null || nums.Length < 2) return -1;

        int minVal = nums[0]; // Track the minimum value encountered so far
        int maxDiff = -1; // Initialize max difference to -1

        for (int j = 1; j < nums.Length; j++) {
            if (nums[j] > minVal) {
                maxDiff = Math.Max(maxDiff, nums[j] - minVal);
            }
            minVal = Math.Min(minVal, nums[j]); // Update minimum value
        }

        return maxDiff;
    }
}
