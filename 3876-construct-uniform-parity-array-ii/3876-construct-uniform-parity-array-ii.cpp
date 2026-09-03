

class Solution {
public:
    bool uniformArray(vector<int>& nums1) {
        int n = nums1.size();
        int even = 0, odd = 0;
        int mn = INT_MAX;

        for (int x : nums1) {
            mn = min(mn, x);
            if (x % 2 == 0) even++;
            else odd++;
        }

        // If all numbers already have same parity → we can just copy them
        if (even == 0 || odd == 0) return true;

        // Mixed parity:
        // If the global minimum is odd, we can make everything odd.
        // If the global minimum is even, it's impossible.
        return (mn % 2 == 1);
    }
};
