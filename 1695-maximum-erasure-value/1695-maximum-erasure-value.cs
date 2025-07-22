public class Solution {
    public int MaximumUniqueSubarray(int[] nums) {
        var seen = new HashSet<int>();
        int left = 0, right = 0, sum = 0, maxScore = 0;

        while (right < nums.Length) {
            if (!seen.Contains(nums[right])) {
                seen.Add(nums[right]);
                sum += nums[right];
                maxScore = Math.Max(maxScore, sum);
                right++;
            } else {
                // Shrink the window from the left until the duplicate is gone
                seen.Remove(nums[left]);
                sum -= nums[left];
                left++;
            }
        }

        return maxScore;
    }
}
