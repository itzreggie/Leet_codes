using System;

public class Solution {
    public int MinMaxDifference(int num) {
        string strNum = num.ToString();
        int maxValue = int.MinValue;
        int minValue = int.MaxValue;

        foreach (char from in "0123456789") {
            foreach (char to in "0123456789") {
                string remapped = strNum.Replace(from, to);
                int value = int.Parse(remapped);
                maxValue = Math.Max(maxValue, value);
                minValue = Math.Min(minValue, value);
            }
        }

        return maxValue - minValue;
    }
}
