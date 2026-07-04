class Solution {
public:
    int minScore(int n, vector<vector<int>>& roads) {
        vector<int> parent(n+1);
        iota(parent.begin(), parent.end(), 0);

        function<int(int)> find = [&](int x){
            return parent[x] == x ? x : parent[x] = find(parent[x]);
        };

        auto unite = [&](int a, int b){
            a = find(a);
            b = find(b);
            if (a != b) parent[b] = a;
        };

        // Build components
        for (auto &r : roads)
            unite(r[0], r[1]);

        int root1 = find(1);
        int ans = INT_MAX;

        // Minimum edge in the component containing 1
        for (auto &r : roads)
            if (find(r[0]) == root1)
                ans = min(ans, r[2]);

        return ans;
    }
};
