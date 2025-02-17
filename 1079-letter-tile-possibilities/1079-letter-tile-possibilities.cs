public class Solution {
    public int NumTilePossibilities(string tiles) {
        var count = new int[26];
        foreach (var c in tiles) {
            count[c - 'A']++;
        }
        return DFS(count);
    }
    
    private int DFS(int[] count) {
        int sum = 0;
        for (int i = 0; i < 26; i++) {
            if (count[i] == 0) continue;
            sum++;
            count[i]--;
            sum += DFS(count);
            count[i]++;
        }
        return sum;
    }
}
