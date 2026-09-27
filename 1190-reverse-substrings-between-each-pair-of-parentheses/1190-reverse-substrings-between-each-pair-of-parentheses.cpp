

class Solution {
public:
    string reverseParentheses(string s) {
        stack<string> st;
        st.push("");

        for (char c : s) {
            if (c == '(') {
                st.push("");
            }
            else if (c == ')') {
                string current = st.top();
                st.pop();

                reverse(current.begin(), current.end());

                st.top() += current;
            }
            else {
                st.top() += c;
            }
        }

        return st.top();
    }
};