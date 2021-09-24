using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public string keyName;
    public bool accent;
    public float frequency;
    public int keyPos;
    public Key(string keyName, int keyPos) {
        this.keyName = keyName;
        this.keyPos = keyPos;
        this.frequency = Mathf.Pow(2, ((float)this.keyPos)/12.0f);
    }
}
