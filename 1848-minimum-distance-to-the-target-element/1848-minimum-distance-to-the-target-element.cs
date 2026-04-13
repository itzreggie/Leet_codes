public class Solution {
    public int GetMinDistance(int[] nums, int target, int start) {
        int n = nums.Length;

        for (int d = 0; d < n; d++) {
            int left = start - d;
            int right = start + d;

            if (left >= 0 && nums[left] == target)
                return d;

            if (right < n && nums[right] == target)
                return d;
        }

        return -1; // never reached because target is guaranteed to exist
    }
}
