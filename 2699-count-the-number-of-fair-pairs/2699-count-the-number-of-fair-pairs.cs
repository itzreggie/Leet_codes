public class Solution {
    public long CountFairPairs(int[] nums, int lower, int upper) {
        Array.Sort(nums);
        return CountPairs(nums, upper) - CountPairs(nums, lower - 1);
    }
    
    private long CountPairs(int[] nums, int target) {
        long count = 0;
        int left = 0, right = nums.Length - 1;
        while (left < right) {
            if ((long)nums[left] + nums[right] <= target) {
                count += (right - left);
                left++;
            } else {
                right--;
            }
        }
        return count;
    }
}
