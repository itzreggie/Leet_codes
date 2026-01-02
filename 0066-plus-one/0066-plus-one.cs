public class Solution {
    public int[] PlusOne(int[] digits) {
        int n = digits.Length;

        // Traverse from the last digit backwards
        for (int i = n - 1; i >= 0; i--) {
            if (digits[i] < 9) {
                digits[i]++;
                return digits; // No carry, return early
            }

            // If digit is 9, it becomes 0 and carry continues
            digits[i] = 0;
        }

        // If we reach here, all digits were 9
        int[] result = new int[n + 1];
        result[0] = 1; // e.g., 999 + 1 = 1000
        return result;
    }
}
