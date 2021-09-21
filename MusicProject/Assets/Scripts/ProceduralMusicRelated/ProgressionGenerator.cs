using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ProgressionGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    private int compasses = 8;
    private int[] generatorMetric;
    private int blackKeysPerCompass;
    private int[] possibleSubDivisions;
    private int[] possibleFullDivisions;
    private PianoScript piano;
    void Start()
    {
        calculateCompassDistribution();
    }

    private void calculateCompassDistribution() {
        possibleFullDivisions = new[]{1, 2, 4};
        if (generatorMetric != null) {
            if (generatorMetric[0] == 4) {
                possibleSubDivisions = new[]{2, 4};
                blackKeysPerCompass = 4;
            } 
            else if (generatorMetric[0] == 3) {
                possibleSubDivisions = new[]{3, 3};
                blackKeysPerCompass = 3;
            }
        }

        // Pick random piano note
        int randomPianoKey = Random.Range(0, 13);
        piano = new PianoScript(randomPianoKey);

        int maxBlackKeys = compasses*4;


        int[] keysArray = Utilities.calculateRandomPush(possibleFullDivisions, compasses);
        int[] subdividedCompass = subdivideCompass(keysArray);

        Debug.Log("Compass subdivided: " + string.Join(",", subdividedCompass));

        // We now do Tonal functions
        calculateTonalFunction(subdividedCompass);

    }

    private int[] subdivideCompass(int[] compassArray) {
        List<int> resultList = new List<int>();

        for(int i=0; i < compassArray.Length; i++) {
            int num = compassArray[i];

            // We only care about splittable compasses, 1 is unsplittable
            if (num > 1) {
                resultList.Add(num*blackKeysPerCompass);
                continue;
            }

            // "else", it's going to be a number we can actually split
            int chance = Random.Range(0, 2);

            // We didnt split
            if (chance == 0) {
                resultList.Add(num*blackKeysPerCompass);
                continue;
            }

            // We split
            int chanceSubdivision = Random.Range(0, 2);
            int pickedSubdivision = possibleSubDivisions[chanceSubdivision];

            resultList.AddRange(
                Enumerable.Repeat(1, pickedSubdivision)
            );
        }

        int[] result = resultList.ToArray();
        return result;
    }

    private void calculateTonalFunction(int[] compassArray) {
        Chord previousChord = null;
        for (int i = 0; i < compassArray.Length; i++) {

        }
    }

    private void chooseGradedChords() {

    }

    public void setMetric(int[] metric) {
        generatorMetric = metric;
    }

}
