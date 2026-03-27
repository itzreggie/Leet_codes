public class Solution {
    public bool AreSimilar(int[][] mat, int k) {
        int m = mat.Length;
        int n = mat[0].Length;

        // Effective shifts (because shifting n times returns to original)
        int shift = k % n;

        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {

                // Calculate where the element should come from after shifting
                int expectedCol;

                if (i % 2 == 0) {
                    // Even row → left shift
                    expectedCol = (j + shift) % n;
                } else {
                    // Odd row → right shift
                    expectedCol = (j - shift + n) % n;
                }

                // If the element after k shifts doesn't match original → false
                if (mat[i][j] != mat[i][expectedCol]) {
                    return false;
                }
            }
        }

        return true;
    }
}
