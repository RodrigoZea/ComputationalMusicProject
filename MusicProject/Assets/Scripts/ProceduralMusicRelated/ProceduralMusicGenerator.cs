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
    private int[] key;
    private int[] filler;
    private int[] metric;
    public GameObject player;

    void Start()
    {
        metric = generateRhythm(0);
        int[] generatedKey = generateKey(metric);

        // --------------------------------------------------

        key = keyArrayFilled(generatedKey);
        filler = generateFiller(generatedKey, metric);

        player.GetComponent<PlayerScript>().enabled = true;
        player.GetComponent<PlayerScript>().setIsEnabled(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private int[] generateRhythm(int seed) {
        int initialSeed;
        List<int[]> metrics = new List<int[]>(){
            new[] {3, 4},
            new[] {4, 4}
        };

        // Seed setting
        if (generateSeed) {
            initialSeed = System.DateTime.Now.Second;
        } else {
            initialSeed = seed;
        }

        Random.InitState(initialSeed);

        // Pick a random metric
        int metricSelection = Random.Range(0, metrics.Count);
        int[] selectedMetric = metrics[metricSelection];

        return selectedMetric;
    }

    private int[] generateKey(int[] selectedMetric) {
        // Can be 3 or 4
        int cantidadSubdivision  = selectedMetric[0];
        // Is always 4.
        int subdivisionBase = selectedMetric[1];

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

        // Possible values for key
        int[] values = {2,3};
        //Debug.Log("Limit: " + newSubdivisionCount);
        List<int> notesList = new List<int>();
        int sum = 0;

        bool searching = true;
        // Algorithm: Pick random numbers and append, validate on each append that the sum of the values is not bigger than the size allowed.
        while(searching){
            sum = notesList.Sum();
            if (sum < newSubdivisionCount)
            {
                // Pick random number. Either 2 or 3.
                int rand = values[Random.Range(0, values.Length)];
                // If value to append is not bigger than the size allowed, add it to array.
                // i.e. limit is 12 keys and our current array looks like [2, 2, 2, 2, 2, x]
                //      sum is 10 and will only accept a new value of 2, a value of 3 is not possible to append.
                if (rand + sum <= newSubdivisionCount)
                    notesList.Add(rand);
            }
            if (sum == newSubdivisionCount-1) {
                notesList = new List<int>();
            }
            if (sum == newSubdivisionCount) {
                searching = false;
                break;
            }
        }

        int[] keysArray = notesList.ToArray();
    
        return keysArray;
    }

    private int[] generateFiller(int[] keys, int[] metric) {
        List<int> resultFiller = new List<int>();
        
        // According to pptx easiest way to create filler is this way.
        int[] twoKeyFiller = {0, 1};
        int[] threeKeyFiller = {0, 1, 1};
        
        for(int i=0; i < keys.Length; i++) {
            if (keys[i] == 3) resultFiller.AddRange(threeKeyFiller);
            else if (keys[i] == 2) resultFiller.AddRange(twoKeyFiller);
        }

        int[] fillingArray = resultFiller.ToArray();

        Debug.Log("Filling: " + string.Join(",", fillingArray));
        Debug.Log("Filling size: " + fillingArray.Length);

        return fillingArray;
    }

    private int[] keyArrayFilled(int[] keys) {
        List<int> keysToPlay = new List<int>();

        // Generating keys array
        int[] twoKey = {1, 0};
        int[] threeKey = {1, 0, 0};
        
        for(int i=0; i < keys.Length; i++) {
            if (keys[i] == 3) keysToPlay.AddRange(threeKey);
            else if (keys[i] == 2) keysToPlay.AddRange(twoKey);
        }
        
        int[] keyArray = keysToPlay.ToArray();
        Debug.Log("Key: " + string.Join(",", keyArray));
        Debug.Log("key size: " + keyArray.Length);

        return keyArray;
    }

    public int[] getKey(){
        return key;
    }

    public int[] getFiller() {
        return filler;
    }

    public int[] getMetric() {
        return metric;
    }
}
