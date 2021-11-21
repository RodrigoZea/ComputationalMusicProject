using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelodyGenerator : MonoBehaviour
{
    private int[] noteDurations = new int[]{4, 8, 16};
    public List<MelodyKey> generateMelodyFromCompass(Compass compass){
        List<MelodyKey> melodyKeys = new List<MelodyKey>();
        int[] melodyDurations = Utilities.calculateRandomPush(noteDurations, compass.duration);

        foreach (int keyDuration in melodyDurations) {
            List<Key> chordKeys = compass.chordToPlay.chordKeys;
            
            int keyPick = Random.Range(0, chordKeys.Count);
            Key randomKey = chordKeys[keyPick];
            MelodyKey melodyKey = new MelodyKey(randomKey, keyDuration);
            melodyKeys.Add(melodyKey);
        }
        return melodyKeys;
    }

}
