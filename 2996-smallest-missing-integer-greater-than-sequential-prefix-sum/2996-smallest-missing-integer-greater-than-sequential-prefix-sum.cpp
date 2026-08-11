class Solution {
public:
    int missingInteger(vector<int>& nums) {
        int n = nums.size();

        // Step 1: longest sequential prefix
        int sum = nums[0];
        for (int i = 1; i < n; i++) {
            if (nums[i] == nums[i - 1] + 1)
                sum += nums[i];
            else
                break;
        }

        // Step 2: find smallest missing >= sum
        unordered_set<int> seen(nums.begin(), nums.end());
        int x = sum;
        while (seen.count(x)) x++;
        return x;
    }
};
