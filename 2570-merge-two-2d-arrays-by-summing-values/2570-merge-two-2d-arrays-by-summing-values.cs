public class Solution {
    public IList<IList<int>> MergeArrays(int[][] nums1, int[][] nums2) {
    Dictionary<int, int> map = new Dictionary<int, int>();

    // Merge nums1 into map
    foreach (var item in nums1) {
        if (map.ContainsKey(item[0])) {
            map[item[0]] += item[1];
        } else {
            map.Add(item[0], item[1]);
        }
    }

    // Merge nums2 into map
    foreach (var item in nums2) {
        if (map.ContainsKey(item[0])) {
            map[item[0]] += item[1];
        } else {
            map.Add(item[0], item[1]);
        }
    }

    // Convert the dictionary to a sorted list
    var result = map.Select(pair => new List<int> { pair.Key, pair.Value })
                    .OrderBy(pair => pair[0])
                    .ToList<IList<int>>();

    return result;
}

}