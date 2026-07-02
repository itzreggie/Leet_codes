class Solution {
public:
    bool findSafeWalk(vector<vector<int>>& grid, int health) {
        int m = grid.size();
        int n = grid[0].size();

        // visited[r][c] = max health we've had when reaching this cell
        vector<vector<int>> visited(m, vector<int>(n, -1));

        queue<tuple<int,int,int>> q; // r, c, health
        int startHealth = health - grid[0][0];
        if (startHealth <= 0) return false;

        q.push({0, 0, startHealth});
        visited[0][0] = startHealth;

        int dirs[4][2] = {{1,0},{-1,0},{0,1},{0,-1}};

        while (!q.empty()) {
            auto [r, c, h] = q.front();
            q.pop();

            if (r == m - 1 && c == n - 1)
                return true; // reached with health >= 1

            for (auto &d : dirs) {
                int nr = r + d[0];
                int nc = c + d[1];

                if (nr < 0 || nr >= m || nc < 0 || nc >= n)
                    continue;

                int newHealth = h - grid[nr][nc];
                if (newHealth <= 0) continue;

                // Only revisit if we arrive with more health than before
                if (newHealth > visited[nr][nc]) {
                    visited[nr][nc] = newHealth;
                    q.push({nr, nc, newHealth});
                }
            }
        }

        return false;
    }
};



