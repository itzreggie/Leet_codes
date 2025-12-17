public class Solution {
    public int RemoveElement(int[] nums, int val) {
        int k = 0; // index for placing non-val elements

        for (int i = 0; i < nums.Length; i++) {
            if (nums[i] != val) {
                nums[k] = nums[i];
                k++;
            }
        }

        return k;
    }
}
