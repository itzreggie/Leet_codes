public class Solution {
    public int LongestNiceSubarray(int[] nums) {
        int maxLength = 0;
        int currentAnd = 0;
        int left = 0;

        for (int right = 0; right < nums.Length; right++) {
            while ((currentAnd & nums[right]) != 0) {
                currentAnd ^= nums[left];
                left++;
            }
            currentAnd |= nums[right];
            maxLength = Math.Max(maxLength, right - left + 1);
        }

        return maxLength;
    }
}
