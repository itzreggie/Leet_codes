class Solution {
public:

    vector<vector<int>> stMax, stMin;
    vector<int> lg;

    //we should build spare table
    void buildSparse(vector<int> & nums){

        int n = nums.size();

        lg.resize(n+1);

        for (int i = 2; i<= n; i++) {
            lg[i] = lg[i / 2] + 1;
        }

        int K= lg[n] + 1;

        stMax.assign(K, vector<int>(n));
        stMin.assign(K, vector<int>(n));

        for (int i = 0; i < n; i++){
            stMax[0][i] = nums[i];
            stMin[0][i] = nums[i];
        }

        for (int k = 1; k<K; k++){

            for (int i = 0; i + (1 << k)<= n; i++){

                stMax[k][i]= max(
                    stMax[k - 1][i],
                    stMax[k - 1][i + (1 << (k - 1))]
                );

                stMin[k][i] = min(
                    stMin[k - 1][i],
                    stMin[k - 1 ][i + (1 << (k -1))]
                );
            }
        }
    
    
    }

    long long getValue(int L, int R) {

        int j = lg[R - L + 1];

        
        int mx = max(
            stMax[j][L],
            stMax[j ][ R - (1 << j) + 1]
        );

        
        int mn = min(
            stMin[j][L],
            stMin[j ][ R - (1 << j) + 1]
        );

        return 1LL * mx- mn;
    }
   

    long long maxTotalValue(vector<int>& nums, int k) {

       int n = nums.size();

       buildSparse(nums);

       priority_queue<tuple<long long, int, int>> pq;

       for(int l= 0; l<n; l++){
         pq.push({getValue(l, n -1), l, n-1});
       }

       long long ans=0;

       while (k--){

        auto [val, l, r] = pq.top();

        pq.pop();

        ans += val;

        if (r > l) {
            pq.push({getValue(l,r-1), l, r-1});
        }


       }

       return ans;
    }
};