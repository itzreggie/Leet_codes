public class Solution {
    public int IntersectionSizeTwo(int[][] intervals) {
        Array.Sort(intervals, (a, b) => {
            if (a[1] != b[1]) return a[1].CompareTo(b[1]);
            return b[0].CompareTo(a[0]);
        });

        var result = new List<int>();

        foreach (var interval in intervals) {
            int start = interval[0], end = interval[1];
            int count = 0;

            // Count how many elements from result are in this interval
            for (int i = result.Count - 1; i >= 0 && result[i] >= start; i--) {
                if (result[i] <= end) count++;
                if (count == 2) break;
            }

            // Add missing elements from the end of the interval
            for (int i = end; count < 2 && i >= start; i--) {
                if (!result.Contains(i)) {
                    result.Add(i);
                    count++;
                }
            }
        }

        return result.Count;
    }
}
