
public class Solution
{
    public int[] MaxTargetNodes(int[][] edges1, int[][] edges2)
    {
        int n = edges1.Length + 1; // tree1 has n nodes [0, n-1]
        int m = edges2.Length + 1; // tree2 has m nodes [0, m-1]

        // Build undirected graphs for both trees.
        List<int>[] tree1 = BuildGraph(n, edges1);
        List<int>[] tree2 = BuildGraph(m, edges2);

        // Compute bipartite parity labeling for tree1.
        int[] parityTree1 = new int[n];
        bool[] visitedTree1 = new bool[n];
        BFSComputeParity(tree1, 0, parityTree1, visitedTree1);
        
        // Count nodes in tree1 by parity.
        int countT1Parity0 = 0, countT1Parity1 = 0;
        for (int i = 0; i < n; i++)
        {
            if (parityTree1[i] == 0)
                countT1Parity0++;
            else
                countT1Parity1++;
        }

        // Compute bipartite parity for tree2. We can choose any root, say 0.
        int[] parityTree2 = new int[m];
        bool[] visitedTree2 = new bool[m];
        BFSComputeParity(tree2, 0, parityTree2, visitedTree2);

        // Count nodes in tree2 by parity.
        int countT2Parity0 = 0, countT2Parity1 = 0;
        for (int i = 0; i < m; i++)
        {
            if (parityTree2[i] == 0)
                countT2Parity0++;
            else
                countT2Parity1++;
        }
        // When connecting tree1 to tree2, for any chosen connector b,
        // if g(b) = 0 then extra reachable tree2 nodes = (count of tree2 nodes with parity1),
        // if g(b) = 1 then extra reachable tree2 nodes = (count of tree2 nodes with parity0).
        int bestTree2 = Math.Max(countT2Parity0, countT2Parity1);

        // Build answer for each node in tree1.
        // For node i in tree1, nodes in tree1 at even distance (target nodes) are 
        // those that have the same parity as i.
        int[] answer = new int[n];
        for (int i = 0; i < n; i++)
        {
            if (parityTree1[i] == 0)
                answer[i] = countT1Parity0 + bestTree2;
            else
                answer[i] = countT1Parity1 + bestTree2;
        }

        return answer;
    }

    // Helper: Build an undirected graph from edge list.
    private List<int>[] BuildGraph(int nodeCount, int[][] edges)
    {
        List<int>[] graph = new List<int>[nodeCount];
        for (int i = 0; i < nodeCount; i++)
            graph[i] = new List<int>();

        foreach (var edge in edges)
        {
            int u = edge[0], v = edge[1];
            graph[u].Add(v);
            graph[v].Add(u);
        }
        return graph;
    }

    // Helper: Perform BFS to compute parity (0 or 1) for each node.
    private void BFSComputeParity(List<int>[] graph, int start, int[] parity, bool[] visited)
    {
        Queue<(int node, int par)> queue = new Queue<(int, int)>();
        queue.Enqueue((start, 0));
        while (queue.Count > 0)
        {
            var (node, par) = queue.Dequeue();
            if (visited[node])
                continue;
            visited[node] = true;
            parity[node] = par;
            foreach (int neighbor in graph[node])
            {
                if (!visited[neighbor])
                {
                    queue.Enqueue((neighbor, 1 - par)); // Toggle parity.
                }
            }
        }
    }
}
