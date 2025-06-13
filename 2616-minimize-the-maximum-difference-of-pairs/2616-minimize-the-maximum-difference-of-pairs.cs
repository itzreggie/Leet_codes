using System;

public class Solution {
    public int MinimizeMax(int[] nums, int p) {
        Array.Sort(nums);
        int n = nums.Length;
        // The search space for the answer: min diff can be 0 and max diff is the difference between maximum and minimum element.
        int left = 0;
        int right = nums[n - 1] - nums[0];
        int ans = right;
        
        while (left <= right) {
            int mid = left + (right - left) / 2;
            if (CanFormPairs(nums, p, mid)) {
                ans = mid;
                right = mid - 1;
            } else {
                left = mid + 1;
            }
        }
        
        return ans;
    }
    
    // Checks if we can form at least p pairs with maximum difference <= maxDiff.
    private bool CanFormPairs(int[] nums, int p, int maxDiff) {
        int count = 0;
        for (int i = 1; i < nums.Length; i++) {
            if (nums[i] - nums[i - 1] <= maxDiff) {
                count++;
                i++; // Skip the next element since it forms a valid pair with the current one.
            }
        }
        return count >= p;
    }
}
