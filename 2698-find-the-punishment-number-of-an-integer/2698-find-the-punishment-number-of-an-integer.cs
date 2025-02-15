public class Solution {
    public int PunishmentNumber(int n) {
        int punishmentNumber = 0;
        for (int i = 1; i <= n; i++) {
            int square = i * i;
            if (CanBePartitioned(square, i)) {
                punishmentNumber += square;
            }
        }
        return punishmentNumber;
    }

    private bool CanBePartitioned(int square, int target) {
        string strSquare = square.ToString();
        return CanBePartitionedHelper(strSquare, target, 0);
    }

    private bool CanBePartitionedHelper(string str, int target, int index) {
        if (index == str.Length) {
            return target == 0;
        }
        int sum = 0;
        for (int i = index; i < str.Length; i++) {
            sum = sum * 10 + (str[i] - '0');
            if (sum > target) {
                break;
            }
            if (CanBePartitionedHelper(str, target - sum, i + 1)) {
                return true;
            }
        }
        return false;
    }
}
