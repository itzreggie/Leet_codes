class Solution {
public:
    int uniqueXorTriplets(vector<int>& nums) {
        int n = nums.size();
        if (n <= 2) return n;          // handle small edge cases

        int p = 1;
        while (p <= n) p <<= 1;        // next power of two greater than n
        return p;
    }
};
