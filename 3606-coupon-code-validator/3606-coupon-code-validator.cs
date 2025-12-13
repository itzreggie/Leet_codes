using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Solution
{
    public IList<string> ValidateCoupons(string[] code, string[] businessLine, bool[] isActive)
    {
        int n = code.Length;
        var validCoupons = new List<(string Code, string BusinessLine)>();

        // Allowed categories
        HashSet<string> allowedCategories = new HashSet<string>
        {
            "electronics", "grocery", "pharmacy", "restaurant"
        };

        // Regex for valid code (alphanumeric + underscore)
        Regex regex = new Regex(@"^[a-zA-Z0-9_]+$");

        for (int i = 0; i < n; i++)
        {
            if (string.IsNullOrEmpty(code[i])) continue;
            if (!regex.IsMatch(code[i])) continue;
            if (!allowedCategories.Contains(businessLine[i])) continue;
            if (!isActive[i]) continue;

            validCoupons.Add((code[i], businessLine[i]));
        }

        // Business line order mapping
        Dictionary<string, int> order = new Dictionary<string, int>
        {
            {"electronics", 0},
            {"grocery", 1},
            {"pharmacy", 2},
            {"restaurant", 3}
        };

        validCoupons.Sort((a, b) =>
        {
            int cmp = order[a.BusinessLine].CompareTo(order[b.BusinessLine]);
            if (cmp != 0) return cmp;
            return string.Compare(a.Code, b.Code, StringComparison.Ordinal);
        });

        var result = new List<string>();
        foreach (var coupon in validCoupons)
        {
            result.Add(coupon.Code);
        }

        return result;
    }
}
