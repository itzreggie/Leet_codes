class Solution {
public:
    int largestOverlap(vector<vector<int>>& img1, vector<vector<int>>& img2) {
        int n = img1.size();
        int ans = 0;

        auto overlap = [&](int dx, int dy) {
            int cnt = 0;
            for (int i = 0; i < n; i++) {
                int x = i + dx;
                if (x < 0 || x >= n) continue;
                for (int j = 0; j < n; j++) {
                    int y = j + dy;
                    if (y < 0 || y >= n) continue;
                    if (img1[i][j] == 1 && img2[x][y] == 1)
                        cnt++;
                }
            }
            return cnt;
        };

        for (int dx = -n + 1; dx <= n - 1; dx++) {
            for (int dy = -n + 1; dy <= n - 1; dy++) {
                ans = max(ans, overlap(dx, dy));
            }
        }

        return ans;
    }
};

