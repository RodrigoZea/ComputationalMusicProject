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
    private AudioSource audioSource;

    // --------------------------------
    public int[] metric;
    public int bpm;
    private bool isEnabled;
    private int counter;
    private float interval;
    public List<AudioClip> audioClips;
    void Start()
    {
        generator = MusicGenerator.GetComponent<ProceduralMusicGenerator>();
        drumsScript = Drums.GetComponent<DrumsScript>();
        audioSource = GetComponent<AudioSource>();
        bpm = 100;
        interval = 60.0f/bpm;
        
        PlayDrums();

        StartCoroutine(StartBeat());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayDrums(){
        drumsScript.enabled = true;
    }

    private IEnumerator StartBeat() {
        metric = generator.getMetric();
        int subdivisionSemicorcheas = metric[0]*4;
        float spm = interval/4f;

        Debug.Log("SPM: " + spm);

        counter = 0;
        while (isEnabled) {
            counter++;
            if (counter % subdivisionSemicorcheas == 1) {
                audioSource.PlayOneShot(audioClips[0], 0.7F);
                Debug.Log("A");
            } else {
                audioSource.PlayOneShot(audioClips[1], 0.7F);
                Debug.Log("B");
            }
            //Debug.Log("Counter: " + counter);

            yield return new WaitForSecondsRealtime(spm);
        }

    }

    // -------------------------------------------------------

    public void setIsEnabled(bool enabled) {
        isEnabled = enabled;
    }
}
