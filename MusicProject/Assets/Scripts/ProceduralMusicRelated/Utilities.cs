using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class Utilities
{
    public static int[] calculateRandomPush(int[] possibleValues, int max) {
    
        List<int> notesList = new List<int>();
        int sum = 0;

        bool searching = true;
        // Algorithm: Pick random numbers and append, validate on each append that the sum of the values is not bigger than the size allowed.
        while(searching){
            sum = notesList.Sum();
            if (sum < max)
            {
                // Pick random number. Either 2 or 3.
                int rand = possibleValues[Random.Range(0, possibleValues.Length)];
                // If value to append is not bigger than the size allowed, add it to array.
                // i.e. limit is 12 keys and our current array looks like [2, 2, 2, 2, 2, x]
                //      sum is 10 and will only accept a new value of 2, a value of 3 is not possible to append.
                if (rand + sum <= max)
                    notesList.Add(rand);
            }
            if (sum == max-1) {
                notesList = new List<int>();
            }
            if (sum == max) {
                searching = false;
                break;
            }
        }

        int[] keysArray = notesList.ToArray();
    
        return keysArray;
    }
}
