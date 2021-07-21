using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMetronome : MonoBehaviour
{
    // AudioClip 1: First metronome indicator
    // AudioClip 2: Rest of indicators
    public List<AudioClip> audioClips; 
   private AudioSource audioSource; 
   public InputField bpm;
   public Slider metric; 
   public Toggle subdivisionMode;

   private int counter;
   private float interval;
   private float beatsPerBar;

   private bool isEnabled;

    
    // Start is called before the first frame update
    void StartMetronome()
    {
        audioSource = GetComponent<AudioSource>();
        beatsPerBar = metric.value;
        interval = 60.0f/(float.Parse(bpm.text));
        Debug.Log("Interval is: " + interval);

        Debug.Log("subdivision:" + subdivisionMode.isOn);


        if (subdivisionMode.isOn) {
            StartCoroutine("metronomeSubdivided");
        } else {
            StartCoroutine("metronome");
        }
    }

    IEnumerator metronome() {
        counter = 0;
        while (isEnabled) {
            counter++;
            if (counter % beatsPerBar == 1) {
                audioSource.PlayOneShot(audioClips[0], 0.7F);
                Debug.Log("start beat");
            } else {
                audioSource.PlayOneShot(audioClips[1], 0.7F);
                Debug.Log("other beat");
            }

            yield return new WaitForSecondsRealtime(interval);
        }
    }

    IEnumerator metronomeSubdivided() {
        counter = 0;
        beatsPerBar = beatsPerBar*2;
        while (isEnabled) {
            counter++;
            if (counter % beatsPerBar == 1) {
                audioSource.PlayOneShot(audioClips[0], 0.7F);
                Debug.Log("Beat inicial");
            } else {
                if (counter % 2 == 0){
                    audioSource.PlayOneShot(audioClips[2], 0.7F);
                    Debug.Log("Beat subdividido");
                } else {
                    audioSource.PlayOneShot(audioClips[1], 0.7F);
                    Debug.Log("Beat entero");
                }
            }
            
            yield return new WaitForSecondsRealtime(interval/2);
        }
    }

    public void setEnabled() {
        isEnabled = true;
        StartMetronome();
    }
    public void setDisabled() {
        isEnabled = false;
    }
}
