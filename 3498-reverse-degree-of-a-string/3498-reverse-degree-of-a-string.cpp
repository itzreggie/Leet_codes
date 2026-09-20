
class Solution {
public:
    int reverseDegree(string s) {
        int ans = 0;

        for (int i = 0; i < s.size(); i++) {
            // 'a' -> 26, 'b' -> 25, ..., 'z' -> 1
            int reverseValue = 'z' - s[i] + 1;

            // Position is i + 1
            ans += reverseValue * (i + 1);
        }

        return ans;
    }
};