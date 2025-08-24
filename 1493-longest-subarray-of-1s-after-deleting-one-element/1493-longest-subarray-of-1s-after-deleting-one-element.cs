public class Solution {
    public int LongestSubarray(int[] nums) {
        int left = 0;
        int zeroCount = 0;
        int maxLen = 0;

        for (int right = 0; right < nums.Length; right++) {
            if (nums[right] == 0) {
                zeroCount++;
            }

            // Shrink window if more than one zero
            while (zeroCount > 1) {
                if (nums[left] == 0) {
                    zeroCount--;
                }
                left++;
            }

            // right - left + 1 is window size
            // subtract 1 because we must delete one element
            maxLen = Math.Max(maxLen, right - left);
        }

        return maxLen;
    }
}
