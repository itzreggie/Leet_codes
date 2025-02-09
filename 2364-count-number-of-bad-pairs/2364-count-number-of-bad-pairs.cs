
public class Solution {
    public long CountBadPairs(int[] nums) {
        int n = nums.Length;
        long totalPairs = (long)n * (n - 1) / 2;
        Dictionary<int, int> freqMap = new Dictionary<int, int>();
        
        foreach (var key in nums.Select((val, idx) => val - idx)) {
            if (freqMap.ContainsKey(key)) {
                freqMap[key]++;
            } else {
                freqMap[key] = 1;
            }
        }
        
        long goodPairs = 0;
        foreach (int count in freqMap.Values) {
            goodPairs += (long)count * (count - 1) / 2;
        }
        
        return totalPairs - goodPairs;
    }
}