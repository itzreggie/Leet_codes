public class Solution {
    public int SubsetXORSum(int[] nums) {
        int result = 0;

        void CalculateXOR(int index, int currentXOR) {
            if (index == nums.Length) {
                result += currentXOR;
                return;
            }
            // Include current number in XOR
            CalculateXOR(index + 1, currentXOR ^ nums[index]);
            // Exclude current number
            CalculateXOR(index + 1, currentXOR);
        }

        CalculateXOR(0, 0); // Start recursion
        return result;
    }
}
