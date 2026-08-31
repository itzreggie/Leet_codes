class Solution {
public:
    vector<int> nodesBetweenCriticalPoints(ListNode* head) {
        vector<int> critical;
        int idx = 0;

        ListNode* prev = nullptr;
        ListNode* curr = head;
        ListNode* next = head ? head->next : nullptr;

        while (next) {
            if (prev) {
                int a = prev->val;
                int b = curr->val;
                int c = next->val;

                if ((b > a && b > c) || (b < a && b < c)) {
                    critical.push_back(idx);
                }
            }
            prev = curr;
            curr = next;
            next = next->next;
            idx++;
        }

        if (critical.size() < 2) return {-1, -1};

        int minDist = INT_MAX;
        for (int i = 1; i < critical.size(); i++) {
            minDist = min(minDist, critical[i] - critical[i - 1]);
        }

        int maxDist = critical.back() - critical.front();

        return {minDist, maxDist};
    }
};
