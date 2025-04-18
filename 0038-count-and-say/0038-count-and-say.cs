public class Solution {
    public string CountAndSay(int n) {
        string result = "1";
        for (int i = 1; i < n; i++) {
            result = GetNext(result);
        }
        return result;
    }
    
    private string GetNext(string s) {
        var sb = new System.Text.StringBuilder();
        int count = 1;
        for (int i = 1; i < s.Length; i++) {
            if (s[i] == s[i - 1]) {
                count++;
            } else {
                sb.Append(count.ToString());
                sb.Append(s[i - 1]);
                count = 1;
            }
        }
        sb.Append(count.ToString());
        sb.Append(s[s.Length - 1]);
        return sb.ToString();
    }
}
