public class Solution {
    public int NumberOfSubstrings(string s) {
        int count = 0;
        int[] freq = new int[3];
        int left = 0;
        
        for (int right = 0; right < s.Length; right++) {
            freq[s[right] - 'a']++;
            
            while (freq[0] > 0 && freq[1] > 0 && freq[2] > 0) {
                count += s.Length - right;
                freq[s[left] - 'a']--;
                left++;
            }
        }
        
        return count;
    }
}
