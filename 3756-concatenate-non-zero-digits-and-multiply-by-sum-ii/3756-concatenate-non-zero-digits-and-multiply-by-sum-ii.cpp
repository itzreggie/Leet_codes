class Solution {
public:
    static const int mod= 1'000'000'007;

    vector<int> sumAndMultiply(string s, vector<vector<int>>& queries) {

    int n = s.size();

    vector<long long> prefixsum(n+1,0);    
    vector<long long> prefixx(n+1,0);    
    vector<long long> prefixcnt(n+1,0);    
    vector<long long> pow10(n+1,0); 
    pow10[0]=1;
    for (int i = 1; i<= n ; i++){
        pow10[i]= (pow10[i-1] * 10) % mod;
    }   

    for(int i =1 ; i<=n; i++){
        char c = s[i-1];
        prefixsum[i] = prefixsum[i-1];
        prefixx[i] = prefixx[i-1];
        prefixcnt[i] = prefixcnt[i-1];

        if (c != '0'){
            int d = c -'0';
            prefixsum[i] += d;
            prefixcnt[i] += 1;
            prefixx[i] = (prefixx[i] * 10 + d) % mod;
        }
    }

    vector<int> ans;
    ans.reserve(queries.size());

    for (auto &q: queries){
        int l= q[0] + 1;
        int r= q[1] + 1;

        long long sum= prefixsum[r]-prefixsum[l-1];
        long long cnt= prefixcnt[r]-prefixcnt[l-1];

        long long x = prefixx[r];
        long long leftpart = prefixx[l-1];

        long long remove = (leftpart * pow10[cnt]) % mod;
        x=(x - remove + mod) % mod;

        long long res= (x * sum) % mod;
        ans.push_back(res);
    }

    return ans;
    }
};