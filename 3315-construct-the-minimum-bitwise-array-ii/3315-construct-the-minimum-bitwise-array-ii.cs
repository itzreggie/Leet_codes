public class Solution {
    public int[] MinBitwiseArray(IList<int> nums) {
        int n = nums.Count;
        int[] ans = new int[n];

        for (int i = 0; i < n; i++) {
            int p = nums[i];

            // Only prime that fails is 2
            if ((p & 1) == 0) { // even
                ans[i] = -1;
                continue;
            }

            // Count trailing ones in p
            int k = 0;
            while (((p >> k) & 1) == 1) {
                k++;
            }

            // x = p - 2^(k-1)
            int x = p - (1 << (k - 1));

            // Optional safety check (always true for odd p)
            if ( (x | (x + 1)) == p )
                ans[i] = x;
            else
                ans[i] = -1;
        }

        return ans;
    }
}
