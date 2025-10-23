using System;
using System.Collections.Generic;

public class Solution {
    public bool HasSameDigits(string s) {
        // convert to list of digits
        List<int> digits = new List<int>(s.Length);
        foreach (char c in s) digits.Add(c - '0');

        // reduce until exactly two digits remain
        while (digits.Count > 2) {
            List<int> next = new List<int>(digits.Count - 1);
            for (int i = 0; i + 1 < digits.Count; i++) {
                next.Add((digits[i] + digits[i + 1]) % 10);
            }
            digits = next;
        }

        return digits.Count == 2 && digits[0] == digits[1];
    }
}
