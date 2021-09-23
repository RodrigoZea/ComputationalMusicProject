using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChordGrade {
    Tonica, Subdominante, Dominante, None
}

public enum ChordQuality {
    Mayor, Menor, Aumentado, Disminuido, None
}
public class Chord : MonoBehaviour
{
    public string chordMainKey;
    public List<Key> chordKeys;
    ChordQuality chordQuality;
    public ChordGrade chordGrade;
    public Chord() {}
    public Chord(List<Key> chordKeys, int chordIndex, string chordMainKey) {
        this.chordMainKey = chordMainKey;
        this.chordKeys = chordKeys;
        this.chordGrade = getGrade(chordIndex);
        this.chordQuality = getQuality(this.chordKeys);
    }

    // ---------------------------------------------

    private ChordQuality getQuality(List<Key> keys) {
        ChordQuality chordQuality = ChordQuality.None;
        int firstDistance = keys[0].keyPos - keys[1].keyPos;
        int secondDistance = keys[1].keyPos - keys[2].keyPos;

        if (firstDistance == 9) {
            firstDistance = Mathf.Abs(firstDistance-12);
        }
        if (secondDistance == 9) {
            secondDistance = Mathf.Abs(secondDistance-12);
        }

        firstDistance = Mathf.Abs(firstDistance);
        secondDistance = Mathf.Abs(secondDistance);

        chordQuality = calcQuality(firstDistance, secondDistance);
        return chordQuality;
    }

    private ChordQuality calcQuality(int firstDistance, int secondDistance) {
        ChordQuality chordQuality = ChordQuality.None;

        if (firstDistance == 4 && secondDistance == 3){
            chordQuality = ChordQuality.Mayor;
        }
        else if (firstDistance == 3 && secondDistance == 4){
            chordQuality = ChordQuality.Menor;
        }
        else if (firstDistance == 3 && secondDistance == 3){
            chordQuality = ChordQuality.Disminuido;
        }
        else if (firstDistance == 4 && secondDistance == 4){
            chordQuality = ChordQuality.Aumentado;
        }

        return chordQuality;
    }
    
    // ---------------------------------------------

    private ChordGrade getGrade(int index) {
        ChordGrade chordGrade = ChordGrade.None;
        if (index == 0 || index == 2 || index == 5) {
            chordGrade = ChordGrade.Tonica;
        } else if (index == 1 || index == 3) {
            chordGrade = ChordGrade.Subdominante;
        } else if (index == 4) {
            chordGrade = ChordGrade.Dominante;
        }

        return chordGrade;
    }

    public string getChordKeys() {
        return chordKeys[0].keyName + " | " + chordKeys[1].keyName + " | " + chordKeys[2].keyName;
    }

}
