class Solution {
public:
    vector<int> sequentialDigits(int low, int high) {
        vector<int> result;

        for (int start = 1; start <= 9; start++) {
            int num = start;
            int next = start + 1;

            while (next <= 9) {
                num = num * 10 + next;
                next++;

                if (num >= low && num <= high) {
                    result.push_back(num);
                }
            }
        }

        sort(result.begin(), result.end());
        return result;
    }
};
