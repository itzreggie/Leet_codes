
class Solution {
public:
    int minimumPushes(string word) {
        vector<int> freq(26, 0);
        for (char c : word) freq[c - 'a']++;

        vector<int> f;
        for (int x : freq) if (x > 0) f.push_back(x);

        sort(f.begin(), f.end(), greater<int>());

        int ans = 0;
        for (int i = 0; i < f.size(); i++) {
            ans += f[i] * ((i / 8) + 1);
        }
        return ans;
    }
};
