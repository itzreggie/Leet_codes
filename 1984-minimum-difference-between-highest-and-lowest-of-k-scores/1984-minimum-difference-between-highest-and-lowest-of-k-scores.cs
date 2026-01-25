public class Solution {
    public int MinimumDifference(int[] nums, int k) {
        if (k == 1) return 0;

        Array.Sort(nums);
        int n = nums.Length;
        int answer = int.MaxValue;

        for (int i = 0; i + k - 1 < n; i++) {
            int diff = nums[i + k - 1] - nums[i];
            if (diff < answer) {
                answer = diff;
            }
        }

        return answer;
    }
}
