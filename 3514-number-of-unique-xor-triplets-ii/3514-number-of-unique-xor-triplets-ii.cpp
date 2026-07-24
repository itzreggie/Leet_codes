#include <bits/stdc++.h>
using namespace std;

class Solution {
public:
    int uniqueXorTriplets(vector<int>& nums) {
        int n = nums.size();
        int maxEl = 0;
        for (int x : nums) maxEl = max(maxEl, x);

        int T = 1;
        while (T <= maxEl) T <<= 1;  // next power of two > maxEl

        vector<bool> s1(T, false), s2(T, false);

        // All XORs of pairs (i <= j)
        for (int i = 0; i < n; ++i) {
            for (int j = i; j < n; ++j) {
                s1[nums[i] ^ nums[j]] = true;
            }
        }

        // For each pair XOR value, XOR with every element to get triplet XORs
        for (int v = 0; v < T; ++v) {
            if (s1[v]) {
                for (int x : nums) {
                    s2[v ^ x] = true;
                }
            }
        }

        int count = 0;
        for (int v = 0; v < T; ++v) {
            if (s2[v]) ++count;
        }

        return count;
    }
};
