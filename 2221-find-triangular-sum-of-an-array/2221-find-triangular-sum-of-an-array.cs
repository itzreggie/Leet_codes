


public class Solution {
    public int TriangularSum(int[] nums) {
        while (nums.Length > 1) {
            int[] newNums = new int[nums.Length - 1];
            for (int i = 0; i < nums.Length - 1; i++) {
                newNums[i] = (nums[i] + nums[i + 1]) % 10;
            }
            nums = newNums;
        }
        return nums[0];
    }
}
