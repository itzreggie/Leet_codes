using System;
using System.Collections.Generic;

public class Solution {
    public int CountMaxOrSubsets(int[] nums) {
        // 1) compute the maximum OR you can get by OR-ing all numbers
        int maxOr = 0;
        foreach (int num in nums) {
            maxOr |= num;
        }
        
        // 2) dp[orVal] = how many subsets so far have bitwise OR == orVal
        // start with the empty subset having OR == 0
        var dp = new Dictionary<int, long> {{ 0, 1 }};
        
        // 3) for each number, merge in subsets that include it
        foreach (int num in nums) {
            var next = new Dictionary<int, long>(dp);
            foreach (var kv in dp) {
                int newOr = kv.Key | num;
                long newCount = kv.Value;
                
                if (next.ContainsKey(newOr))
                    next[newOr] += newCount;
                else
                    next[newOr] = newCount;
            }
            dp = next;
        }

        // 4) dp[maxOr] is the count of non-empty subsets OR-ing to maxOr
        //    except when maxOr == 0 we’ve also counted the empty subset once
        long result = dp.ContainsKey(maxOr) ? dp[maxOr] : 0;
        if (maxOr == 0) result--;  

        return (int)result;
    }
}
