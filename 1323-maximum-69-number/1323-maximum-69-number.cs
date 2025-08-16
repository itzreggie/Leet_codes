public class Solution {
    public int Maximum69Number(int num) {
        // Convert the number to a char array
        char[] digits = num.ToString().ToCharArray();

        // Change the first '6' to '9', then break
        for (int i = 0; i < digits.Length; i++) {
            if (digits[i] == '6') {
                digits[i] = '9';
                break;
            }
        }

        // Parse back to int and return
        return int.Parse(new string(digits));
    }
}
