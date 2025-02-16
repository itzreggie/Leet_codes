public class Solution {
   public int[] ConstructDistancedSequence(int n) {
    int[] result = new int[2 * n - 1];
    bool[] used = new bool[n + 1];
    Generate(result, used, 0, n);
    return result;
}

private bool Generate(int[] result, bool[] used, int index, int n) {
    if (index == result.Length) return true;
    if (result[index] != 0) return Generate(result, used, index + 1, n);
    
    for (int i = n; i > 1; i--) {
        if (!used[i] && index + i < result.Length && result[index + i] == 0) {
            result[index] = result[index + i] = i;
            used[i] = true;
            if (Generate(result, used, index + 1, n)) return true;
            used[i] = false;
            result[index] = result[index + i] = 0;
        }
    }
    
    if (!used[1]) {
        result[index] = 1;
        used[1] = true;
        if (Generate(result, used, index + 1, n)) return true;
        used[1] = false;
        result[index] = 0;
    }
    
    return false;
}

}