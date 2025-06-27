public class Solution {
    string s;
    int n, k, maxLen;
    int[] freq = new int[26];
    int[,] nextPos;      // nextPos[i,c] = smallest index ≥ i where s[index]==c, or -1
    char[] candidate;    
    int[] usedCount = new int[26];
    string answer = "";
    bool found = false;
    List<int> letters;   // list of c in [0..25] with freq[c] ≥ k, sorted desc

    public string LongestSubsequenceRepeatedK(string s, int k) {
        this.s = s;
        this.k = k;
        n = s.Length;
        maxLen = n / k;                    // can't repeat a sequence longer than this

        // 1) Count character freqs
        for (int i = 0; i < n; i++)
            freq[s[i] - 'a']++;

        // 2) Build descending‐order candidate chars
        letters = new List<int>();
        for (int c = 25; c >= 0; c--)
            if (freq[c] >= k)
                letters.Add(c);

        // 3) Build nextPos table for O(1) subsequence jumps
        BuildNextPos();

        // 4) Try lengths L = maxLen down to 1
        candidate = new char[maxLen];
        for (int L = maxLen; L >= 1 && !found; L--) {
            DFS(0, L);
        }

        return answer;
    }

    private void DFS(int idx, int L) {
        if (found) return;       // we already have the best answer
        if (idx == L) {
            // We have a full candidate[0..L-1], check if it repeats k times
            if (CheckRepeated(L)) {
                // first one we find is both max length (because L is descending)
                // and lexicographically largest (because letters is desc)
                answer = new string(candidate, 0, L);
                found = true;
            }
            return;
        }

        // Try each letter in descending order for lex‐max
        foreach (int c in letters) {
            // Prune: we can use c at most freq[c]/k times in *one* copy
            if (usedCount[c] + 1 > freq[c] / k) 
                continue;

            usedCount[c]++;
            candidate[idx] = (char)('a' + c);

            DFS(idx + 1, L);
            if (found) return;   // immediately unwind

            usedCount[c]--;
        }
    }

    // Check if candidate[0..L-1] repeated k times is a subsequence of s
    private bool CheckRepeated(int L) {
        int pos = 0;
        for (int repeat = 0; repeat < k; repeat++) {
            for (int i = 0; i < L; i++) {
                int c = candidate[i] - 'a';
                pos = nextPos[pos, c];
                if (pos == -1) 
                    return false;
                pos++;  
            }
        }
        return true;
    }

    // Build a table so nextPos[i,c] = index of next c at or after i, or -1
    private void BuildNextPos() {
        nextPos = new int[n + 1, 26];
        for (int c = 0; c < 26; c++)
            nextPos[n, c] = -1;
        for (int i = n - 1; i >= 0; i--) {
            for (int c = 0; c < 26; c++)
                nextPos[i, c] = nextPos[i + 1, c];
            nextPos[i, s[i] - 'a'] = i;
        }
    }
}
