public class Solution {
    public long CountSubarrays(int[] nums, int minK, int maxK) {
        int n = nums.Length;
        int lastMin = -1, lastMax = -1, invalidIndex = -1;
        long count = 0;

        for (int i = 0; i < n; i++) {
            if (nums[i] < minK || nums[i] > maxK) {
                invalidIndex = i;
            }
            if (nums[i] == minK) {
                lastMin = i;
            }
            if (nums[i] == maxK) {
                lastMax = i;
            }
            // Count subarrays ending at index i that satisfy the conditions
            count += Math.Max(0, Math.Min(lastMin, lastMax) - invalidIndex);
        }

        return count;
    }
}
