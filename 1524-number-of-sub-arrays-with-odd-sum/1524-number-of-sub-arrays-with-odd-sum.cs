public class Solution {
    public int NumOfSubarrays(int[] arr) {
        int mod = 1000000007;
        int odd = 0, even = 1;
        int sum = 0, result = 0;

        foreach (int num in arr) {
            sum += num;
            if (sum % 2 == 0) {
                result = (result + odd) % mod;
                even++;
            } else {
                result = (result + even) % mod;
                odd++;
            }
        }

        return result;
    }
}
