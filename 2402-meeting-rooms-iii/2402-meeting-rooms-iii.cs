using System;
using System.Collections.Generic;

public class Solution {
    public int MostBooked(int n, int[][] meetings) {
        // 1) Sort meetings by start time
        Array.Sort(meetings, (a, b) => a[0].CompareTo(b[0]));

        // 2) Min-heap of available room indices
        var availableRooms = new PriorityQueue<int, int>();
        for (int i = 0; i < n; i++) {
            availableRooms.Enqueue(i, i);
        }

        // 3) Min-heap of occupied rooms, keyed by when they free up
        //    Element = (roomIndex, endTime), priority = endTime
        var occupiedRooms = new PriorityQueue<(int room, long endTime), long>();

        // 4) Track how many meetings each room hosts
        var bookingCount = new int[n];

        // 5) Process each meeting in start-time order
        foreach (var meeting in meetings) {
            long startTime = meeting[0];
            long endTime   = meeting[1];

            // Free up any rooms that have finished by startTime
            while (occupiedRooms.Count > 0 && occupiedRooms.Peek().endTime <= startTime) {
                var freed = occupiedRooms.Dequeue();
                availableRooms.Enqueue(freed.room, freed.room);
            }

            if (availableRooms.Count > 0) {
                // Assign the meeting immediately to the smallest-numbered room
                int roomIndex = availableRooms.Dequeue();
                occupiedRooms.Enqueue((roomIndex, endTime), endTime);
                bookingCount[roomIndex]++;
            }
            else {
                // No rooms free now → delay until the next room frees
                var next = occupiedRooms.Dequeue();
                long duration = endTime - startTime;
                long newEnd   = next.endTime + duration;
                int roomIndex = next.room;

                occupiedRooms.Enqueue((roomIndex, newEnd), newEnd);
                bookingCount[roomIndex]++;
            }
        }

        // 6) Find the room with the maximum bookings (tie → smallest index)
        int mostBooked = 0;
        for (int i = 1; i < n; i++) {
            if (bookingCount[i] > bookingCount[mostBooked]) {
                mostBooked = i;
            }
        }

        return mostBooked;
    }
}
