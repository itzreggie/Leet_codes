class Solution {
public:
    string smallestSubsequence(string s) {
        vector<int> last(256, -1);
        for (int i = 0; i < s.size(); i++)
            last[s[i]] = i;

        vector<bool> inStack(256, false);
        string st;

        for (int i = 0; i < s.size(); i++) {
            char c = s[i];

            if (inStack[c]) continue;

            while (!st.empty() && c < st.back() && last[st.back()] > i) {
                inStack[st.back()] = false;
                st.pop_back();
            }

            st.push_back(c);
            inStack[c] = true;
        }

        return st;
    }
};
