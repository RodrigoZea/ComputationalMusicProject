using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public string keyName;
    public bool accent;
    public float frequency;
    public int keyPos;
    public Key(string keyName, bool accent, int keyPos) {
        this.keyName = keyName;
        this.accent = accent;
        this.keyPos = keyPos;
        //this.frequency = frequency;
    }
}
