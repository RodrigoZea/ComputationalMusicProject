using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoPlayer : MonoBehaviour
{
    PianoScript pianoInfo;
    List<Compass> compasses;

    public void setPianoInfo(PianoScript pianoScript, List<Compass> compassTonal) {
        pianoInfo = pianoScript;
        compasses = compassTonal;
    }

    public Compass getCompass(int index) {
        return compasses[index];
    }

    public bool checkCompassCounter(int index) {
        bool passed = false;

        if (index >= compasses.Count) {
            passed = true;
        }

        return passed;
    }
}
