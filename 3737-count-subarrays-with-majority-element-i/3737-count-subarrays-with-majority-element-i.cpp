class Solution {
public:
    long long countMajoritySubarrays(vector<int>& nums, int target) {
        int n = nums.size();

        // Transform nums into +1 / -1
        vector<int> A(n);
        for (int i = 0; i < n; i++)
            A[i] = (nums[i] == target ? 1 : -1);

        // Prefix sums
        vector<long long> pref(n + 1, 0);
        for (int i = 0; i < n; i++)
            pref[i + 1] = pref[i] + A[i];

        // Coordinate compression
        vector<long long> sorted = pref;
        sort(sorted.begin(), sorted.end());
        sorted.erase(unique(sorted.begin(), sorted.end()), sorted.end());

        auto getIndex = [&](long long x) {
            return int(lower_bound(sorted.begin(), sorted.end(), x) - sorted.begin()) + 1;
        };

        int m = sorted.size();
        vector<long long> bit(m + 1, 0);

        auto update = [&](int i) {
            while (i <= m) {
                bit[i]++;
                i += i & -i;
            }
        };

        auto query = [&](int i) {
            long long s = 0;
            while (i > 0) {
                s += bit[i];
                i -= i & -i;
            }
            return s;
        };

        long long ans = 0;

        // Insert prefix[0]
        update(getIndex(pref[0]));

        for (int r = 1; r <= n; r++) {
            int pos = getIndex(pref[r]);
            ans += query(pos - 1);   // count prefix < current
            update(pos);
        }

        return ans;
    }
};
