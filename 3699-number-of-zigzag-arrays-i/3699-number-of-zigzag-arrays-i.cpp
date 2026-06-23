class Solution {
public:
    static const int mod=1'000'000'007;
    int zigZagArrays(int n, int l, int r) {
        int m = r - l + 1;

       vector<long long> dp_up(m,0), dp_down(m,0);
       vector<long long> new_up(m,0), new_down(m,0);

        for(int v=0; v<m; ++v){
            dp_up[v] = 1;
            dp_down[v] = 1;
        }

       for(int i = 2; i<=n; ++i){
        vector<long long> pref_up(m + 1, 0), pref_down(m + 1, 0);
         
         for  (int v = 0; v < m; ++v){
            pref_up[v + 1] = ( pref_up[v] + dp_up[v]) % mod;
            pref_down[v + 1] = (pref_down[v] + dp_down[v]) % mod;
         }

         for (int v = 0; v < m; ++v){
            new_up[v] = pref_down[v];

            new_down[v] = (pref_up[m] - pref_up[v + 1] + mod) % mod;
         }

         dp_up = new_up;
         dp_down = new_down;
       }

       long long ans = 0;
       for (int v = 0; v < m; ++v){
        ans = (ans + dp_up[v] + dp_down[v]) % mod;
       }

       return ans;
    }
};