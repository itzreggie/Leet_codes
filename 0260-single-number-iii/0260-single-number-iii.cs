public class Solution {
    public int[] SingleNumber(int[] nums) {
        int xor = 0;

        // Step 1: XOR all numbers → result = a ^ b (the two unique numbers)
        foreach (int num in nums)
            xor ^= num;

        // Step 2: Find a differentiating bit (rightmost set bit)
        int diff = xor & (-xor);

        int a = 0, b = 0;

        // Step 3: Split numbers into two groups based on diff bit
        foreach (int num in nums)
        {
            if ((num & diff) == 0)
                a ^= num;
            else
                b ^= num;
        }

        return new int[] { a, b };
    }
}
