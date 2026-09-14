

class Solution {
public:
    bool isRectangleOverlap(vector<int>& A, vector<int>& B) {
        return A[0] < B[2] && A[2] > B[0] &&
               A[1] < B[3] && A[3] > B[1];
    }
};
