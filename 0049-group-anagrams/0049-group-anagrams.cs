using System;
using System.Collections.Generic;

public class Solution {
    public IList<IList<string>> GroupAnagrams(string[] strs) {
        // Dictionary to group anagrams by their sorted key
        Dictionary<string, List<string>> map = new Dictionary<string, List<string>>();

        foreach (string s in strs) {
            // Sort the string to form the key
            char[] chars = s.ToCharArray();
            Array.Sort(chars);
            string key = new string(chars);

            // Add to dictionary
            if (!map.ContainsKey(key)) {
                map[key] = new List<string>();
            }
            map[key].Add(s);
        }

        // Convert dictionary values to result list
        IList<IList<string>> result = new List<IList<string>>();
        foreach (var group in map.Values) {
            result.Add(group);
        }

        return result;
    }
}
