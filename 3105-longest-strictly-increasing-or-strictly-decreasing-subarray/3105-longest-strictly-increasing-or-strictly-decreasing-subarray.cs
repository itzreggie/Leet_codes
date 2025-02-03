


public class Solution {
    public int LongestMonotonicSubarray(int[] nums) {
        if (nums.Length == 0) return 0;

        int maxLength = 1;
        int incLength = 1;
        int decLength = 1;

        for (int i = 1; i < nums.Length; i++) {
            if (nums[i] > nums[i - 1]) {
                incLength++;
                decLength = 1;
            } else if (nums[i] < nums[i - 1]) {
                decLength++;
                incLength = 1;
            } else {
                incLength = 1;
                decLength = 1;
            }
            maxLength = Math.Max(maxLength, Math.Max(incLength, decLength));
        }

        return maxLength;
    }
}
