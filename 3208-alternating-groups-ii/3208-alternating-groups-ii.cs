

public class Solution {
    public int NumberOfAlternatingGroups(int[] colors, int k) {
        int n = colors.Length;
        int count = 0;
        // Iterate over each possible starting index in the circular array.
        for (int i = 0; i < n; i++) {
            bool isAlternating = true;
            // Check adjacent pairs in the group of k tiles.
            for (int j = 0; j < k - 1; j++) {
                int current = colors[(i + j) % n];
                int next = colors[(i + j + 1) % n];
                if (current == next) {
                    isAlternating = false;
                    break;
                }
            }
            if (isAlternating) count++;
        }
        return count;
    }
}
