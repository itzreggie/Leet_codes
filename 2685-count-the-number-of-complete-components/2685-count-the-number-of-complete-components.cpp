
class Solution {
public:
    int countCompleteComponents(int n, vector<vector<int>>& edges) {
        vector<vector<int>> adj(n);
        for (auto &e : edges) {
            adj[e[0]].push_back(e[1]);
            adj[e[1]].push_back(e[0]);
        }

        vector<int> vis(n, 0);
        int ans = 0;

        for (int i = 0; i < n; i++) {
            if (vis[i]) continue;

            // BFS/DFS to collect component
            queue<int> q;
            q.push(i);
            vis[i] = 1;

            vector<int> comp;
            comp.push_back(i);

            while (!q.empty()) {
                int u = q.front();
                q.pop();
                for (int v : adj[u]) {
                    if (!vis[v]) {
                        vis[v] = 1;
                        q.push(v);
                        comp.push_back(v);
                    }
                }
            }

            // Count edges inside this component
            int k = comp.size();
            int edgeCount = 0;

            for (int u : comp) {
                for (int v : adj[u]) {
                    if (vis[v]) edgeCount++;
                }
            }

            // Each edge counted twice in undirected graph
            edgeCount /= 2;

            // Check if complete
            if (edgeCount == k * (k - 1) / 2) {
                ans++;
            }
        }

        return ans;
    }
};
