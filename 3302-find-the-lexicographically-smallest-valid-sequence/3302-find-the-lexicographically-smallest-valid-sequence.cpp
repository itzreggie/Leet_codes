#include <bits/stdc++.h>
using namespace std;

class Solution {
public:
    vector<int> validSequence(string w1, string w2) {
        int n = w1.size(), m = w2.size();

        // next occurrence array
        vector<vector<int>> nxt(n + 1, vector<int>(26, -1));
        for (int c = 0; c < 26; c++) nxt[n][c] = -1;

        for (int i = n - 1; i >= 0; i--) {
            nxt[i] = nxt[i + 1];
            nxt[i][w1[i] - 'a'] = i;
        }

        // suffix match array
        vector<int> suf(n + 1, 0);
        int j = m - 1;
        for (int i = n - 1; i >= 0; i--) {
            if (j >= 0 && w1[i] == w2[j]) {
                j--;
                suf[i] = suf[i + 1] + 1;
            } else {
                suf[i] = suf[i + 1];
            }
        }

        vector<int> res;
        int cur = 0;
        bool usedMismatch = false;

        for (int i = 0; i < m; i++) {
            int c = w2[i] - 'a';
            int chosen = -1;

            // ✅ 1. direct match at current index
            if (cur < n && w1[cur] == w2[i]) {
                chosen = cur;
            }

            // ✅ 2. mismatch (only if still feasible)
            if (!usedMismatch && cur < n) {
                int remaining = m - (i + 1);
                if (suf[cur + 1] >= remaining) {
                    if (chosen == -1 || cur < chosen) {
                        chosen = cur;
                        usedMismatch = true;
                    }
                }
            }

            // ✅ 3. next occurrence match
            int matchIdx = (cur < n ? nxt[cur][c] : -1);
            if (matchIdx != -1) {
                if (chosen == -1 || matchIdx < chosen) {
                    chosen = matchIdx;
                }
            }

            if (chosen == -1) return {};

            res.push_back(chosen);
            cur = chosen + 1;
        }

        return res;
    }
};