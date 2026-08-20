class Solution {
public:
    vector<int> resultArray(vector<int>& nums) {
        vector<int> arr1, arr2;

        arr1.push_back(nums[0]);
        arr2.push_back(nums[1]);

        for (int i = 2; i < nums.size(); i++) {
            if (arr1.back() > arr2.back()) {
                arr1.push_back(nums[i]);
            } else {
                arr2.push_back(nums[i]);
            }
        }

        // concatenate
        vector<int> res;
        res.reserve(nums.size());
        for (int x : arr1) res.push_back(x);
        for (int x : arr2) res.push_back(x);
        return res;
    }
};
