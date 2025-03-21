public class Solution {
    public IList<string> FindAllRecipes(string[] recipes, IList<IList<string>> ingredients, string[] supplies) {
        var supplySet = new HashSet<string>(supplies);
        var indegree = new Dictionary<string, int>();
        var graph = new Dictionary<string, List<string>>();

        foreach (var recipe in recipes) {
            indegree[recipe] = 0;
        }

        for (int i = 0; i < recipes.Length; i++) {
            foreach (var ingredient in ingredients[i]) {
                if (!supplySet.Contains(ingredient)) {
                    if (!graph.ContainsKey(ingredient)) {
                        graph[ingredient] = new List<string>();
                    }
                    graph[ingredient].Add(recipes[i]);
                    indegree[recipes[i]] = indegree.GetValueOrDefault(recipes[i], 0) + 1;
                }
            }
        }

        var queue = new Queue<string>();
        foreach (var recipe in recipes) {
            if (indegree[recipe] == 0) {
                queue.Enqueue(recipe);
            }
        }

        var result = new List<string>();
        while (queue.Count > 0) {
            var current = queue.Dequeue();
            result.Add(current);

            if (graph.ContainsKey(current)) {
                foreach (var neighbor in graph[current]) {
                    indegree[neighbor]--;
                    if (indegree[neighbor] == 0) {
                        queue.Enqueue(neighbor);
                    }
                }
            }
        }

        return result;
    }
}
