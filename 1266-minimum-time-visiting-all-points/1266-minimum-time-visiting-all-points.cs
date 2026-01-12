public class Solution {
    public int MinTimeToVisitAllPoints(int[][] points) {
        int total = 0;

        for (int i = 1; i < points.Length; i++) {
            int dx = Math.Abs(points[i][0] - points[i - 1][0]);
            int dy = Math.Abs(points[i][1] - points[i - 1][1]);
            total += Math.Max(dx, dy);
        }

        return total;
    }
}
