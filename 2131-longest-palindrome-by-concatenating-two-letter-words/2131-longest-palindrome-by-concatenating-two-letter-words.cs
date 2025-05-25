using System.Collections.Generic;

public class Solution 
{
    public int LongestPalindrome(string[] words) 
    {
        var pairs = new Dictionary<string, int>();
        int palindromeLength = 0;
     
        foreach (string word in words)
        {
            string reversed = new string(new char[] { word[1], word[0] });
            if (pairs.ContainsKey(reversed) && pairs[reversed] > 0)
            {
                // Found a matching reverse pair; add 4 to the length.
                palindromeLength += 4;
                pairs[reversed]--;
            }
            else
            {
                if (!pairs.ContainsKey(word))
                    pairs[word] = 0;
                pairs[word]++;
            }
        }
     
        // Check for a word with identical characters to place in the middle.
        foreach (var kvp in pairs)
        {
            if (kvp.Key[0] == kvp.Key[1] && kvp.Value > 0)
            {
                palindromeLength += 2;
                break;
            }
        }
     
        return palindromeLength;
    }
}
