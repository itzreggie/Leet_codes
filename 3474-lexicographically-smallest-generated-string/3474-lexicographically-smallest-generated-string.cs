public class Solution {
    public string GenerateString(string str1, string str2) {
        int n = str1.Length;
        int m = str2.Length;
        int L = n + m - 1;

        char[] word = Enumerable.Repeat('?', L).ToArray();
        bool[] forcedByT = new bool[L];

        // Step 1: Apply all 'T' constraints
        for (int i = 0; i < n; i++) {
            if (str1[i] == 'T') {
                for (int j = 0; j < m; j++) {
                    int pos = i + j;
                    char needed = str2[j];
                    if (word[pos] == '?' || word[pos] == needed) {
                        word[pos] = needed;
                        forcedByT[pos] = true;
                    } else {
                        return ""; // conflict
                    }
                }
            }
        }

        // Step 2: Handle 'F' constraints, assuming '?' will become 'a'
        for (int i = 0; i < n; i++) {
            if (str1[i] == 'F') {
                bool wouldMatch = true;

                // Check if this window would equal str2 after '?' -> 'a'
                for (int j = 0; j < m; j++) {
                    int pos = i + j;
                    char ch = (word[pos] == '?') ? 'a' : word[pos];
                    if (ch != str2[j]) {
                        wouldMatch = false;
                        break;
                    }
                }

                if (wouldMatch) {
                    // Need to break this window by modifying one position
                    bool broken = false;

                    // Change as far right as possible to keep lexicographically small
                    for (int j = m - 1; j >= 0 && !broken; j--) {
                        int pos = i + j;
                        if (forcedByT[pos]) continue; // can't touch T-forced positions

                        char current = word[pos];
                        char target = str2[j];

                        if (current == '?') {
                            // It would become 'a'; choose smallest != target
                            word[pos] = (target == 'a') ? 'b' : 'a';
                            broken = true;
                        } else if (current == target) {
                            // Change to smallest char != target
                            word[pos] = (target == 'a') ? 'b' : 'a';
                            broken = true;
                        }
                    }

                    if (!broken) {
                        // All positions in this window are forced by T and match str2
                        return "";
                    }
                }
            }
        }

        // Step 3: Fill remaining '?' with 'a'
        for (int i = 0; i < L; i++) {
            if (word[i] == '?')
                word[i] = 'a';
        }

        return new string(word);
    }
}
