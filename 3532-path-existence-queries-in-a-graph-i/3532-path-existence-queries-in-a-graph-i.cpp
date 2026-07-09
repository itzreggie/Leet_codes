

class Solution {
public:
    vector<bool> pathExistenceQueries(int n, vector<int>& nums, int maxDiff, vector<vector<int>>& queries) {
        vector<int> parent(n);
        iota(parent.begin(), parent.end(), 0);

        function<int(int)> find = [&](int x) {
            return parent[x] == x ? x : parent[x] = find(parent[x]);
        };

        auto unite = [&](int a, int b) {
            a = find(a);
            b = find(b);
            if (a != b) parent[b] = a;
        };

        // Union only adjacent nodes
        for (int i = 0; i + 1 < n; i++) {
            if (nums[i+1] - nums[i] <= maxDiff) {
                unite(i, i+1);
            }
        }

        vector<bool> ans;
        ans.reserve(queries.size());

        for (auto &q : queries) {
            int u = q[0], v = q[1];
            ans.push_back(find(u) == find(v));
        }

        return ans;
    }
};
