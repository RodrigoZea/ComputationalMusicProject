using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoPlayer : MonoBehaviour
{
    PianoScript pianoInfo;
    List<Compass> compasses;
    List<MelodyKey> melodyKeys;

    public void setPianoInfo(List<Compass> compassTonal) {
        compasses = compassTonal;
    }

    public void setMelodyInfo(List<MelodyKey> melodyKeysArranged) {
        melodyKeys = melodyKeysArranged;
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

    public MelodyKey GetMelodyKey(int index) {
        return melodyKeys[index];
    }

    public bool checkKeyCounter(int index) {
        bool passed = false;

        if (index >= melodyKeys.Count) {
            passed = true;
        }

        return passed;
    }


}
