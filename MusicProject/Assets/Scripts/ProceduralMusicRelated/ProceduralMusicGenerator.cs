using System.Collections;
using System.Collections.Generic;
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
        generateRhythm(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void generateRhythm(int seed) {
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

        generateKey(selectedMetric);
    }

    void generateKey(int[] selectedMetric) {
        int cantidadSubdivision  = selectedMetric[0];
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
        int selectedSubdivisionMultiplier = possibleSubdivisions[randomSubdivision];
        // Can be either 4, 8 or 16 depending on the what the random picks.
        int newBaseSub = subdivisionBase*selectedSubdivisionMultiplier;
    }

    void generateFiller() {

    }
}
