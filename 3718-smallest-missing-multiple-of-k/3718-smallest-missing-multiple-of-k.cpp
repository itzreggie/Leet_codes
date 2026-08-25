

class Solution {
public:
    int missingMultiple(vector<int>& nums, int k) {
        unordered_set<int> s(nums.begin(), nums.end());

        long long m = k;   // first multiple
        while (true) {
            if (!s.count(m)) return m;
            m += k;
        }
    }
};
