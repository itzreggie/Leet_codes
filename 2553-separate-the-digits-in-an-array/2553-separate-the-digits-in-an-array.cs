public class Solution {
    public int[] SeparateDigits(int[] nums) {
        List<int> result = new List<int>();

        foreach (int num in nums) {
            int x = num;
            List<int> digits = new List<int>();

            // Extract digits in reverse order
            while (x > 0) {
                digits.Add(x % 10);
                x /= 10;
            }

            // Reverse to restore correct order
            digits.Reverse();

            // Add to final result
            result.AddRange(digits);
        }

        return result.ToArray();
    }
}
