public class Solution {
    public int TotalWaviness(int num1, int num2) {
        long total = 0;
        for (int x = num1; x <= num2; x++) {
            total += Waviness(x);
        }
        return (int)total;
    }

    private int Waviness(int x) {
        // extract digits in reverse order (same as Python)
        List<int> nums = new List<int>();
        while (x > 0) {
            nums.Add(x % 10);
            x /= 10;
        }

        int m = nums.Count;
        if (m < 3) return 0;

        int s = 0;
        for (int i = 1; i < m - 1; i++) {
            int left = nums[i - 1];
            int mid  = nums[i];
            int right= nums[i + 1];

            if (mid > left && mid > right) s++;
            else if (mid < left && mid < right) s++;
        }
        return s;
    }
}
