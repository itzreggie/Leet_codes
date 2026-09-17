

class Solution {
public:
    int minSumOfLengths(vector<int>& arr, int target) {
        int n = arr.size();
        const int INF = 1e9;

        vector<int> best(n, INF);   // best[i] = shortest valid subarray ending at or before i

        unordered_map<int,int> mp;
        mp[0] = -1;

        int prefix = 0;
        int ans = INF;
        int shortest = INF;

        for (int i = 0; i < n; i++) {
            prefix += arr[i];

            if (mp.count(prefix - target)) {
                int start = mp[prefix - target];
                int len = i - start;

                // combine with previous non-overlapping
                if (start >= 0 && best[start] < INF)
                    ans = min(ans, best[start] + len);

                shortest = min(shortest, len);
            }

            best[i] = shortest;
            mp[prefix] = i;
        }

        return ans >= INF ? -1 : ans;
    }
};
