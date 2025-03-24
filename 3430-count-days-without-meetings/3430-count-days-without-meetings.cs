public class Solution {
    public int CountDays(int days, int[][] meetings) {
        // Sort the meetings by start and then end
        Array.Sort(meetings, (a, b) => a[0] == b[0] ? a[1].CompareTo(b[1]) : a[0].CompareTo(b[0]));

        int totalCovered = 0;
        int previousEnd = 0;

        foreach (var meeting in meetings) {
            int start = Math.Max(meeting[0], previousEnd + 1); // Avoid overlapping
            int end = meeting[1];

            if (start <= end) {
                totalCovered += end - start + 1; // Add non-overlapping range
                previousEnd = end;
            }
        }

        return days - totalCovered;
    }
}
