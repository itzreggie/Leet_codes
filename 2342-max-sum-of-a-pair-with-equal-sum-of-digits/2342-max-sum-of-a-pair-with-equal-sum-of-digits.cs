
public class Solution {
    public int MaximumSum(int[] nums) {
        Dictionary<int, int> sumOfDigitsToMaxValue = new Dictionary<int, int>();
        int maxSum = -1;

        foreach (int num in nums) {
            int sumOfDigits = GetSumOfDigits(num);

            if (sumOfDigitsToMaxValue.ContainsKey(sumOfDigits)) {
                maxSum = Math.Max(maxSum, sumOfDigitsToMaxValue[sumOfDigits] + num);
                sumOfDigitsToMaxValue[sumOfDigits] = Math.Max(sumOfDigitsToMaxValue[sumOfDigits], num);
            } else {
                sumOfDigitsToMaxValue[sumOfDigits] = num;
            }
        }

        return maxSum;
    }

    private int GetSumOfDigits(int num) {
        int sum = 0;
        while (num > 0) {
            sum += num % 10;
            num /= 10;
        }
        return sum;
    }
}
