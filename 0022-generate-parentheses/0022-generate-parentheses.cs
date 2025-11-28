using System;
using System.Collections.Generic;

public class Solution {
    public IList<string> GenerateParenthesis(int n) {
        var result = new List<string>();
        Backtrack(result, "", 0, 0, n);
        return result;
    }

    private void Backtrack(IList<string> result, string current, int open, int close, int max) {
        // If the current string is complete
        if (current.Length == max * 2) {
            result.Add(current);
            return;
        }

        // Add an opening parenthesis if we still have some left
        if (open < max) {
            Backtrack(result, current + "(", open + 1, close, max);
        }

        // Add a closing parenthesis if it won't break validity
        if (close < open) {
            Backtrack(result, current + ")", open, close + 1, max);
        }
    }
}
