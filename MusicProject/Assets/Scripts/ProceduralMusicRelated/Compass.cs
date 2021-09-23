using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CompassType {
    Fuerte, Debil, None
}
public class Compass : MonoBehaviour
{
    public Chord chordToPlay;
    public int duration;
    public CompassType compassType;

    public Compass(Chord chordToPlay, int duration, CompassType compassType) {
        this.chordToPlay = chordToPlay;
        this.duration = duration;
        this.compassType = compassType;
    }
}
