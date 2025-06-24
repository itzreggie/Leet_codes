


public class Solution
{
    public IList<int> FindKDistantIndices(int[] nums, int key, int k)
    {
        HashSet<int> result = new HashSet<int>();
        
        // Step 1: Find all indices j where nums[j] == key
        List<int> keyIndices = new List<int>();
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] == key)
            {
                keyIndices.Add(i);
            }
        }

        // Step 2: For each key index, add all indices i such that |i - j| <= k
        foreach (int j in keyIndices)
        {
            int start = Math.Max(0, j - k);
            int end = Math.Min(nums.Length - 1, j + k);
            for (int i = start; i <= end; i++)
            {
                result.Add(i);
            }
        }

        // Step 3: Return sorted list
        List<int> sortedResult = result.ToList();
        sortedResult.Sort();
        return sortedResult;
    }
}
