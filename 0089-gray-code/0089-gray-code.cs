public class Solution {
    public IList<int> GrayCode(int n) {
        var result = new List<int>();
        int size = 1 << n; // 2^n

        for (int i = 0; i < size; i++)
        {
            result.Add(i ^ (i >> 1));
        }

        return result;
    }
}
