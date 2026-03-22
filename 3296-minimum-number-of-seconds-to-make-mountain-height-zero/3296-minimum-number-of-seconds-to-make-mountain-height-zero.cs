public class Solution {
    public long MinNumberOfSeconds(int mountainHeight, int[] workerTimes) {
        long left = 0;
        long right = (long)1e18; // large upper bound

        while (left < right) {
            long mid = (left + right) / 2;

            if (CanFinish(mountainHeight, workerTimes, mid))
                right = mid;
            else
                left = mid + 1;
        }

        return left;
    }

    private bool CanFinish(int mountainHeight, int[] workerTimes, long timeLimit) {
        long total = 0;

        foreach (int t in workerTimes) {
            // Solve t * x(x+1)/2 <= timeLimit
            // x^2 + x - (2*timeLimit / t) <= 0
            long maxX = SolveMaxX(timeLimit, t);
            total += maxX;

            if (total >= mountainHeight)
                return true;
        }

        return total >= mountainHeight;
    }

    private long SolveMaxX(long timeLimit, long t) {
        if (t > timeLimit) return 0;

        // Solve quadratic: x^2 + x - (2*timeLimit / t) <= 0
        double C = (double)(2.0 * timeLimit / t);
        double x = (-1 + Math.Sqrt(1 + 4 * C)) / 2.0;

        return (long)x;
    }
}
