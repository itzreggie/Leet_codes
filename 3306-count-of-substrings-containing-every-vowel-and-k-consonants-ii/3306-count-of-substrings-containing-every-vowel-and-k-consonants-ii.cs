public class Solution {
    public long CountOfSubstrings(string word, int k) {
        int n = word.Length;
        int[] prefCon = new int[n + 1];
        int[] prefA = new int[n + 1];
        int[] prefE = new int[n + 1];
        int[] prefI = new int[n + 1];
        int[] prefO = new int[n + 1];
        int[] prefU = new int[n + 1];
        
        for (int i = 0; i < n; i++) {
            char c = word[i];
            prefCon[i + 1] = prefCon[i] + (IsConsonant(c) ? 1 : 0);
            prefA[i + 1] = prefA[i] + (c == 'a' ? 1 : 0);
            prefE[i + 1] = prefE[i] + (c == 'e' ? 1 : 0);
            prefI[i + 1] = prefI[i] + (c == 'i' ? 1 : 0);
            prefO[i + 1] = prefO[i] + (c == 'o' ? 1 : 0);
            prefU[i + 1] = prefU[i] + (c == 'u' ? 1 : 0);
        }
        
        long total = 0;
        for (int i = 0; i < n; i++) {
            int jV = FindFirstAllVowels(i, n - 1, prefA, prefE, prefI, prefO, prefU);
            if (jV == -1) continue;
            
            int target = prefCon[i] + k;
            int L = LowerBound(prefCon, i + 1, n + 1, target);
            if (L == n + 1 || prefCon[L] != target)
                continue;
            int R = UpperBound(prefCon, L, n + 1, target) - 1;
            int lowIndex = Math.Max(L, jV + 1);
            if (lowIndex <= R) {
                total += (R - lowIndex + 1);
            }
        }
        return total;
    }
    
    private bool IsConsonant(char c) {
        return "aeiou".IndexOf(c) < 0;
    }
    
    private int LowerBound(int[] arr, int start, int end, int target) {
        int lo = start, hi = end;
        while (lo < hi) {
            int mid = lo + (hi - lo) / 2;
            if (arr[mid] < target)
                lo = mid + 1;
            else
                hi = mid;
        }
        return lo;
    }
    
    private int UpperBound(int[] arr, int start, int end, int target) {
        int lo = start, hi = end;
        while (lo < hi) {
            int mid = lo + (hi - lo) / 2;
            if (arr[mid] <= target)
                lo = mid + 1;
            else
                hi = mid;
        }
        return lo;
    }
    
    private int FindFirstAllVowels(int i, int high, int[] prefA, int[] prefE, int[] prefI, int[] prefO, int[] prefU) {
        int lo = i, ans = -1;
        while (lo <= high) {
            int mid = lo + (high - lo) / 2;
            if (HasAllVowels(i, mid, prefA, prefE, prefI, prefO, prefU)) {
                ans = mid;
                high = mid - 1;
            } else {
                lo = mid + 1;
            }
        }
        return ans;
    }
    
    private bool HasAllVowels(int i, int j, int[] prefA, int[] prefE, int[] prefI, int[] prefO, int[] prefU) {
        return (prefA[j + 1] - prefA[i] > 0) &&
               (prefE[j + 1] - prefE[i] > 0) &&
               (prefI[j + 1] - prefI[i] > 0) &&
               (prefO[j + 1] - prefO[i] > 0) &&
               (prefU[j + 1] - prefU[i] > 0);
    }
}
