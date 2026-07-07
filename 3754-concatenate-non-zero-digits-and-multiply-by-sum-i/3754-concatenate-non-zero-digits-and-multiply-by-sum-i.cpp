class Solution {
public:
    long long sumAndMultiply(int n) {
        string s=to_string(n);
        string xstr= "";
        int sum= 0;

        for (char c : s){
            if (c != '0'){
                xstr += c;
                sum += (c - '0');
            }
        }

        long long x = xstr.empty() ? 0 : stoll(xstr);
        return x * sum;
        
    }
};