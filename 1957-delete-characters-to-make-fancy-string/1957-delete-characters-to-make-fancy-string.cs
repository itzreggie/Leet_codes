public class Solution {
    public string MakeFancyString(string s) {
        if (string.IsNullOrEmpty(s)) return s;

        var result = new StringBuilder();
        int count = 1;

        result.Append(s[0]);

        for (int i = 1; i < s.Length; i++) {
            if (s[i] == s[i - 1]) {
                count++;
            } else {
                count = 1;
            }

            if (count < 3) {
                result.Append(s[i]);
            }
        }

        return result.ToString();
    }
}
