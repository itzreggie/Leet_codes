class Solution {
public:
    vector<int> gcdValues(vector<int>& nums, vector<long long>& queries) {
        int n = nums.size();
        int maxA = *max_element(nums.begin(), nums.end());

        vector<int> freq(maxA + 1, 0);
        for (int x : nums) freq[x]++;

        vector<long long> cnt(maxA + 1, 0);
        for (int g = 1; g <= maxA; g++) {
            long long c = 0;
            for (int k = g; k <= maxA; k += g) {
                c += freq[k];
            }
            cnt[g] = c;
        }

        vector<long long> total(maxA + 1, 0);
        for (int g = 1; g <= maxA; g++) {
            if (cnt[g] >= 2) {
                total[g] = cnt[g] * (cnt[g] - 1) / 2;
            }
        }

        vector<long long> exact(maxA + 1, 0);
        for (int g = maxA; g >= 1; g--) {
            long long bad = 0;
            for (int k = 2 * g; k <= maxA; k += g) {
                bad += exact[k];
            }
            exact[g] = total[g] - bad;
        }

        vector<long long> pref(maxA + 1, 0);
        for (int g = 1; g <= maxA; g++) {
            pref[g] = pref[g - 1] + exact[g];
        }

        vector<int> ans;
        ans.reserve(queries.size());

        for (long long q : queries) {
            long long idx = q + 1;
            int lo = 1, hi = maxA, res = -1;

            while (lo <= hi) {
                int mid = (lo + hi) / 2;
                if (pref[mid] >= idx) {
                    res = mid;
                    hi = mid - 1;
                } else {
                    lo = mid + 1;
                }
            }

            ans.push_back(res);
        }

        return ans;
    }
};
