class Solution {
public:
    vector<int> pathsWithMaxScore(vector<string>& board) {
        int n = board.size();
        const int MOD = 1e9+7;

        vector<vector<int>> dpSum(n, vector<int>(n, -1));
        vector<vector<int>> dpCnt(n, vector<int>(n, 0));

        dpSum[n-1][n-1] = 0;   // S
        dpCnt[n-1][n-1] = 1;

        auto val = [&](int i, int j) {
            char c = board[i][j];
            if (c >= '0' && c <= '9') return c - '0';
            return 0;
        };

        for (int i = n-1; i >= 0; i--) {
            for (int j = n-1; j >= 0; j--) {
                if (board[i][j] == 'X') continue;
                if (i == n-1 && j == n-1) continue; // S

                int best = -1;
                long long ways = 0;

                auto relax = [&](int x, int y) {
                    if (x < 0 || y < 0 || x >= n || y >= n) return;   // FIX
                    if (dpSum[x][y] < 0) return;

                    if (dpSum[x][y] > best) {
                        best = dpSum[x][y];
                        ways = dpCnt[x][y];
                    } else if (dpSum[x][y] == best) {
                        ways = (ways + dpCnt[x][y]) % MOD;
                    }
                };

                relax(i+1, j);
                relax(i, j+1);
                relax(i+1, j+1);

                if (best < 0) continue;

                dpSum[i][j] = best + val(i, j);
                dpCnt[i][j] = ways % MOD;
            }
        }

        if (dpSum[0][0] < 0) return {0, 0};
        return {dpSum[0][0], dpCnt[0][0]};
    }
};
