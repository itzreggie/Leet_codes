class Solution {
public:
    bool sumGame(string num) {
        int n = num.size();
        int half = n / 2;

        int Lsum = 0, Rsum = 0;
        int Lq = 0, Rq = 0;

        for (int i = 0; i < half; i++) {
            if (num[i] == '?') Lq++;
            else Lsum += num[i] - '0';
        }

        for (int i = half; i < n; i++) {
            if (num[i] == '?') Rq++;
            else Rsum += num[i] - '0';
        }

        // If total '?' is odd, Alice wins
        if ((Lq + Rq) % 2 == 1) return true;

        // Otherwise, check if Bob can perfectly balance
        int diff = Lsum - Rsum;
        int need = 9 * ((Rq - Lq) / 2);

        return diff != need;
    }
};
