
using System;
using System.Text;
using System.Collections.Generic;

public class Solution {
    public string RobotWithString(string s) {
        int n = s.Length;
        // Precompute an array "minRemaining" where minRemaining[i] holds 
        // the smallest character in the suffix s[i…n-1].
        char[] minRemaining = new char[n];
        minRemaining[n - 1] = s[n - 1];
        for (int i = n - 2; i >= 0; i--) {
            minRemaining[i] = s[i] < minRemaining[i + 1] ? s[i] : minRemaining[i + 1];
        }

        // Use a stack to simulate the robot's temporary string "t"
        var stack = new Stack<char>();
        // StringBuilder for constructing the final printed string "p"
        var result = new StringBuilder();
        int iPtr = 0;  // Pointer for the current position in string s

        // Continue until both s and the stack are empty.
        while (iPtr < n || stack.Count > 0) {
            // If the stack is not empty and either we've processed all characters in s 
            // or the top of the stack is not greater than the smallest remaining character in s,
            // then pop from the stack (simulate printing that character).
            if (stack.Count > 0 && (iPtr == n || stack.Peek() <= minRemaining[iPtr])) {
                result.Append(stack.Pop());
            } else {
                // Otherwise, remove the first character of s and push it onto the stack.
                stack.Push(s[iPtr]);
                iPtr++;
            }
        }
        
        return result.ToString();
    }
}
