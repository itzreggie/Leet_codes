

public class Solution {
    public int MaxAdjacentDistance(int[] nums) {
        int n = nums.Length;
        int maxDiff = 0;

        for (int i = 0; i < n; i++) {
            int j = (i + 1) % n; // Wrap around for circular comparison
            int diff = Math.Abs(nums[i] - nums[j]);
            maxDiff = Math.Max(maxDiff, diff);
        }

        return maxDiff;
    }
}
