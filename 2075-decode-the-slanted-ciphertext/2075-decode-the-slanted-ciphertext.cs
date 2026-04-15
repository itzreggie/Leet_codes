public class Solution {
    public string DecodeCiphertext(string encodedText, int rows) {
        int n = encodedText.Length;
        if (rows == 1) return encodedText;

        int cols = n / rows;
        char[,] grid = new char[rows, cols];

        // Fill the matrix row-wise from encodedText
        int idx = 0;
        for (int r = 0; r < rows; r++) {
            for (int c = 0; c < cols; c++) {
                grid[r, c] = encodedText[idx++];
            }
        }

        // Reconstruct original text by reading diagonally
        var result = new System.Text.StringBuilder();

        for (int startCol = 0; startCol < cols; startCol++) {
            int r = 0, c = startCol;
            while (r < rows && c < cols) {
                result.Append(grid[r, c]);
                r++;
                c++;
            }
        }

        // Remove trailing spaces
        return result.ToString().TrimEnd();
    }
}
