class Solution {
public:
    vector<int> pathExistenceQueries(int n, vector<int>& nums, int maxDiff, vector<vector<int>>& queries) {
        // 1. Sort indices by nums[i]
        vector<int> idx(n);
        iota(idx.begin(), idx.end(), 0);
        sort(idx.begin(), idx.end(), [&](int a, int b){
            return nums[a] < nums[b];
        });

        // pos[u] = position of node u in sorted order
        vector<int> pos(n);
        for (int i = 0; i < n; i++) {
            pos[idx[i]] = i;
        }

        // 2. Compute reach[i] = furthest index reachable in ONE step
        vector<int> reach(n);
        int j = 0;
        for (int i = 0; i < n; i++) {
            if (j < i) j = i;
            while (j + 1 < n && nums[idx[j + 1]] - nums[idx[i]] <= maxDiff) {
                j++;
            }
            reach[i] = j;  // can go from i to any k in [i..j] in one edge
        }

        // 3. Binary lifting on reach[]
        // We need max power of two >= n to cover paths up to length n-1
        int LOG = 0;
        while ((1 << LOG) <= 2 * n) LOG++;  // safe upper bound

        vector<vector<int>> up(LOG, vector<int>(n));
        for (int i = 0; i < n; i++) {
            up[0][i] = reach[i];
        }
        for (int k = 1; k < LOG; k++) {
            for (int i = 0; i < n; i++) {
                up[k][i] = up[k - 1][ up[k - 1][i] ];
            }
        }

        // 4. Answer queries
        vector<int> ans;
        ans.reserve(queries.size());

        for (auto &q : queries) {
            int u = q[0], v = q[1];

            if (u == v) {
                ans.push_back(0);
                continue;
            }

            int a = pos[u];
            int b = pos[v];

            // Always move from smaller value to larger value
            if (a > b) swap(a, b);

            // If even the largest jump can't reach b → unreachable
            if (up[LOG - 1][a] < b) {
                ans.push_back(-1);
                continue;
            }

            int steps = 0;
            int cur = a;

            // Greedy binary lifting: push cur as far as possible while still < b
            for (int k = LOG - 1; k >= 0; k--) {
                if (up[k][cur] < b) {
                    cur = up[k][cur];
                    steps += (1 << k);
                }
            }

            // One final hop to cross into >= b
            steps += 1;

            ans.push_back(steps);
        }

        return ans;
    }
};
