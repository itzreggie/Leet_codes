public class Solution {
    public int MinMoves(int[] nums, int limit) {
        int n = nums.Length;
        int[] diff = new int[2 * limit + 2];

        for (int i = 0; i < n / 2; i++) {
            int a = nums[i];
            int b = nums[n - 1 - i];

            int low = Math.Min(a, b);
            int high = Math.Max(a, b);

            // 2 moves for all sums initially
            diff[2] += 2;
            diff[2 * limit + 1] -= 2;

            // 1 move for sums in [low + 1, high + limit]
            diff[low + 1] -= 1;
            diff[high + limit + 1] += 1;

            // 0 moves for sum = a + b
            diff[a + b] -= 1;
            diff[a + b + 1] += 1;
        }

        int ans = int.MaxValue;
        int curr = 0;

        for (int s = 2; s <= 2 * limit; s++) {
            curr += diff[s];
            ans = Math.Min(ans, curr);
        }

        return ans;
    }
}
