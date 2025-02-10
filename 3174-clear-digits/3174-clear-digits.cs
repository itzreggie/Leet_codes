

public class Solution {
    public string ClearDigits(string s) {
        while (true) {
            int digitIndex = -1;


            for (int i = 0; i < s.Length; i++) {
                if (char.IsDigit(s[i])) {
                    digitIndex = i;
                    break;
                }
            }

            if (digitIndex == -1) break;

            int nonDigitIndex = -1;
            for (int i = digitIndex - 1; i >= 0; i--) {
                if (!char.IsDigit(s[i])) {
                    nonDigitIndex = i;
                    break;
                }
            }


            if (nonDigitIndex != -1) {
                s = s.Remove(nonDigitIndex, 1); 
                s = s.Remove(digitIndex - 1, 1); 
            } else {
                s = s.Remove(digitIndex, 1); 
            }
        }

        return s;
    }
}
