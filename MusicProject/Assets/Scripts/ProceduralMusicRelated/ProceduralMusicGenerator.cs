using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProceduralMusicGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public int cantidadSubdivision;
    public int subdivisionBase;
    public bool generateSeed;
    public int seedToUse;

    void Start()
    {
        int[] metric = generateRhythm(0);
        int[] generatedKey = generateKey(metric);
        //generateFiller(generatedKey, metric);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int[] generateRhythm(int seed) {
        int initialSeed;
        List<int[]> metrics = new List<int[]>();

        // Seed setting
        if (generateSeed) {
            initialSeed = System.DateTime.Now.Second;
        } else {
            initialSeed = seed;
        }

        Random.InitState(initialSeed);

        int[] threeFourths = {3, 4};
        int[] fourthFourths = {4, 4};

        metrics.Add(threeFourths);
        metrics.Add(fourthFourths);

        // Pick a random metric
        int metricSelection = Random.Range(0, metrics.Count);
        int[] selectedMetric = metrics[metricSelection];

        return selectedMetric;
    }

    int[] generateKey(int[] selectedMetric) {
        // Can be 3 or 4
        int cantidadSubdivision  = selectedMetric[0];
        // Is always 4.
        int subdivisionBase = selectedMetric[1];

        // For the beat array
        // i.e. stores a randomly generated [0,1,1,0,1,0,0,1,0,0,1,0] (int[12]) on a 3/4 Metric
        // Possible values at the moment are 12 or 16.
        int keysSize = cantidadSubdivision*4;
        int[] keysArray = new int[keysSize];

        // 1, 2, 4
        // 1: Semicorcheas
        // 2: Corcheas
        // 4: Negras
        int[] possibleSubdivisions = {1, 2, 4};
        // Pick a random subdivision
        int randomSubdivision = Random.Range(0, possibleSubdivisions.Length);
        // Can be either 3, 6, 12 / 4, 8, 16 depending on the what the random picks.
        int newSubdivisionCount = cantidadSubdivision*possibleSubdivisions[randomSubdivision];

        // ----------------------------------------------------------------------------
        // Possible values 
        int[] values = {2,3};
   
        int currentKeyPos = 0;
        Debug.Log("Array Size: " + keysSize);
        Debug.Log("Limit: " + newSubdivisionCount);
        // Algorithm: Pick random numbers and append, validate on each append that the sum of the values is not bigger than the size allowed.
        while(keysArray.Sum() < newSubdivisionCount) {
            // Pick random number. Either 2 or 3.
            int rand = values[Random.Range(0, values.Length)];
            // If value to append is not bigger than the size allowed, add it to array.
            // i.e. limit is 12 keys and our current array looks like [2, 2, 2, 2, 2, x]
            //      sum is 10 and will only accept a new value of 2, a value of 3 is not possible to append.
            if (rand + keysArray.Sum() <= newSubdivisionCount) {  
                keysArray[currentKeyPos] = rand;
                // On successful append...
                currentKeyPos += 1;
            } 
        }

        Debug.Log("Clave test: " + string.Join(",", keysArray));

        return keysArray;
    }

    void generateFiller(int[] keys, int[] metric) {
        List<int> result = new List<int>();
        
        foreach(int note in keys) {
            if (note == 2) result.AddRange(new List<int>{0, 1, 1});
            else if (note == 3) result.AddRange(new List<int>{0, 1});
        }

        int[] fillingResult = result.ToArray();
        Debug.Log("Filling test: " + string.Join(",", fillingResult));
    }
}
