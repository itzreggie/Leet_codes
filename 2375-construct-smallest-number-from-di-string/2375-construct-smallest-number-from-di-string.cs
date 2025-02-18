public class Solution {
    public string SmallestNumber(string pattern) {
        int n = pattern.Length;
        var result = new char[n + 1];
        var stack = new Stack<int>();

        int index = 0;
        for (int i = 0; i <= n; ++i) {
            stack.Push(i + 1);
            if (i == n || pattern[i] == 'I') {
                while (stack.Count > 0) {
                    result[index++] = (char)(stack.Pop() + '0');
                }
            }
        }

        return new string(result);
    }
}
