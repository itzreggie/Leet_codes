class Solution {
public:
    int minimumDeletions(vector<int>& nums) {
        int n = nums.size();
        int minIdx = 0, maxIdx = 0;

        for (int i = 0; i < n; i++) {
            if (nums[i] < nums[minIdx]) minIdx = i;
            if (nums[i] > nums[maxIdx]) maxIdx = i;
        }

        int i = minIdx, j = maxIdx;

        int front = max(i, j) + 1;
        int back = n - min(i, j);
        int frontMinBackMax = (i + 1) + (n - j);
        int frontMaxBackMin = (j + 1) + (n - i);

        return min({front, back, frontMinBackMax, frontMaxBackMin});
    }
};
