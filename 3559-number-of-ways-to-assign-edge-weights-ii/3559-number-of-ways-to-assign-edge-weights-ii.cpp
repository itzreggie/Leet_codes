class Solution {
    static const int MOD = 1'000'000'007;
public:
    vector<int> assignEdgeWeights( vector<vector<int>>& edges, vector<vector<int>>& queries) {
        int n = 0;
        for ( auto &e : edges){
            n = max(n, max(e[0], e[1]));
        }

        vector<vector<int>> adj(n + 1);
        for (auto &e : edges) {
            int u = e[0], v = e[1];
            adj[u].push_back(v);
            adj[v].push_back(u);
        }

        int log=0;
        while ((1<< log) <= n) ++log;

        vector<int> depth(n + 1, 0);
        vector<vector<int>> up(n + 1, vector<int>(log));

        queue<int> q;
        vector<int> vis(n + 1, 0);
        q.push(1);
        vis[1] = 1;
        depth[1] = 0;
        up[1][0] = 1;

        while(!q.empty()) {
            int u = q.front(); q.pop();
            for (int v: adj[u]){
                if (!vis[v]) {
                    vis[v] = 1;
                    depth[v] = depth[u] + 1;
                    up[v][0] = u;
                    q.push(v);
                }
            }
        }

        for (int j = 1; j< log; ++j){
            for (int v = 1; v<=n; ++v){
                up[v][j] = up[up[v][j - 1]][j - 1];
            }
        }

        auto lca= [&](int a , int b){
            if (depth[a] < depth[b]) swap(a, b);
            int diff= depth[a] - depth[b];

            for(int j = 0; j< log; ++j){
                if (diff & ( 1 << j)) {
                    a = up[a][j];
                }
            }

            if (a==b) return a;

            for (int j = log - 1; j >=0; --j){
                if (up[a][j] != up[b][j]){
                    a = up[a][j];
                    b = up[b][j];
                }
            }

            return up[a][0];
        };
        
        vector<long long> pow2(n + 1, 1);
        for (int i = 1; i<= n; ++i){
            pow2[i]= (pow2[i-1]*2) % MOD;
        }

        vector<int> ans;
        ans.reserve(queries.size());

        for(auto &qv: queries) {
            int u = qv[0], v = qv[1];
            int l = lca(u,v);
            int k = depth[u] + depth[v] - 2 * depth[l];

            if (k == 0) {
                ans.push_back(0);
            }else {
                ans.push_back((int)pow2[k -1]);

            }
        }
        return ans;
    }
};