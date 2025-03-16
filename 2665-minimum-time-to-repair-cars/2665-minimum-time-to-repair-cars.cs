public class Solution {
    public long RepairCars(int[] ranks, int cars) {
        long left = 1, right = (long)1e14; // Use a large enough value for the upper bound
        while (left < right) {
            long mid = left + (right - left) / 2;
            if (CanRepairInTime(ranks, cars, mid)) {
                right = mid; // Mid is feasible, try lower times
            } else {
                left = mid + 1; // Mid is not feasible, try higher times
            }
        }
        return left;
    }
    
    private bool CanRepairInTime(int[] ranks, int cars, long time) {
        long count = 0;
        foreach (int rank in ranks) {
            long maxCars = (long)Math.Sqrt(time / rank); // Calculate max cars a mechanic can repair
            count += maxCars;
            if (count >= cars) return true; // Early exit if sufficient cars are repaired
        }
        return count >= cars;
    }
}
