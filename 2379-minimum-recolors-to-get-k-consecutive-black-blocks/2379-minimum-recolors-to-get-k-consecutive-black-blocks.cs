public class Solution {
    public int MinimumRecolors(string blocks, int k) {
        int n = blocks.Length;
        int whiteCount = 0;
        // Count white blocks in the first window
        for (int i = 0; i < k; i++) {
            if (blocks[i] == 'W') whiteCount++;
        }
        
        int res = whiteCount;
        // Slide the window across the string
        for (int i = k; i < n; i++) {
            if (blocks[i - k] == 'W') whiteCount--;
            if (blocks[i] == 'W') whiteCount++;
            res = Math.Min(res, whiteCount);
        }
        
        return res;
    }
}
