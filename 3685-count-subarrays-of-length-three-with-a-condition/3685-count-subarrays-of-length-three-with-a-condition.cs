public class Solution {
    public int CountSubarrays(int[] nums) {
        int n = nums.Length;
        int count = 0;

        // Loop through all subarrays of length 3
        for (int i = 0; i <= n - 3; i++) {
            int first = nums[i];
            int middle = nums[i + 1];
            int third = nums[i + 2];

            // Check the condition: first + third == middle / 2
            if (first + third == middle / 2.0) {
                count++;
            }
        }

        return count;
    }
}
