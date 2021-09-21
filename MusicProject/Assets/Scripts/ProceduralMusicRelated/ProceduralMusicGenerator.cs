using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class ProceduralMusicGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    private int[] key;
    private int[] filler;
    private int[] metric;
    public GameObject player;
     // -----------------------------
    public Toggle generateSeedOption;
    public InputField seed;
   
    public Text metricText;
    public Text keyText;
    public InputField bpmInputField;
    private float bpm;
    public GameObject progressionGenerator;

    public void StartRhythm() {
        metric = generateRhythm(generateSeedOption.isOn);
        int[] generatedKey = generateKey(metric);
        bpm = (float.Parse(bpmInputField.text));

        // Set max
        if (bpm > 300) bpm = 300;

        // Set default
        if (bpm <= 0) bpm = 100;

        // --------------------------------------------------

        key = keyArrayFilled(generatedKey);
        filler = generateFiller(generatedKey, metric);

        bpmInputField.enabled = false;
        generateSeedOption.enabled = false;
        seed.enabled = false;

        progressionGenerator.GetComponent<ProgressionGenerator>().enabled = true;
        progressionGenerator.GetComponent<ProgressionGenerator>().setMetric(metric);

        player.GetComponent<PlayerScript>().enabled = true;
        player.GetComponent<PlayerScript>().setIsEnabled(true);
        player.GetComponent<PlayerScript>().StartPlayer();
    }

    public void StopRhythm(){
        player.GetComponent<PlayerScript>().ResetPlayer();
        player.GetComponent<PlayerScript>().enabled = false; 

        bpmInputField.enabled = true;
        generateSeedOption.enabled = true;
        seed.enabled = true;
    }

    private int[] generateRhythm(bool generateSeed) {
        int initialSeed;
        List<int[]> metrics = new List<int[]>(){
            new[] {3, 4},
            new[] {4, 4}
        };

        // Seed setting
        if (generateSeed) {
            initialSeed = System.DateTime.Now.Second;
        } else {
            initialSeed = int.Parse(seed.text);
        }

        Random.InitState(initialSeed);

        // Pick a random metric
        int metricSelection = Random.Range(0, metrics.Count);
        int[] selectedMetric = metrics[metricSelection];

        metricText.text = selectedMetric[0].ToString() + "/" + selectedMetric[1].ToString();


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
        int [] keysArray = Utilities.calculateRandomPush(values, newSubdivisionCount);

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

        keyText.text = string.Join(",", keyArray);

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

    public float getBPM() {
        return bpm;
    }
}
