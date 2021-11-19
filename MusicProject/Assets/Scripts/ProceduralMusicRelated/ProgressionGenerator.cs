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
    public GameObject formGenerator;
    public GameObject player;
    public GameObject pianoPlayer;

    private void OnEnable() {
        initPiano();
        formGenerator.GetComponent<FormGenerator>().enabled = true;
        //List<Compass> compassTonal = calculateCompassDistribution();
        
        /*
        player.GetComponent<PlayerScript>().enabled = true;
        player.GetComponent<PlayerScript>().setIsEnabled(true);
        player.GetComponent<PlayerScript>().StartPlayer();*/
    }

    private void initPiano() {
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
        } else {
            Debug.Log(generatorMetric);
        }

        // Pick random piano note
        int randomPianoKey = Random.Range(0, 12);
        piano = new PianoScript(randomPianoKey);

        int maxBlackKeys = compasses*4;
    }

    public List<Compass> calculateCompassDistribution() {
        int[] keysArray = Utilities.calculateRandomPush(possibleFullDivisions, compasses);
        int[] subdividedCompass = subdivideCompass(keysArray);

        Debug.Log("Compass subdivided: " + string.Join(",", subdividedCompass));

        // We now do Tonal functions
        List<Compass> compassTonal = calculateTonalFunction(subdividedCompass);

        foreach(Compass compass in compassTonal) {
            Debug.Log("Compass duration: " + compass.duration);
            Debug.Log("Compass type: " + compass.compassType);
            Debug.Log("Chord a planchar: " + compass.chordToPlay.chordMainKey);
            Debug.Log("Compass keys: " + compass.chordToPlay.getChordKeys());
        }

        return compassTonal;
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

    private List<Compass> calculateTonalFunction(int[] compassArray) {
        Compass previousCompass = null;
        List<Compass> compassList = new List<Compass>();

        for (int i = 0; i < compassArray.Length; i++) {
            // Start with a strong one
            CompassType compassType = CompassType.Fuerte;

            // Check previous one
            if (previousCompass != null) {
                if (compassArray[i] % previousCompass.duration == 0 && previousCompass.compassType == CompassType.Fuerte) {
                    compassType = CompassType.Debil;
                }
            }

            Chord chord = new Chord();
            // Select a random one
            if (compassType == CompassType.Fuerte) {
                chord = piano.getRandomStrongChord();
            } else if (compassType == CompassType.Debil) {
                chord = piano.getRandomWeakChord();
            }

            // Check double weak
            if (previousCompass != null) {
                if (previousCompass.compassType == CompassType.Debil && previousCompass.chordToPlay.chordGrade == ChordGrade.Subdominante) {
                    chord = piano.getRandomDomChord();
                }
            }

            Compass newCompass = new Compass(chord, compassArray[i]*4, compassType);
            compassList.Add(newCompass);
            previousCompass = newCompass;
        }
        return compassList;
    }

    public void setMetric(int[] metric) {
        generatorMetric = metric;
    }

}
