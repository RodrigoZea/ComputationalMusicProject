using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelodyKey : MonoBehaviour
{
    Key key;
    int duration;

    public MelodyKey(Key key, int duration) {
        this.key = key;
        this.duration = duration;
    }
}
