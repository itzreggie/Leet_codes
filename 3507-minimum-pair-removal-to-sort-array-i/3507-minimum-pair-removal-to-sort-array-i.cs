public class Solution {
    public int MinimumPairRemoval(int[] nums) {
        List<int> arr = new List<int>(nums);
        int ops = 0;

        while (!IsNonDecreasing(arr)) {
            int n = arr.Count;
            int minSum = int.MaxValue;
            int idx = 0;

            // Find leftmost adjacent pair with minimum sum
            for (int i = 0; i < n - 1; i++) {
                int s = arr[i] + arr[i + 1];
                if (s < minSum) {
                    minSum = s;
                    idx = i;
                }
            }

            // Merge the pair
            int merged = arr[idx] + arr[idx + 1];
            arr[idx] = merged;
            arr.RemoveAt(idx + 1);

            ops++;
        }

        return ops;
    }

    private bool IsNonDecreasing(List<int> arr) {
        for (int i = 1; i < arr.Count; i++) {
            if (arr[i] < arr[i - 1]) return false;
        }
        return true;
    }
}
