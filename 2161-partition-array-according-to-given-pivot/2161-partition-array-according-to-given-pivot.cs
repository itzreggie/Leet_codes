public class Solution {
    public int[] PivotArray(int[] nums, int pivot) {
        List<int> lessThanPivot = new List<int>();
        List<int> equalToPivot = new List<int>();
        List<int> greaterThanPivot = new List<int>();

        foreach (int num in nums) {
            if (num < pivot) {
                lessThanPivot.Add(num);
            } else if (num == pivot) {
                equalToPivot.Add(num);
            } else {
                greaterThanPivot.Add(num);
            }
        }

        List<int> result = new List<int>();
        result.AddRange(lessThanPivot);
        result.AddRange(equalToPivot);
        result.AddRange(greaterThanPivot);

        return result.ToArray();
    }
}
