public class Solution {
    public int CountHillValley(int[] nums) {
        int count = 0;

        // Remove consecutive duplicates to simplify comparisons
        var filtered = new List<int>();
        filtered.Add(nums[0]);
        for (int i = 1; i < nums.Length; i++) {
            if (nums[i] != nums[i - 1]) {
                filtered.Add(nums[i]);
            }
        }

        // Check for hills and valleys
        for (int i = 1; i < filtered.Count - 1; i++) {
            int left = filtered[i - 1];
            int mid = filtered[i];
            int right = filtered[i + 1];

            if ((mid > left && mid > right) || (mid < left && mid < right)) {
                count++;
            }
        }

        return count;
    }
}
