public class Solution {
    public int MinOperations(int[] nums) {
        int n = nums.Length;
        int operations = 0;

        // Iterate through the array
        for (int i = 0; i <= n - 3; i++) {
            // If the current element is 0, perform a flip
            if (nums[i] == 0) {
                // Flip the next three consecutive elements
                nums[i] ^= 1;
                nums[i + 1] ^= 1;
                nums[i + 2] ^= 1;
                operations++;
            }
        }

        // Check if all elements are 1
        for (int i = 0; i < n; i++) {
            if (nums[i] == 0) {
                return -1; // Impossible to make all elements 1
            }
        }

        return operations; // Return the total number of operations
    }
}
