public class Solution {
    public long GetDescentPeriods(int[] prices) {
        long count = 1;   // every single day is a descent period
        long result = 1;

        for (int i = 1; i < prices.Length; i++) {
            if (prices[i - 1] - prices[i] == 1) {
                count++;   // continue the descent
            } else {
                count = 1; // reset
            }
            result += count;
        }

        return result;
    }
}
