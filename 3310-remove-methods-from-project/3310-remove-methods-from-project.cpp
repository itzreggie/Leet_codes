class Solution {
public:
    vector<int> remainingMethods(int n, int k, vector<vector<int>>& invocations) {
        vector<vector<int>> g(n);
        for (auto &e : invocations) {
            g[e[0]].push_back(e[1]);
        }

        // Step 1: find suspicious set S = all reachable from k
        vector<int> suspicious(n, 0);
        queue<int> q;
        q.push(k);
        suspicious[k] = 1;

        while (!q.empty()) {
            int u = q.front(); q.pop();
            for (int v : g[u]) {
                if (!suspicious[v]) {
                    suspicious[v] = 1;
                    q.push(v);
                }
            }
        }

        // Step 2: check if any outside invokes inside
        for (auto &e : invocations) {
            int a = e[0], b = e[1];
            if (suspicious[b] && !suspicious[a]) {
                // outside invokes inside → cannot remove
                vector<int> all(n);
                iota(all.begin(), all.end(), 0);
                return all;
            }
        }

        // Step 3: removal allowed → return non-suspicious
        vector<int> result;
        for (int i = 0; i < n; i++) {
            if (!suspicious[i]) result.push_back(i);
        }
        return result;
    }
};
