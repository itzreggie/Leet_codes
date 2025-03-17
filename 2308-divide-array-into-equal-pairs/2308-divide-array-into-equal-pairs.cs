
public class Solution {
    public bool DivideArray(int[] nums) {
        Dictionary<int, int> count = new Dictionary<int, int>();
        
        // Count the frequency of each number.
        foreach (int num in nums) {
            if(count.ContainsKey(num))
                count[num]++;
            else
                count[num] = 1;
        }
        
        // Check that each frequency is even.
        foreach (var kv in count) {
            if (kv.Value % 2 != 0) {
                return false;
            }
        }
        
        return true;
    }
}
