using System;
using System.Text;

public class Solution {
    public string ClearStars(string s) {
        int n = s.Length;
        int INF = int.MaxValue;  // For the stars segment tree.
        
        // Build an array for the stars segment tree:
        // For each index, if s[i] is '*' then store i; otherwise, store INF.
        int[] starArr = new int[n];
        for (int i = 0; i < n; i++){
            starArr[i] = (s[i] == '*') ? i : INF;
        }
        StarSegTree starST = new StarSegTree(starArr);
        
        // Build an array for the letters segment tree.
        // For positions with a letter, store (s[i], i); for '*' positions, store the identity.
        LetterNode[] letterArr = new LetterNode[n];
        for (int i = 0; i < n; i++){
            if(s[i] == '*')
                letterArr[i] = new LetterNode('{', -1); // Sentinel: '{' is greater than 'z'
            else 
                letterArr[i] = new LetterNode(s[i], i);
        }
        LetterSegTree letterST = new LetterSegTree(letterArr);
        
        // This array tracks which positions have been “removed.”
        bool[] removed = new bool[n];
        
        // Process until there is no star left.
        while (true){
            int starPos = starST.Query(0, n); // Query over all positions for the minimum index.
            if (starPos == INF) break;         // No star found: we're done.
            
            // Query the letter tree for the range [0, starPos).
            if (starPos > 0) {
                LetterNode candidate = letterST.Query(0, starPos);
                if (candidate.letter != '{') {   // Found a valid candidate letter.
                    removed[candidate.index] = true;
                    letterST.Update(candidate.index, new LetterNode('{', -1));
                }
            }
            // Remove the star at starPos.
            removed[starPos] = true;
            starST.Update(starPos, INF);
        }
        
        // Build the final answer by taking each character (if not removed and not a star)
        // in the original order.
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < n; i++){
            if (!removed[i] && s[i] != '*') {
                sb.Append(s[i]);
            }
        }
        return sb.ToString();
    }
    
    //--------------------------------------------------------------------------
    // Segment Tree for Stars: supports point updates and range-minimum queries.
    class StarSegTree {
       int n;
       int[] tree;
       
       public StarSegTree(int[] arr) {
           n = 1;
           while (n < arr.Length) n *= 2;
           tree = new int[2 * n];
           for (int i = 0; i < 2 * n; i++) 
               tree[i] = int.MaxValue;
           // Build the leaves.
           for (int i = 0; i < arr.Length; i++){
               tree[n + i] = arr[i];
           }
           // Build the internal nodes.
           for (int i = n - 1; i > 0; i--){
               tree[i] = Math.Min(tree[2 * i], tree[2 * i + 1]);
           }
       }
       
       // Updates position i to value v.
       public void Update(int i, int v) {
           int pos = i + n;
           tree[pos] = v;
           pos /= 2;
           while (pos > 0) {
               tree[pos] = Math.Min(tree[2 * pos], tree[2 * pos + 1]);
               pos /= 2;
           }
       }
       
       // Returns the minimum value in the range [l, r).
       public int Query(int l, int r) {
           int res = int.MaxValue;
           l += n; r += n;
           while (l < r) {
               if ((l & 1) == 1) { res = Math.Min(res, tree[l]); l++; }
               if ((r & 1) == 1) { r--; res = Math.Min(res, tree[r]); }
               l /= 2; r /= 2;
           }
           return res;
       }
    }
    
    //--------------------------------------------------------------------------
    // For the letters segment tree we define a struct to hold (letter, index).
    // (The index is kept so that we can later mark the letter as removed.)
    struct LetterNode {
       public char letter;
       public int index;
       public LetterNode(char letter, int index) {
           this.letter = letter;
           this.index = index;
       }
    }
    
    // Segment tree for letters: supports querying the minimum letter (and in case
    // of ties, the one with the larger index) over a range.
    class LetterSegTree {
       int n;
       LetterNode[] tree;
       // Identity element: letter '{' (sentinel larger than any lowercase letter)
       // and an invalid index.
       private LetterNode identity = new LetterNode('{', -1);
       
       public LetterSegTree(LetterNode[] arr) {
           n = 1;
           while (n < arr.Length) n *= 2;
           tree = new LetterNode[2 * n];
           for (int i = 0; i < 2 * n; i++){
               tree[i] = identity;
           }
           for (int i = 0; i < arr.Length; i++){
               tree[n + i] = arr[i];
           }
           for (int i = n - 1; i > 0; i--){
               tree[i] = Combine(tree[2 * i], tree[2 * i + 1]);
           }
       }
       
       // Combine function: returns the node with the smaller letter; if the letters are equal,
       // returns the node with the higher index (i.e. the rightmost occurrence).
       private LetterNode Combine(LetterNode a, LetterNode b){
           if(a.letter < b.letter) return a;
           else if(a.letter > b.letter) return b;
           else { 
               return (a.index > b.index) ? a : b;
           }
       }
       
       // Updates position i to value v.
       public void Update(int i, LetterNode v) {
           int pos = i + n;
           tree[pos] = v;
           pos /= 2;
           while (pos > 0) {
               tree[pos] = Combine(tree[2 * pos], tree[2 * pos + 1]);
               pos /= 2;
           }
       }
       
       // Returns the combined node over the range [l, r).
       public LetterNode Query(int l, int r) {
           LetterNode resLeft = identity, resRight = identity;
           l += n; r += n;
           while (l < r) {
               if ((l & 1) == 1) { resLeft = Combine(resLeft, tree[l]); l++; }
               if ((r & 1) == 1) { r--; resRight = Combine(tree[r], resRight); }
               l /= 2; r /= 2;
           }
           return Combine(resLeft, resRight);
       }
    }
}
