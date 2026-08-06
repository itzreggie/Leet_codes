class Solution {
public:
    int smallestNumber(int n, int t) {
        for (long long x = n; ; x++) {
            long long prod = 1;
            long long y = x;

            while (y > 0) {
                int d = y % 10;
                prod *= d;
                y /= 10;
            }

            if (prod % t == 0)
                return x;
        }
    }
};
