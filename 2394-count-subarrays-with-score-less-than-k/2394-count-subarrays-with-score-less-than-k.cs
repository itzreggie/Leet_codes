public class Solution {
    public long CountSubarrays(int[] nums, long k) {
        int n = nums.Length;
        long result = 0;
        long sum = 0;
        int left = 0;

        for (int right = 0; right < n; right++) {
            sum += nums[right];

            // Shrink the window if the score is >= k
            while (sum * (right - left + 1) >= k) {
                sum -= nums[left];
                left++;
            }

            // Count valid subarrays ending at index `right`
            result += (right - left + 1);
        }

        return result;
    }
}
