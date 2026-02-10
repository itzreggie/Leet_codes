public class Solution {
    public IList<IList<int>> Combine(int n, int k) {
        var result = new List<IList<int>>();
        var current = new List<int>();

        void Backtrack(int start) {
            // If we have k numbers, add a copy to the result
            if (current.Count == k) {
                result.Add(new List<int>(current));
                return;
            }

            // Loop through numbers from 'start' to 'n'
            for (int i = start; i <= n; i++) {
                current.Add(i);          // choose
                Backtrack(i + 1);        // explore
                current.RemoveAt(current.Count - 1); // un-choose
            }
        }

        Backtrack(1);
        return result;
    }
}
