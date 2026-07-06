class Solution {
public:
    int removeCoveredIntervals(vector<vector<int>>& intervals) {
        sort(intervals.begin(), intervals.end(),
             [](auto &a, auto &b) {
                 if (a[0] == b[0]) return a[1] > b[1];
                 return a[0] < b[0];
             });

        int count = 0;
        int maxRight = -1;

        for (auto &in : intervals) {
            int l = in[0], r = in[1];
            if (r > maxRight) {
                count++;
                maxRight = r;
            }
        }
        return count;
    }
};
