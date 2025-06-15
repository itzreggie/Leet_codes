using System;

public class Solution {
    public int MaxDiff(int num) {
        string s = num.ToString();
        
        // Step 1: For maximum, replace the first digit that is not '9' with '9'
        char? dMax = null;
        foreach (char c in s) {
            if (c != '9') {
                dMax = c;
                break;
            }
        }
        string a = dMax.HasValue ? s.Replace(dMax.Value, '9') : s;
        
        // Step 2: For minimum, we have two cases:
        string b;
        if (s[0] != '1') {
            // If the first digit is not '1', then replace all instances of this digit with '1'
            b = s.Replace(s[0], '1');
        } else {
            // Otherwise, the first digit is '1'
            // Find the first digit from index 1 that is not '0' and not '1'
            char? dMin = null;
            for (int i = 1; i < s.Length; i++) {
                if (s[i] != '0' && s[i] != '1') {
                    dMin = s[i];
                    break;
                }
            }
            b = dMin.HasValue ? s.Replace(dMin.Value, '0') : s;
        }
        
        return int.Parse(a) - int.Parse(b);
    }
}
