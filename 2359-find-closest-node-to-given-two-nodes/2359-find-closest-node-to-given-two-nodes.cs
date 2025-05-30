

using System;
using System.Collections.Generic;

public class Solution 
{
    public int ClosestMeetingNode(int[] edges, int node1, int node2) 
    {
        int n = edges.Length;

        // Compute shortest distances from node1 and node2
        int[] dist1 = ComputeDistances(edges, node1, n);
        int[] dist2 = ComputeDistances(edges, node2, n);

        // Find the node with minimum max distance
        int minMaxDist = int.MaxValue;
        int bestNode = -1;

        for (int i = 0; i < n; i++) 
        {
            if (dist1[i] == int.MaxValue || dist2[i] == int.MaxValue) 
                continue; // Not reachable from both
            
            int maxDist = Math.Max(dist1[i], dist2[i]);
            
            if (maxDist < minMaxDist) 
            {
                minMaxDist = maxDist;
                bestNode = i;
            }
        }

        return bestNode;
    }

    private int[] ComputeDistances(int[] edges, int start, int n) 
    {
        int[] dist = new int[n];
        Array.Fill(dist, int.MaxValue); // Initialize distances as infinite

        int curr = start, step = 0;
        while (curr != -1 && dist[curr] == int.MaxValue) 
        {
            dist[curr] = step;
            step++;
            curr = edges[curr];
        }

        return dist;
    }
}
