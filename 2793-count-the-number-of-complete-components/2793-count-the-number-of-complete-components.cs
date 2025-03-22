public class Solution {
    public int CountCompleteComponents(int n, int[][] edges) {
        // Build the adjacency list
        var adjList = new List<int>[n];
        for (int i = 0; i < n; i++) {
            adjList[i] = new List<int>();
        }
        foreach (var edge in edges) {
            adjList[edge[0]].Add(edge[1]);
            adjList[edge[1]].Add(edge[0]);
        }

        // To keep track of visited nodes
        var visited = new bool[n];
        int completeComponents = 0;

        // Helper function to perform DFS
        void DFS(int node, HashSet<int> component) {
            visited[node] = true;
            component.Add(node);
            foreach (var neighbor in adjList[node]) {
                if (!visited[neighbor]) {
                    DFS(neighbor, component);
                }
            }
        }

        // Count complete components
        for (int i = 0; i < n; i++) {
            if (!visited[i]) {
                var component = new HashSet<int>();
                DFS(i, component);

                // Check if the component is complete
                bool isComplete = true;
                int size = component.Count;
                foreach (var node in component) {
                    if (adjList[node].Count != size - 1) {
                        isComplete = false;
                        break;
                    }
                }
                if (isComplete) completeComponents++;
            }
        }

        return completeComponents;
    }
}
