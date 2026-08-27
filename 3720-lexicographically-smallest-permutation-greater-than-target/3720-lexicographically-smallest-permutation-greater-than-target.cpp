class Solution {
public:
    string lexGreaterPermutation(string s, string target) {
        int n = s.size();
        vector<int> originalCnt(26, 0);
        for (char c : s) originalCnt[c - 'a']++;

        string best = "";

        for (int i = 0; i < n; i++) {
            // Start from full counts
            vector<int> cnt = originalCnt;
            bool okPrefix = true;

            // Use target[0..i-1] as prefix
            for (int j = 0; j < i; j++) {
                int idx = target[j] - 'a';
                if (cnt[idx] == 0) {
                    okPrefix = false;
                    break;
                }
                cnt[idx]--;
            }

            if (!okPrefix) break; // further i will also fail

            int t = target[i] - 'a';

            // Try all letters > target[i]
            for (int c = t + 1; c < 26; c++) {
                if (cnt[c] == 0) continue;

                // Build candidate
                string cand = target.substr(0, i);
                cand.push_back('a' + c);
                cnt[c]--;

                // Fill the rest with smallest possible letters
                for (int d = 0; d < 26; d++) {
                    while (cnt[d] > 0) {
                        cand.push_back('a' + d);
                        cnt[d]--;
                    }
                }

                if (best.empty() || cand < best) {
                    best = cand;
                }

                // Only need smallest c > t, so break
                break;
            }
        }

        return best;
    }
};
