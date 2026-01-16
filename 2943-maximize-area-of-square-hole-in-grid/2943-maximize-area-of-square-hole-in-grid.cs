using System;
using System.Collections.Generic;

public class Solution
{
    public int MaximizeSquareHoleArea(int n, int m, int[] hBars, int[] vBars)
    {
        int maxH = GetMaxGap(hBars);
        int maxV = GetMaxGap(vBars);

        int side = Math.Min(maxH, maxV);
        return side * side;
    }

    private int GetMaxGap(int[] bars)
    {
        Array.Sort(bars);

        int maxConsecutive = 1;
        int current = 1;

        for (int i = 1; i < bars.Length; i++)
        {
            if (bars[i] == bars[i - 1] + 1)
            {
                current++;
            }
            else
            {
                current = 1;
            }
            maxConsecutive = Math.Max(maxConsecutive, current);
        }

        // gap size = consecutive bars removed + 1
        return maxConsecutive + 1;
    }
}
