class Solution {
public:
    vector<int> findMissingElements(vector<int>& nums) {
        unordered_set<int> s(nums.begin(), nums.end());
        if (s.empty()) return {};

        int mn = *min_element(nums.begin(), nums.end());
        int mx = *max_element(nums.begin(), nums.end());

        vector<int> missing;
        for (int x = mn; x <= mx; x++) {
            if (!s.count(x)) missing.push_back(x);
        }
        return missing;
    }
};
