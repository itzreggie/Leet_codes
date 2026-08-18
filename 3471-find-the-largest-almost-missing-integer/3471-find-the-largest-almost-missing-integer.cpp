class Solution {
public:
    int largestInteger(vector<int>& nums, int k) {
        int n = nums.size();
        unordered_map<int,int> freq;

        for (int L = 0; L + k <= n; L++) {
            unordered_set<int> seen;
            for (int i = L; i < L + k; i++) {
                seen.insert(nums[i]);
            }
            for (int x : seen) {
                freq[x]++;
            }
        }

        int ans = -1;
        for (auto &p : freq) {
            if (p.second == 1) ans = max(ans, p.first);
        }
        return ans;
    }
};
