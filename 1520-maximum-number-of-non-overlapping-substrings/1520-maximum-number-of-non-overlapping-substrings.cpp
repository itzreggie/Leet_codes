
class Solution {
public:
    vector<string> maxNumOfSubstrings(string s) {
        int n = s.size();
        vector<int> L(26, n), R(26, -1);

        // Step 1: find first and last occurrence of each character
        for (int i = 0; i < n; i++) {
            int c = s[i] - 'a';
            L[c] = min(L[c], i);
            R[c] = max(R[c], i);
        }

        vector<pair<int,int>> intervals;

        // Step 2: for each position i that is the first occurrence of its char,
        // try to build a minimal valid substring
        for (int i = 0; i < n; i++) {
            if (L[s[i]-'a'] != i) continue;   // only start at first occurrence

            int end = R[s[i]-'a'];
            bool ok = true;

            // expand interval to include all occurrences of chars inside it
            for (int j = i; j <= end; j++) {
                int c = s[j] - 'a';
                if (L[c] < i) { ok = false; break; }
                end = max(end, R[c]);
            }

            if (ok) intervals.push_back({i, end});
        }

        // Step 3: greedy choose non-overlapping intervals with smallest end
        sort(intervals.begin(), intervals.end(),
             [](auto &a, auto &b){ return a.second < b.second; });

        vector<string> ans;
        int lastEnd = -1;

        for (auto &p : intervals) {
            if (p.first > lastEnd) {
                ans.push_back(s.substr(p.first, p.second - p.first + 1));
                lastEnd = p.second;
            }
        }

        return ans;
    }
};
