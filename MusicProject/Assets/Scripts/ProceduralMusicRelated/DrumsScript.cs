using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumsScript : MonoBehaviour
{
    // 0: Key
    // 1: Filling
    // 2: Metric

    public List<AudioClip> audioClips; 
    private int[] drumKey;
    private int[] drumFiller;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getKeyNote(int pos) {
        int noteValue = drumKey[pos];
        return noteValue;
    }

    public int getFillerNote(int pos) {
        int noteValue = drumFiller[pos];
        return noteValue;
    }

    public void setKey(int[] generatedKey) {
        drumKey = generatedKey;
    }

    public void setFiller(int[] generatedFiller) {
        drumFiller = generatedFiller;
    }
}
