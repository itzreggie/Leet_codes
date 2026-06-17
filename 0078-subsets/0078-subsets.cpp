class Solution {
public:
    vector<vector<int>> subsets(vector<int>& nums) {
        vector<vector<int>> result;
        vector<int> curr;
        backtrack(0, nums, curr, result);
        return result;
    }

    void backtrack(int index, vector<int>& nums, vector<int>& curr, vector<vector<int>>& result){
        if (index == nums.size()){
            result.push_back(curr);
            return;
        }

        backtrack(index + 1, nums, curr, result);

        curr.push_back(nums[index]);
        backtrack(index + 1, nums, curr, result);

        curr.pop_back();
    }
};