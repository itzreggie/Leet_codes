public class Solution {
    public long CountSubarrays(int[] nums, int k) {
        int n = nums.Length;
        // Find the global maximum element T.
        int T = int.MinValue;
        foreach (int num in nums) {
            if (num > T) T = num;
        }
        
        long ans = 0;
        int countT = 0;  // number of occurrences of T in the current window
        int left = 0;
        
        // Use two pointers to find windows having at least k occurrences of T.
        for (int right = 0; right < n; right++) {
            if (nums[right] == T)
                countT++;
            
            // When the current window [left, right] contains at least k T's,
            // then every subarray starting at 'left' and ending at an index in [right, n-1]
            // is valid.
            while (countT >= k && left <= right) {
                ans += (n - right);
                if (nums[left] == T)
                    countT--;
                left++;
            }
        }
        
        return ans;
    }
}
