using System;
using System.Collections.Generic;

public class Solution {
    public long KMirror(int k, int n) {
        // (Assume k is at least 2 and at most 36.)
        long sum = 0;
        int count = 0;
        int length = 1;
        
        // Increase the length of palindromic candidates until we've found n matches.
        while (count < n) {
            foreach (string numStr in GeneratePalindromes(length)) {
                long num = long.Parse(numStr);  // decimal palindrome generated as a string
                // Convert to base-k using a custom helper (this supports bases 2...36)
                string baseK = ConvertToBase(num, k);
                if (IsPalindrome(baseK)) {
                    sum += num;
                    count++;
                    if (count == n)
                        return sum;
                }
            }
            length++;
        }
        
        return sum;
    }
    
    // Generates palindromic numbers in decimal (base-10) of a given length as strings.
    private IEnumerable<string> GeneratePalindromes(int length) {
        if (length == 1) {
            // Single-digit palindromes: 1 to 9.
            for (char c = '1'; c <= '9'; c++)
                yield return c.ToString();
            yield break;
        }
        int halfLen = (length + 1) / 2; // For even length, this equals length/2; for odd, one extra digit.
        int start = (int)Math.Pow(10, halfLen - 1);
        int end = (int)Math.Pow(10, halfLen);
        
        for (int i = start; i < end; i++) {
            string left = i.ToString();
            // For an even-length palindrome, mirror the entire left part;
            // for odd, skip the last digit when mirroring.
            string right = left.Substring(0, length / 2);
            yield return left + Reverse(right);
        }
    }
    
    // Checks if a string is a palindrome.
    private bool IsPalindrome(string s) {
        int i = 0, j = s.Length - 1;
        while (i < j) {
            if (s[i] != s[j])
                return false;
            i++;
            j--;
        }
        return true;
    }
    
    // Reverses a string.
    private string Reverse(string s) {
        char[] arr = s.ToCharArray();
        Array.Reverse(arr);
        return new string(arr);
    }
    
    // Converts a long integer to its string representation in base 'k'.
    // Supports bases from 2 up through 36.
    private string ConvertToBase(long num, int k) {
        if (k < 2 || k > 36)
            throw new ArgumentException("Base k must be between 2 and 36, inclusive.");
        if (num == 0)
            return "0";
        
        const string digits = "0123456789abcdefghijklmnopqrstuvwxyz";
        string res = "";
        while (num > 0) {
            int remainder = (int)(num % k);
            res = digits[remainder] + res;
            num /= k;
        }
        return res;
    }
}
