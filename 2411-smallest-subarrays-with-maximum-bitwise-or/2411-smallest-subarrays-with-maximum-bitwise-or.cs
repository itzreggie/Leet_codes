public class Solution {
    public int[] SmallestSubarrays(int[] nums) {
        int n = nums.Length;
        int[] ans = new int[n];
        
        // next[b] = earliest index ≥ i where bit b appears
        int[] next = new int[32];
        for (int b = 0; b < 32; b++) 
            next[b] = n;
        
        int suffixOr = 0;
        for (int i = n - 1; i >= 0; i--) {
            // accumulate OR of nums[i..n-1]
            suffixOr |= nums[i];
            
            // update next positions for bits set in nums[i]
            for (int b = 0; b < 32; b++) {
                if (((nums[i] >> b) & 1) == 1) 
                    next[b] = i;
            }
            
            // find the furthest index needed to cover all bits in suffixOr
            int furthest = i;
            for (int b = 0; b < 32; b++) {
                if (((suffixOr >> b) & 1) == 1 && next[b] > furthest) 
                    furthest = next[b];
            }
            
            ans[i] = furthest - i + 1;
        }
        
        return ans;
    }
}
