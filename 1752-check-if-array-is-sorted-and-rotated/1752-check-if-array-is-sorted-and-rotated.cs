public class Solution {
    public bool Check(int[] nums) {
        int count = 0;
        for (int i = 0; i < nums.Length; i++) {
            if (nums[i] > nums[(i + 1) % nums.Length]) {
                count++;
            }
        }
        return count <= 1;
    }
}
