
class Solution {
public:
    vector<int> longestRepeating(string s, string qc, vector<int>& qi) {
        int n = s.size();
        
        // intervals: start -> (end, char)
        map<int, pair<int,char>> intervals;
        
        // Build initial intervals
        int i = 0;
        while (i < n) {
            int j = i;
            while (j < n && s[j] == s[i]) j++;
            intervals[i] = {j - 1, s[i]};
            i = j;
        }
        
        // max lengths
        multiset<int> lengths;
        for (auto &p : intervals)
            lengths.insert(p.second.first - p.first + 1);
        
        auto removeInterval = [&](int start) {
            auto [end, ch] = intervals[start];
            lengths.erase(lengths.find(end - start + 1));
            intervals.erase(start);
        };
        
        auto addInterval = [&](int start, int end, char ch) {
            intervals[start] = {end, ch};
            lengths.insert(end - start + 1);
        };
        
        auto findInterval = [&](int idx) {
            auto it = intervals.upper_bound(idx);
            it--;
            return it;
        };
        
        vector<int> ans;
        
        for (int k = 0; k < qi.size(); k++) {
            int idx = qi[k];
            char c = qc[k];
            
            auto it = findInterval(idx);
            int start = it->first;
            auto [end, ch] = it->second;
            
            if (ch == c) {
                ans.push_back(*lengths.rbegin());
                continue;
            }
            
            // Remove old interval
            removeInterval(start);
            
            // Left part
            if (start <= idx - 1)
                addInterval(start, idx - 1, ch);
            
            // Right part
            if (idx + 1 <= end)
                addInterval(idx + 1, end, ch);
            
            // New single-char interval at idx
            int newStart = idx, newEnd = idx;
            
            // Merge left if same char
            auto itL = intervals.upper_bound(idx);
            if (itL != intervals.begin()) {
                itL--;
                int Ls = itL->first;
                auto [Le, Lc] = itL->second;
                if (Lc == c && Le == idx - 1) {
                    removeInterval(Ls);
                    newStart = Ls;
                }
            }
            
            // Merge right if same char
            auto itR = intervals.upper_bound(idx);
            if (itR != intervals.end()) {
                int Rs = itR->first;
                auto [Re, Rc] = itR->second;
                if (Rc == c && Rs == idx + 1) {
                    removeInterval(Rs);
                    newEnd = Re;
                }
            }
            
            // Insert merged interval
            addInterval(newStart, newEnd, c);
            
            ans.push_back(*lengths.rbegin());
        }
        
        return ans;
    }
};
