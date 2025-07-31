using System;
using System.Collections.Generic;

public class Solution {
    public int SubarrayBitwiseORs(int[] arr) {
        var result = new HashSet<int>();
        var curr = new HashSet<int>();
        
        foreach (int x in arr) {
            var next = new HashSet<int> { x };
            
            foreach (int y in curr) {
                next.Add(y | x);
            }
            
            curr = next;
            
            foreach (int v in curr) {
                result.Add(v);
            }
        }
        
        return result.Count;
    }
}
