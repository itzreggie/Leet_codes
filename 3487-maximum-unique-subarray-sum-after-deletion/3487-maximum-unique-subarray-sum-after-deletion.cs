public class Solution {
    public int MaxSum(int[] nums) {
        var seenPos = new HashSet<int>();
        int maxElem = int.MinValue;
        long posSum = 0;

        foreach (int x in nums) {
            // Track the overall maximum in case all are ≤0
            if (x > maxElem) 
                maxElem = x;

            // Only accumulate distinct positives
            if (x > 0 && seenPos.Add(x)) 
                posSum += x;
        }

        // If we saw any positive, that's our best sum.
        // Otherwise return the largest (≤0) element.
        return seenPos.Count > 0
            ? (int)posSum
            : maxElem;
    }
}
