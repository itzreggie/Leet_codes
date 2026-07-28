class Solution {
public:
    string smallestPalindrome(string s) {
        vector<int> cnt(26, 0);
        for (char c : s) cnt[c - 'a']++;

        int n = s.size();
        string res(n, ' ');

        int left = 0, right = n - 1;

        // Fill symmetric pairs
        for (int ch = 0; ch < 26; ch++) {
            while (cnt[ch] >= 2) {
                res[left++] = char('a' + ch);
                res[right--] = char('a' + ch);
                cnt[ch] -= 2;
            }
        }

        // Fill middle if odd length
        for (int ch = 0; ch < 26; ch++) {
            if (cnt[ch] == 1) {
                res[left] = char('a' + ch);
                break;
            }
        }

        return res;
    }
};
