using System;
using System.Collections.Generic;

public class Solution {
    public bool ReorderedPowerOf2(int n) {
        string target = GetDigitSignature(n);
        
        for (int i = 0; i < 31; i++) {
            int powerOfTwo = 1 << i; // 2^i
            if (GetDigitSignature(powerOfTwo) == target) {
                return true;
            }
        }
        
        return false;
    }

    private string GetDigitSignature(int num) {
        char[] digits = num.ToString().ToCharArray();
        Array.Sort(digits);
        return new string(digits);
    }
}
