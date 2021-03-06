using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MusicGenerator;
    public GameObject Drums;
    public GameObject Piano;

    // --------------------------------
    private ProceduralMusicGenerator generator;
    private DrumsScript drumsScript;
    private AudioSource playerAudioSource;
    private AudioSource drumAudioSource;

    // --------------------------------
    private PianoScript pianoScript; 
    public PianoPlayer pianoPlayer;
    public List<AudioSource> pianoKeysAudioSource;
    public AudioSource melodyAudioSource;

    // --------------------------------
    public int[] metric;
    public float bpm;
    private bool isEnabled;
    private int counter;
    private int currentCompass;
    private int currentKey;
    private int compassSemiCounter;
    private int melodySemiCounter;
    private float interval;
    public List<AudioClip> audioClips;
    private float semicorcheasPerMinute;
    private int subdivisionSemicorcheas;
    private int[] metricToPlay;
    private int drumArrayLength;
    public GameObject progressionGenerator;
    public GameObject formGenerator;

    // --------------------------------

    public void StartPlayer()
    {
        generator = MusicGenerator.GetComponent<ProceduralMusicGenerator>();
        drumsScript = Drums.GetComponent<DrumsScript>();

        playerAudioSource = GetComponent<AudioSource>();
        drumAudioSource = Drums.GetComponent<AudioSource>();

        // -------------------------------------
        bpm = generator.getBPM();
        interval = 60.0f/bpm;
        semicorcheasPerMinute = interval/4f;
        
        metric = generator.getMetric();
        subdivisionSemicorcheas = metric[0]*4;

        metricToPlay = calculateMetric();

        // -------------------------------------        

        SetupInstruments();

        StartCoroutine(StartBeat());
        StartCoroutine(StartPiano());
        StartCoroutine(StartMelody());
    }

    public void ResetPlayer() {
        counter = 0;
        currentCompass = 0;
        currentKey = 0;
        compassSemiCounter = 0;

        isEnabled = false;
        progressionGenerator.GetComponent<ProgressionGenerator>().enabled = false;
        formGenerator.GetComponent<FormGenerator>().enabled = false;
        drumsScript.enabled = false;
        pianoPlayer.enabled = false;
    }

    private void SetupInstruments(){
        drumsScript.setKey(generator.getKey());
        drumsScript.setFiller(generator.getFiller());
        drumArrayLength = drumsScript.drumArrayLength();

        drumsScript.enabled = true;
        pianoPlayer.enabled = true;
    }

    private IEnumerator StartBeat() {
        counter = 0;
        while (isEnabled) {
            int currentMetricKey = metricToPlay[counter%metricToPlay.Length];
            int currentKeyNote = drumsScript.getKeyNote(counter%drumArrayLength);
            int currentFillerNote = drumsScript.getFillerNote(counter%drumArrayLength);

            if (currentMetricKey == 1) {
                playerAudioSource.PlayOneShot(drumsScript.audioClips[2], 0.7F);
            } 
            if (currentKeyNote == 1) {
                drumAudioSource.PlayOneShot(drumsScript.audioClips[0], 0.7F);
                //Debug.Log("Played Key");
            }
            if (currentFillerNote == 1) {
                drumAudioSource.PlayOneShot(drumsScript.audioClips[1], 0.7F);
                //Debug.Log("Played Filler");
            }

            counter++;
            yield return new WaitForSecondsRealtime(semicorcheasPerMinute);
        }
    }

    private IEnumerator StartPiano() {
        currentCompass = 0;
        compassSemiCounter = 0;

        while (isEnabled) {
            Compass currentCompassObj = pianoPlayer.getCompass(currentCompass);

            //compassSemiCounter++;

            // Play chord
             playChord(
                currentCompassObj.chordToPlay.chordKeys[0], 
                currentCompassObj.chordToPlay.chordKeys[1], 
                currentCompassObj.chordToPlay.chordKeys[2]
            );

            /*
            if (compassSemiCounter % metric[0] == 0) {
                // Play chord
                playChord(
                    currentCompassObj.chordToPlay.chordKeys[0], 
                    currentCompassObj.chordToPlay.chordKeys[1], 
                    currentCompassObj.chordToPlay.chordKeys[2]
                );
            }*/
            currentCompass++;

            /*if (compassSemiCounter == currentCompassObj.duration) {
                currentCompass++;
                compassSemiCounter = 0;
            }*/

            if (pianoPlayer.checkCompassCounter(currentCompass)) {
                currentCompass = 0;
            }

            yield return new WaitForSecondsRealtime(currentCompassObj.duration);
        }
    }

    private IEnumerator StartMelody() {
        currentKey = 0;
        melodySemiCounter = 0;

        while (isEnabled) {
            MelodyKey currentMelodyKey = pianoPlayer.GetMelodyKey(currentKey);
            currentKey++;

            melodyAudioSource.pitch = currentMelodyKey.key.frequency;
            melodyAudioSource.Play();

            //melodySemiCounter++;
            //currentKey++;
            /*if (melodySemiCounter > currentMelodyKey.duration) {
                currentKey++;
                melodySemiCounter=0;
            }*/

            if (pianoPlayer.checkKeyCounter(currentKey)) {
                currentKey = 0;
            }

            yield return new WaitForSecondsRealtime(currentMelodyKey.duration);
        }
    }

    // -------------------------------------------------------
    
    private void playChord(Key key1, Key key2, Key key3) {
        pianoKeysAudioSource[0].pitch = key1.frequency;
        pianoKeysAudioSource[1].pitch = key2.frequency;
        pianoKeysAudioSource[2].pitch = key3.frequency;


        pianoKeysAudioSource[0].Play();
        pianoKeysAudioSource[1].Play();
        pianoKeysAudioSource[2].Play();
    }

    public void setIsEnabled(bool enabled) {
        isEnabled = enabled;
    }

    private int[] calculateMetric() {
        // 3: [1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0]
        // 4: [1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0]
        List<int> metricToPlay = new List<int>();

        for(int i=0; i < metric[0]; i++) {
            metricToPlay.AddRange(new int[] {1, 0, 0, 0});
        }

        int[] metricArray = metricToPlay.ToArray();

        return metricArray;
    }
}
