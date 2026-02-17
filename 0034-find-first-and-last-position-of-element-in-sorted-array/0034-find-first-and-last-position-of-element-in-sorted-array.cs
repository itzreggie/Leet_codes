public class Solution {
    public int[] SearchRange(int[] nums, int target) {
        return new int[] {
            FindBound(nums, target, true),   // leftmost index
            FindBound(nums, target, false)   // rightmost index
        };
    }

    private int FindBound(int[] nums, int target, bool findLeft) {
        int left = 0, right = nums.Length - 1;
        int result = -1;

        while (left <= right) {
            int mid = left + (right - left) / 2;

            if (nums[mid] == target) {
                result = mid;
                if (findLeft) {
                    right = mid - 1;   // keep searching left side
                } else {
                    left = mid + 1;    // keep searching right side
                }
            }
            else if (nums[mid] < target) {
                left = mid + 1;
            }
            else {
                right = mid - 1;
            }
        }

        return result;
    }
}
