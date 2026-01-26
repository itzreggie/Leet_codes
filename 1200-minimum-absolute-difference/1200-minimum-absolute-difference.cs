public class Solution {
    public IList<IList<int>> MinimumAbsDifference(int[] arr) {
        Array.Sort(arr);
        int n = arr.Length;

        int minDiff = int.MaxValue;
        var result = new List<IList<int>>();

        // First pass: find the minimum difference
        for (int i = 1; i < n; i++) {
            int diff = arr[i] - arr[i - 1];
            if (diff < minDiff) {
                minDiff = diff;
            }
        }

        // Second pass: collect all pairs with that difference
        for (int i = 1; i < n; i++) {
            if (arr[i] - arr[i - 1] == minDiff) {
                result.Add(new List<int> { arr[i - 1], arr[i] });
            }
        }

        return result;
    }
}
