using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MusicGenerator;
    public GameObject Drums;

    // --------------------------------
    private ProceduralMusicGenerator generator;
    private DrumsScript drumsScript;
    private AudioSource playerAudioSource;
    private AudioSource drumAudioSource;

    // --------------------------------
    public int[] metric;
    public int bpm;
    private bool isEnabled;
    private int counter;
    private float interval;
    public List<AudioClip> audioClips;
    private float semicorcheasPerMinute;
    private int subdivisionSemicorcheas;
    private int[] metricToPlay;
    void Start()
    {
        generator = MusicGenerator.GetComponent<ProceduralMusicGenerator>();
        drumsScript = Drums.GetComponent<DrumsScript>();

        playerAudioSource = GetComponent<AudioSource>();
        drumAudioSource = Drums.GetComponent<AudioSource>();

        bpm = 100;
        interval = 60.0f/bpm;
        semicorcheasPerMinute = interval/4f;
        
        metric = generator.getMetric();
        subdivisionSemicorcheas = metric[0]*4;
        
        metricToPlay = calculateMetric();

        SetupDrums();
        StartCoroutine(StartBeat());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetupDrums(){
        drumsScript.setKey(generator.getKey());
        drumsScript.setFiller(generator.getFiller());

        drumsScript.enabled = true;
    }

    private IEnumerator StartBeat() {
        counter = 0;
        while (isEnabled) {
            counter++;

            int currentMetricKey = metricToPlay[counter%metricToPlay.Length];
            int currentKeyNote = drumsScript.getKeyNote(counter%metricToPlay.Length);
            int currentFillerNote = drumsScript.getFillerNote(counter%metricToPlay.Length);

            if (currentMetricKey == 1) {
                playerAudioSource.PlayOneShot(audioClips[0], 0.7F);
                Debug.Log("Playing metric key.");
            } 

            if (currentKeyNote == 1) {
                drumAudioSource.PlayOneShot(drumsScript.audioClips[0], 0.7F);
                Debug.Log("Playing drum key.");
            }

            if (currentFillerNote == 1) {
                drumAudioSource.PlayOneShot(drumsScript.audioClips[1], 0.7F);
                Debug.Log("Playing drum filler.");
            }

            yield return new WaitForSecondsRealtime(semicorcheasPerMinute);
        }
    }


    // -------------------------------------------------------

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
