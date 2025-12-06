public class Solution {
    public int CountPartitions(int[] nums, int k) {
        int n = nums.Length;
        const int MOD = 1000000007;
        long[] dp = new long[n + 1];
        long[] prefix = new long[n + 1]; // prefix sums of dp
        dp[0] = 1;
        prefix[0] = 1;

        var minDeque = new LinkedList<int>();
        var maxDeque = new LinkedList<int>();

        int left = 0;
        for (int i = 0; i < n; i++) {
            // maintain deques
            while (minDeque.Count > 0 && nums[minDeque.Last.Value] >= nums[i])
                minDeque.RemoveLast();
            minDeque.AddLast(i);

            while (maxDeque.Count > 0 && nums[maxDeque.Last.Value] <= nums[i])
                maxDeque.RemoveLast();
            maxDeque.AddLast(i);

            // shrink left until valid
            while (nums[maxDeque.First.Value] - nums[minDeque.First.Value] > k) {
                if (minDeque.First.Value == left) minDeque.RemoveFirst();
                if (maxDeque.First.Value == left) maxDeque.RemoveFirst();
                left++;
            }

            // dp[i+1] = sum of dp[left..i]
            long total = (prefix[i] - (left > 0 ? prefix[left - 1] : 0) + MOD) % MOD;
            dp[i + 1] = total;
            prefix[i + 1] = (prefix[i] + dp[i + 1]) % MOD;
        }

        return (int)dp[n];
    }
}
