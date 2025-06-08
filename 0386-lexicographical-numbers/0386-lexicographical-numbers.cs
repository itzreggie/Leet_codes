
public class Solution {
    public IList<int> LexicalOrder(int n) {
        List<int> result = new List<int>();
        int current = 1;

        for (int i = 0; i < n; i++) {
            result.Add(current);
            
            if (current * 10 <= n) {
                // Try moving to the next depth level (ex: 1 → 10)
                current *= 10;
            } else {
                // If not possible, increment normally
                while (current % 10 == 9 || current + 1 > n) {
                    current /= 10;
                }
                current++;
            }
        }

        return result;
    }
}
