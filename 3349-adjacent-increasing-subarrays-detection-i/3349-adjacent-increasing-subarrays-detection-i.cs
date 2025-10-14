public class Solution {
    public bool HasIncreasingSubarrays(IList<int> nums, int k) {
        int n = nums.Count;

        if (n < 2 * k) return false;

        for (int i = 0; i <= n - 2 * k; i++) {
            if (IsStrictlyIncreasing(nums, i, k) && IsStrictlyIncreasing(nums, i + k, k)) {
                return true;
            }
        }

        return false;
    }

    private bool IsStrictlyIncreasing(IList<int> nums, int start, int length) {
        for (int i = start; i < start + length - 1; i++) {
            if (nums[i] >= nums[i + 1]) {
                return false;
            }
        }
        return true;
    }
}
