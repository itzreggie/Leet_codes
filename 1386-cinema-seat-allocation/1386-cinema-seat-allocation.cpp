class Solution {
public:
    int maxNumberOfFamilies(int n, vector<vector<int>>& reservedSeats) {
        unordered_map<int, int> mask;  // row -> bitmask of reserved seats

        for (auto &r : reservedSeats) {
            int row = r[0], seat = r[1];
            mask[row] |= (1 << seat);
        }

        int ans = 0;

        // Rows with no reservations → 2 families each
        ans += (n - mask.size()) * 2;

        for (auto &p : mask) {
            int m = p.second;

            bool A = !(m & ((1<<2)|(1<<3)|(1<<4)|(1<<5)));
            bool B = !(m & ((1<<4)|(1<<5)|(1<<6)|(1<<7)));
            bool C = !(m & ((1<<6)|(1<<7)|(1<<8)|(1<<9)));

            if (A && C) ans += 2;
            else if (A || B || C) ans += 1;
        }

        return ans;
    }
};
