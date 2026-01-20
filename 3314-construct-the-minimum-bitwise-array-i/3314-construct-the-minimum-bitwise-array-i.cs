public class Solution {
    public int[] MinBitwiseArray(IList<int> nums) {
        int n = nums.Count;
        int[] ans = new int[n];

        for (int i = 0; i < n; i++) {
            int p = nums[i];

            // p = 2 is impossible
            if (p == 2) {
                ans[i] = -1;
                continue;
            }

            // Check if p is of the form 2^k - 1 (Mersenne prime)
            if ((p & (p + 1)) != 0) {
                ans[i] = -1;
                continue;
            }

            // Candidate smallest x
            int x = p >> 1;

            // Verify condition
            if ((x | (x + 1)) == p)
                ans[i] = x;
            else
                ans[i] = -1;
        }

        return ans;
    }
}
