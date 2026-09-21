
class Solution {
public:
    vector<long long> resultArray(vector<int>& nums, int k) {
        vector<long long> ans(k, 0);
        vector<long long> dp(k, 0);

        for (int num : nums) {
            vector<long long> ndp(k, 0);
            
            int val = num % k;

            // Start a new subarray with just nums[i]
            ndp[val]++;

            // Extend every previous subarray
            for (int r = 0; r < k; r++) {
                int newRemainder = (r * val) % k;
                ndp[newRemainder] += dp[r];
            }

            // Add all subarrays ending at this position
            for (int r = 0; r < k; r++) {
                ans[r] += ndp[r];
            }

            dp = ndp;
        }

        return ans;
    }
};