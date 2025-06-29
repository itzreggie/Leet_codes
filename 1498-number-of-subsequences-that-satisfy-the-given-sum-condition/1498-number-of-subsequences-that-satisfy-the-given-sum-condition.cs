


using System;

public class Solution {
    const int MOD = 1000000007;

    public int NumSubseq(int[] nums, int target) {
        Array.Sort(nums);
        int n = nums.Length;
        int[] pow2 = new int[n + 1];
        pow2[0] = 1;

        // Precompute powers of 2 modulo MOD
        for (int i = 1; i <= n; i++) {
            pow2[i] = (pow2[i - 1] * 2) % MOD;
        }

        int left = 0, right = n - 1;
        int result = 0;

        while (left <= right) {
            if (nums[left] + nums[right] <= target) {
                result = (result + pow2[right - left]) % MOD;
                left++;
            } else {
                right--;
            }
        }

        return result;
    }
}
