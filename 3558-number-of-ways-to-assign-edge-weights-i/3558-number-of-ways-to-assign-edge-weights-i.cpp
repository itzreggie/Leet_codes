class Solution {
public:
    
        const int mod = 1e9 + 7;

        long long modPow(long long base, long long exp){
            long long result = 1;
            base %= mod;

            while (exp > 0){
                if (exp % 2 == 1)
                    result = result * base % mod;
                base = base * base % mod;
                exp /= 2;
            }

            return result;
        }

        int assignEdgeWeights(vector<vector<int>>& edges) {
            int n = edges.size() + 1;

            vector<vector<int>> adj(n + 1);

            for (auto& e: edges) {
                adj[e[0]].push_back(e[1]);
                adj[e[1]].push_back(e[0]);
            }

            vector<int> depth(n + 1, -1);
            queue<int> q;

            q.push(1);
            depth[1]= 0;

            int maxDepth = 0;

            while(!q.empty()){
                int node = q.front();
                q.pop();

                for(int nei : adj[node]){
                    if (depth[nei] == -1) {
                        depth[nei] = depth[node] + 1;
                        maxDepth = max(maxDepth, depth[nei]);
                        q.push(nei);
                    }
                }
            }

            return modPow(2, maxDepth - 1);
        }
    
};


