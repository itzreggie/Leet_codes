

public class Solution {
    public int[] GetNoZeroIntegers(int n) {
        for (int a = 1; a < n; a++) {
            int b = n - a;
            if (IsNoZero(a) && IsNoZero(b)) {
                return new int[] { a, b };
            }
        }
        return new int[] { }; // Should never hit this due to problem constraints
    }

    private bool IsNoZero(int num) {
        while (num > 0) {
            if (num % 10 == 0) return false;
            num /= 10;
        }
        return true;
    }
}
