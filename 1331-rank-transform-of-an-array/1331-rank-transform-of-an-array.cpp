class Solution {
public:
    vector<int> arrayRankTransform(vector<int>& arr) {
        int n = arr.size();
        vector<int> sorted = arr;
        sort(sorted.begin(), sorted.end());

        unordered_map<int,int> rank;
        int r = 1;

        for (int x : sorted) {
            if (!rank.count(x)) {
                rank[x] = r++;
            }
        }

        vector<int> ans(n);
        for (int i = 0; i < n; i++) {
            ans[i] = rank[arr[i]];
        }
        return ans;
    }
};
