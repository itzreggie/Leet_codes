

class Solution {
public:
    string shortestBeautifulSubstring(string s, int k) {
        int n = s.size();

        // Phase 1: find minimum length
        int l = 0, ones = 0;
        int minLen = INT_MAX;

        for (int r = 0; r < n; r++) {
            if (s[r] == '1') ones++;

            while (ones == k) {
                minLen = min(minLen, r - l + 1);
                if (s[l] == '1') ones--;
                l++;
            }
        }

        if (minLen == INT_MAX) return "";

        // Phase 2: lexicographically smallest substring of length minLen
        string best = "";
        ones = 0;

        // count ones in first window
        for (int i = 0; i < minLen; i++)
            if (s[i] == '1') ones++;

        if (ones == k) best = s.substr(0, minLen);

        // slide window
        for (int i = minLen; i < n; i++) {
            if (s[i] == '1') ones++;
            if (s[i - minLen] == '1') ones--;

            if (ones == k) {
                string candidate = s.substr(i - minLen + 1, minLen);
                if (best == "" || candidate < best)
                    best = candidate;
            }
        }

        return best;
    }
};
