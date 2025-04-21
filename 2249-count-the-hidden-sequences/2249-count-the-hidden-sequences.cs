public class Solution {
    public int NumberOfArrays(int[] differences, int lower, int upper) {
        long current = 0, min = 0, max = 0;
        
        foreach (var diff in differences) {
            current += diff;
            min = Math.Min(min, current);
            max = Math.Max(max, current);
        }

        long range = (upper - lower) + 1;
        long possibleValues = range - (max - min);

        return possibleValues > 0 ? (int)possibleValues : 0;
    }
}
