
class Solution {
public:
    int minMoves(vector<string>& classroom, int energy) {
        int m = classroom.size();
        int n = classroom[0].size();

        int sr = -1, sc = -1;
        vector<pair<int,int>> litter;

        // Locate S and all L
        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                if (classroom[i][j] == 'S') {
                    sr = i; sc = j;
                } else if (classroom[i][j] == 'L') {
                    litter.push_back({i, j});
                }
            }
        }

        int L = litter.size();
        int fullMask = (1 << L) - 1;

        // BFS queue: r, c, energy, mask, dist
        queue<array<int,5>> q;
        q.push({sr, sc, energy, 0, 0});

        // visited[r][c][energy][mask]
        vector<vector<vector<vector<bool>>>> visited(
            m, vector<vector<vector<bool>>>(
                n, vector<vector<bool>>(
                    energy + 1, vector<bool>(1 << L, false)
                )
            )
        );

        visited[sr][sc][energy][0] = true;

        int dirs[4][2] = {{1,0},{-1,0},{0,1},{0,-1}};

        while (!q.empty()) {
            auto [r, c, e, mask, dist] = q.front();
            q.pop();

            if (mask == fullMask) return dist;

            for (auto& d : dirs) {
                int nr = r + d[0];
                int nc = c + d[1];

                if (nr < 0 || nr >= m || nc < 0 || nc >= n) continue;
                if (classroom[nr][nc] == 'X') continue;

                int ne = e - 1;
                if (ne < 0) continue;

                // Reset energy if stepping on R
                if (classroom[nr][nc] == 'R') ne = energy;

                int nmask = mask;

                // Collect litter if present
                if (classroom[nr][nc] == 'L') {
                    for (int k = 0; k < L; k++) {
                        if (litter[k].first == nr && litter[k].second == nc) {
                            nmask |= (1 << k);
                            break;
                        }
                    }
                }

                if (!visited[nr][nc][ne][nmask]) {
                    visited[nr][nc][ne][nmask] = true;
                    q.push({nr, nc, ne, nmask, dist + 1});
                }
            }
        }

        return -1;
    }
};
