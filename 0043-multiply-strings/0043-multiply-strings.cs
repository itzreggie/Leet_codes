public class Solution {
    public string Multiply(string num1, string num2) {
        if (num1 == "0" || num2 == "0")
            return "0";

        int n1 = num1.Length;
        int n2 = num2.Length;
        int[] result = new int[n1 + n2];

        // Reverse iterate through both strings
        for (int i = n1 - 1; i >= 0; i--) {
            for (int j = n2 - 1; j >= 0; j--) {
                int digit1 = num1[i] - '0';
                int digit2 = num2[j] - '0';

                int sum = digit1 * digit2 + result[i + j + 1];

                result[i + j + 1] = sum % 10;
                result[i + j] += sum / 10;
            }
        }

        // Convert result array to string
        StringBuilder sb = new StringBuilder();
        foreach (int digit in result) {
            if (!(sb.Length == 0 && digit == 0)) {
                sb.Append(digit);
            }
        }

        return sb.ToString();
    }
}
