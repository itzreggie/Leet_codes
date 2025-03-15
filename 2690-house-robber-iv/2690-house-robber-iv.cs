public class Solution {
    public int MinCapability(int[] nums, int k) {
        // The candidate capability must be in the range [1, max(nums)]
        int left = 1, right = 0;
        foreach (int num in nums) {
            right = Math.Max(right, num);
        }
        
        int ans = right;
        while (left <= right) {
            int mid = left + (right - left) / 2;
            if (CanRob(nums, k, mid)) {
                ans = mid;       // mid is feasible, try for a lower capability
                right = mid - 1;
            } else {
                left = mid + 1;
            }
        }
        return ans;
    }

    private bool CanRob(int[] nums, int k, int cap) {
        int count = 0;
        for (int i = 0; i < nums.Length;) {
            if (nums[i] <= cap) {
                count++;
                i += 2; // rob this house and skip the adjacent one
            } else {
                i++;
            }
        }
        return count >= k;
    }
}
