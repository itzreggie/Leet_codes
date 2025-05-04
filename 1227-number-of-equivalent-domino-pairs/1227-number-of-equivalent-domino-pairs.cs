public class Solution {
    public int NumEquivDominoPairs(int[][] dominoes) {
        Dictionary<int, int> countMap = new Dictionary<int, int>();
        int pairs = 0;

        foreach (var domino in dominoes) {
            // Normalize domino representation to be order-independent
            int key = Math.Min(domino[0], domino[1]) * 10 + Math.Max(domino[0], domino[1]);
            
            if (countMap.ContainsKey(key)) {
                pairs += countMap[key]; // Each previous occurrence contributes to the pair count
                countMap[key]++;
            } else {
                countMap[key] = 1;
            }
        }
        
        return pairs;
    }
}
