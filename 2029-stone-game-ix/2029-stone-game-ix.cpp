class Solution {
public:
    bool stoneGameIX(vector<int>& stones) {
        int c0 = 0, c1 = 0, c2 = 0;
        for (int x : stones) {
            int r = x % 3;
            if (r == 0) c0++;
            else if (r == 1) c1++;
            else c2++;
        }

        if (c0 % 2 == 0) {
            // even number of 0-mod stones
            return (c1 > 0 && c2 > 0);
        } else {
            // odd number of 0-mod stones
            return (abs(c1 - c2) > 2);
        }
    }
};
