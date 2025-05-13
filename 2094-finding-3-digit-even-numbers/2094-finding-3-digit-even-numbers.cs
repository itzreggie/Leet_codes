using System;
using System.Collections.Generic;
using System.Linq;

public class Solution {
    public int[] FindEvenNumbers(int[] digits) {
        HashSet<int> uniqueNums = new HashSet<int>();
        
        for (int i = 0; i < digits.Length; i++) {
            // Skip if the hundreds digit is 0 (leading zero not allowed)
            if (digits[i] == 0) continue;
            for (int j = 0; j < digits.Length; j++) {
                if (j == i) continue;
                for (int k = 0; k < digits.Length; k++) {
                    if (k == i || k == j) continue;
                    // Check if the number will be even (last digit is even)
                    if (digits[k] % 2 != 0) continue;
                    
                    int num = digits[i] * 100 + digits[j] * 10 + digits[k];
                    uniqueNums.Add(num);
                }
            }
        }
        
        int[] result = uniqueNums.ToArray();
        Array.Sort(result);
        return result;
    }
}
