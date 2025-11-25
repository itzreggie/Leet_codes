public class Solution {
    public bool IsValidSudoku(char[][] board) {
        // Use hash sets to track seen numbers in rows, columns, and boxes
        HashSet<char>[] rows = new HashSet<char>[9];
        HashSet<char>[] cols = new HashSet<char>[9];
        HashSet<char>[] boxes = new HashSet<char>[9];

        for (int i = 0; i < 9; i++) {
            rows[i] = new HashSet<char>();
            cols[i] = new HashSet<char>();
            boxes[i] = new HashSet<char>();
        }

        for (int r = 0; r < 9; r++) {
            for (int c = 0; c < 9; c++) {
                char val = board[r][c];
                if (val == '.') continue; // skip empty cells

                // Check row
                if (rows[r].Contains(val)) return false;
                rows[r].Add(val);

                // Check column
                if (cols[c].Contains(val)) return false;
                cols[c].Add(val);

                // Check 3x3 sub-box
                int boxIndex = (r / 3) * 3 + (c / 3);
                if (boxes[boxIndex].Contains(val)) return false;
                boxes[boxIndex].Add(val);
            }
        }

        return true;
    }
}
