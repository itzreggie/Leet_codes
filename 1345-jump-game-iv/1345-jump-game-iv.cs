public class Solution {
    public int MinJumps(int[] arr) {
        int n = arr.Length;
        if (n == 1) return 0;

        // Map each value to all indices where it appears
        var map = new Dictionary<int, List<int>>();
        for (int i = 0; i < n; i++) {
            if (!map.ContainsKey(arr[i])) map[arr[i]] = new List<int>();
            map[arr[i]].Add(i);
        }

        var visited = new bool[n];
        visited[0] = true;

        var queue = new Queue<int>();
        queue.Enqueue(0);

        int steps = 0;

        while (queue.Count > 0) {
            int size = queue.Count;

            while (size-- > 0) {
                int i = queue.Dequeue();

                // Reached the last index
                if (i == n - 1) return steps;

                // 1. i - 1
                if (i - 1 >= 0 && !visited[i - 1]) {
                    visited[i - 1] = true;
                    queue.Enqueue(i - 1);
                }

                // 2. i + 1
                if (i + 1 < n && !visited[i + 1]) {
                    visited[i + 1] = true;
                    queue.Enqueue(i + 1);
                }

                // 3. All j where arr[j] == arr[i]
                if (map.ContainsKey(arr[i])) {
                    foreach (int j in map[arr[i]]) {
                        if (!visited[j]) {
                            visited[j] = true;
                            queue.Enqueue(j);
                        }
                    }
                    // Important: clear to avoid reprocessing
                    map[arr[i]].Clear();
                }
            }

            steps++;
        }

        return -1; // Should never happen
    }
}
