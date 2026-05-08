public class Solution {
    public int MinJumps(int[] nums) {
        int n = nums.Length;
        if (n == 1) return 0;

        // Map: prime -> list of indices whose value is divisible by that prime
        var primeMap = new Dictionary<int, List<int>>();

        // Factor each number and fill primeMap
        for (int i = 0; i < n; i++) {
            int x = nums[i];
            int temp = x;

            for (int d = 2; d * d <= temp; d++) {
                if (temp % d == 0) {
                    AddPrimeIndex(primeMap, d, i);
                    while (temp % d == 0) temp /= d;
                }
            }
            if (temp > 1) {
                // temp is now a prime factor > sqrt(original x)
                AddPrimeIndex(primeMap, temp, i);
            }
        }

        // BFS
        var q = new Queue<int>();
        q.Enqueue(0);

        var visited = new bool[n];
        visited[0] = true;

        int steps = 0;

        while (q.Count > 0) {
            int size = q.Count;

            while (size-- > 0) {
                int i = q.Dequeue();
                if (i == n - 1) return steps;

                // Adjacent moves
                TryPush(i - 1);
                TryPush(i + 1);

                // Teleportation: only if nums[i] is prime
                if (IsPrime(nums[i])) {
                    int p = nums[i];
                    if (primeMap.TryGetValue(p, out var list)) {
                        foreach (int j in list) {
                            if (j != i) TryPush(j);
                        }
                        // Clear to avoid reusing this prime group
                        list.Clear();
                    }
                }
            }

            steps++;
        }

        return -1; // unreachable

        void TryPush(int idx) {
            if (idx >= 0 && idx < n && !visited[idx]) {
                visited[idx] = true;
                q.Enqueue(idx);
            }
        }
    }

    private static void AddPrimeIndex(Dictionary<int, List<int>> map, int p, int idx) {
        if (!map.TryGetValue(p, out var list)) {
            list = new List<int>();
            map[p] = list;
        }
        list.Add(idx);
    }

    private bool IsPrime(int x) {
        if (x < 2) return false;
        if (x == 2) return true;
        if (x % 2 == 0) return false;
        for (int i = 3; i * i <= x; i += 2)
            if (x % i == 0) return false;
        return true;
    }
}
