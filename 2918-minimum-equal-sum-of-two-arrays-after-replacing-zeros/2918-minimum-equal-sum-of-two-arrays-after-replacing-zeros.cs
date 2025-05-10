using System;

public class Solution {
    public long MinSum(int[] nums1, int[] nums2) {
        long sum1 = 0, sum2 = 0;
        long zeros1 = 0, zeros2 = 0;
        
        foreach (int n in nums1) {
            if (n == 0)
                zeros1++;
            else
                sum1 += n;
        }
        
        foreach (int n in nums2) {
            if (n == 0)
                zeros2++;
            else
                sum2 += n;
        }
        
        long candidate = Math.Max(sum1 + zeros1, sum2 + zeros2);
        
        // If an array has no zeros, its sum is fixed.
        if (zeros1 == 0 && candidate != sum1)
            return -1;
        if (zeros2 == 0 && candidate != sum2)
            return -1;
        
        return candidate;
    }
}
