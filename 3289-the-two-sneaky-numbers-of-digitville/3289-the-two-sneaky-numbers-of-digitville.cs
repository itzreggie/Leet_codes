public class Solution {
    public int[] GetSneakyNumbers(int[] nums) {
        Dictionary<int, int> countMap = new Dictionary<int, int>();
        List<int> result = new List<int>();

        foreach (int num in nums) {
            if (countMap.ContainsKey(num)) {
                countMap[num]++;
                if (countMap[num] == 2) {
                    result.Add(num);
                }
            } else {
                countMap[num] = 1;
            }
        }

        return result.ToArray();
    }
}
