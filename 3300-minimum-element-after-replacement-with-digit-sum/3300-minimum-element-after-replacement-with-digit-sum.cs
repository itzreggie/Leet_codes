public class Solution {
    public int MinElement(int[] nums) {
        int min = int.MaxValue;

        foreach (int n in nums) {
            int x = n;
            int sum = 0;

            while (x > 0) {
                sum += x % 10;
                x /= 10;
            }

            min = Math.Min(min, sum);
        }

        return min;
    }
}
