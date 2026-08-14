class Solution {
public:
    int maximumLengthSubstring(string s) {
    vector<int> freq(26, 0);
    int left = 0, best = 0;

    for (int right = 0; right < s.size(); right++) {
        freq[s[right] - 'a']++;

        // shrink until every character has freq ≤ 2
        while (freq[s[right] - 'a'] > 2) {
            freq[s[left] - 'a']--;
            left++;
        }

        best = max(best, right - left + 1);
    }

    return best;
}
 
    
};

