public class Solution {
    private int m, n;
    private char[][] grid;
    private bool[,] visited;

    public bool ContainsCycle(char[][] grid) {
        this.grid = grid;
        m = grid.Length;
        n = grid[0].Length;
        visited = new bool[m, n];

        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                if (!visited[i, j]) {
                    if (DFS(i, j, -1, -1, grid[i][j])) {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    private bool DFS(int x, int y, int px, int py, char target) {
        visited[x, y] = true;

        int[] dx = { 1, -1, 0, 0 };
        int[] dy = { 0, 0, 1, -1 };

        for (int k = 0; k < 4; k++) {
            int nx = x + dx[k];
            int ny = y + dy[k];

            // Out of bounds
            if (nx < 0 || ny < 0 || nx >= m || ny >= n)
                continue;

            // Must match the same character
            if (grid[nx][ny] != target)
                continue;

            // Skip the cell we came from
            if (nx == px && ny == py)
                continue;

            // If visited and not the parent → cycle found
            if (visited[nx, ny])
                return true;

            // DFS deeper
            if (DFS(nx, ny, x, y, target))
                return true;
        }

        return false;
    }
}
