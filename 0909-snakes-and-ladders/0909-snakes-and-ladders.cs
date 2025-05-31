

using System;
using System.Collections.Generic;

public class Solution {
    public int SnakesAndLadders(int[][] board) {
        int n = board.Length;
        int target = n * n;
        
        // Use an array to record the number of moves to reach each square.
        // We'll index from 1 to n*n (square numbering).
        int[] moves = new int[target + 1];
        Array.Fill(moves, -1);
        
        // Start from square 1
        moves[1] = 0;
        Queue<int> queue = new Queue<int>();
        queue.Enqueue(1);
        
        while (queue.Count > 0) {
            int curr = queue.Dequeue();
            if (curr == target) {
                return moves[curr];
            }
            
            // Try all possible dice rolls [1,6]
            for (int dice = 1; dice <= 6; dice++) {
                int next = curr + dice;
                if (next > target) {
                    break;
                }
                
                // Convert "next" (square number) into (row, col) indices.
                (int r, int c) = GetBoardCoordinates(next, n);
                
                // If there's a snake or ladder, move to its destination.
                if (board[r][c] != -1) {
                    next = board[r][c];
                }
                
                // If this square hasn't been visited, record the move count.
                if (moves[next] == -1) {
                    moves[next] = moves[curr] + 1;
                    queue.Enqueue(next);
                }
            }
        }
        
        return -1;
    }

    // Helper method to convert a square number (1-indexed) into board coordinates.
    // The board is labeled in Boustrophedon style.
    private (int r, int c) GetBoardCoordinates(int s, int n) {
        int quot = (s - 1) / n;
        int rem = (s - 1) % n;
        // Row index, counting from top (0) to bottom (n-1)
        int r = n - 1 - quot;
        // Column reverses direction every row.
        int c = (quot % 2 == 0) ? rem : n - 1 - rem;
        return (r, c);
    }
}
