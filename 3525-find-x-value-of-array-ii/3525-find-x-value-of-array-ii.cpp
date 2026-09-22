class Solution {
    struct Node {
        int prod;
        vector<long long> cnt;

        Node() {}

        Node(int k) {
            prod = 1 % k;
            cnt.assign(k, 0);
        }
    };

    int n, k;
    vector<int> nums;
    vector<Node> tree;

    Node merge(Node &L, Node &R) {
        Node res(k);

        res.prod = (long long)L.prod * R.prod % k;

        // Prefixes entirely inside the left segment
        for (int r = 0; r < k; r++) {
            res.cnt[r] += L.cnt[r];
        }

        // Prefixes that use the entire left segment
        // and then some prefix of the right segment
        for (int r = 0; r < k; r++) {
            int newRem = (long long)L.prod * r % k;
            res.cnt[newRem] += R.cnt[r];
        }

        return res;
    }

    void build(int node, int l, int r) {
        if (l == r) {
            tree[node] = Node(k);

            int rem = nums[l] % k;
            tree[node].prod = rem;
            tree[node].cnt[rem] = 1;

            return;
        }

        int mid = (l + r) / 2;

        build(node * 2, l, mid);
        build(node * 2 + 1, mid + 1, r);

        tree[node] = merge(tree[node * 2], tree[node * 2 + 1]);
    }

    void update(int node, int l, int r, int pos, int value) {
        if (l == r) {
            tree[node] = Node(k);

            int rem = value % k;
            tree[node].prod = rem;
            tree[node].cnt[rem] = 1;

            return;
        }

        int mid = (l + r) / 2;

        if (pos <= mid) {
            update(node * 2, l, mid, pos, value);
        } else {
            update(node * 2 + 1, mid + 1, r, pos, value);
        }

        tree[node] = merge(tree[node * 2], tree[node * 2 + 1]);
    }

    Node query(int node, int l, int r, int ql, int qr) {
        if (ql <= l && r <= qr) {
            return tree[node];
        }

        int mid = (l + r) / 2;

        if (qr <= mid) {
            return query(node * 2, l, mid, ql, qr);
        }

        if (ql > mid) {
            return query(node * 2 + 1, mid + 1, r, ql, qr);
        }

        Node left = query(node * 2, l, mid, ql, qr);
        Node right = query(node * 2 + 1, mid + 1, r, ql, qr);

        return merge(left, right);
    }

public:
    vector<int> resultArray(
        vector<int>& nums,
        int k,
        vector<vector<int>>& queries
    ) {
        this->nums = nums;
        this->k = k;
        this->n = nums.size();

        tree.resize(4 * n);

        build(1, 0, n - 1);

        vector<int> answer;

        for (auto &q : queries) {
            int index = q[0];
            int value = q[1];
            int start = q[2];
            int x = q[3];

            // Update persists for future queries
            update(1, 0, n - 1, index, value);

            // Every possible remaining array is a non-empty
            // prefix of nums[start...n-1].
            Node res = query(1, 0, n - 1, start, n - 1);

            answer.push_back((int)res.cnt[x]);
        }

        return answer;
    }
};