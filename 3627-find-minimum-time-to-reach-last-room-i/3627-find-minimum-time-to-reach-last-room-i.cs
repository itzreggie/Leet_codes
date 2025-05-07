public class Solution {
    public int MinTimeToReach(int[][] moveTime) {
        int n = moveTime.Length, m = moveTime[0].Length;
        int[,] dist = new int[n, m];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
                dist[i, j] = int.MaxValue;
        dist[0, 0] = 0;
        
        // Using .NET 6 PriorityQueue
        var pq = new PriorityQueue<(int x, int y), int>();
        pq.Enqueue((0, 0), 0);
        
        int[][] dirs = new int[][] {
            new int[]{1, 0}, new int[]{-1, 0},
            new int[]{0, 1}, new int[]{0, -1}
        };
        
        while(pq.Count > 0) {
            var (x, y) = pq.Dequeue();
            int curTime = dist[x, y];
            if (x == n - 1 && y == m - 1)
                return curTime;
            foreach (var d in dirs) {
                int nx = x + d[0], ny = y + d[1];
                if (nx < 0 || nx >= n || ny < 0 || ny >= m) continue;
                int available = moveTime[nx][ny];
                int arrivalTime = (curTime < available ? available : curTime) + 1;
                if(arrivalTime < dist[nx, ny]) {
                    dist[nx, ny] = arrivalTime;
                    pq.Enqueue((nx, ny), arrivalTime);
                }
            }
        }
        return -1;
    }
}
