public class Solution {
    public int CountSymmetricIntegers(int low, int high) {
        int count = 0;
        for (int x = low; x <= high; x++) {
            string s = x.ToString();
            // Skip numbers with an odd number of digits.
            if (s.Length % 2 != 0)
                continue;
                
            int half = s.Length / 2, sum1 = 0, sum2 = 0;
            // Sum the first half digits.
            for (int i = 0; i < half; i++) {
                sum1 += s[i] - '0';
            }
            // Sum the last half digits.
            for (int i = half; i < s.Length; i++) {
                sum2 += s[i] - '0';
            }
            // If the sums are equal, it's a symmetric number.
            if (sum1 == sum2)
                count++;
        }
        return count;
    }
}
