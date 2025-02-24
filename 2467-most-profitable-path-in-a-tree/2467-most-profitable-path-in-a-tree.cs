public class Solution {
    public int MostProfitablePath(int[][] edges, int bob, int[] amount) {
        int n = amount.Length;
        List<int>[] graph = new List<int>[n];
        for (int i = 0; i < n; i++) {
            graph[i] = new List<int>();
        }

        foreach (var edge in edges) {
            graph[edge[0]].Add(edge[1]);
            graph[edge[1]].Add(edge[0]);
        }

        int[] bobTime = new int[n];
        for (int i = 0; i < n; i++) {
            bobTime[i] = -1;
        }

        DfsBob(graph, bob, -1, bobTime, 0);
        
        int maxProfit = int.MinValue;
        DfsAlice(graph, 0, -1, amount, bobTime, 0, 0, ref maxProfit);
        return maxProfit;
    }

    private bool DfsBob(List<int>[] graph, int node, int parent, int[] bobTime, int time) {
        bobTime[node] = time;
        if (node == 0) {
            return true;
        }
        foreach (int neighbor in graph[node]) {
            if (neighbor != parent) {
                if (DfsBob(graph, neighbor, node, bobTime, time + 1)) {
                    return true;
                }
            }
        }
        bobTime[node] = -1;
        return false;
    }

    private void DfsAlice(List<int>[] graph, int node, int parent, int[] amount, int[] bobTime, int time, int currentProfit, ref int maxProfit) {
        if (bobTime[node] == -1 || time < bobTime[node]) {
            // Bob hasn't reached this node yet or Alice arrives earlier
            currentProfit += amount[node];
        } else if (time == bobTime[node]) {
            // Alice and Bob arrive at the same time
            currentProfit += amount[node] / 2;
        }
        // If Bob reaches earlier, Alice gets nothing at this node

        // Check if it's a leaf node
        if (node != 0 && graph[node].Count == 1) {
            // Alice stops moving at leaf node
            maxProfit = Math.Max(maxProfit, currentProfit);
            return;
        }

        foreach (int neighbor in graph[node]) {
            if (neighbor != parent) {
                DfsAlice(graph, neighbor, node, amount, bobTime, time + 1, currentProfit, ref maxProfit);
            }
        }
    }
}
