using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoScript : MonoBehaviour
{
    public string[] notes = {
        "Do",
        "Do#", 
        "Re", 
        "Re#", 
        "Mi",
        "Fa",
        "Fa#", 
        "Sol", 
        "Sol#", 
        "La",
        "La#", 
        "Si" 
    };
    int noteAmount = 12;

    int rootNoteIndex;
    string[] majorKey;
    List<Chord> chords;
    public List<Chord> strongChords;
    public List<Chord> weakChords;

    public PianoScript(int rootNoteIndex) {
        this.rootNoteIndex = rootNoteIndex;
        this.majorKey = generateMajorKey(rootNoteIndex);
        this.chords = generateChordsToUse(this.majorKey);

        this.strongChords = getStrongChords(this.chords);
        this.weakChords = getWeakChords(this.chords);
    }

    private string[] generateMajorKey(int rootNoteIndex) {
        List<string> majorKey = new List<string>();
        int[] positions = {rootNoteIndex+2, rootNoteIndex+4, rootNoteIndex+5, rootNoteIndex+7, rootNoteIndex+9, rootNoteIndex+11};

        majorKey.Add(notes[rootNoteIndex]);

        foreach (var pos in positions) {
            majorKey.Add(notes[pos%noteAmount]);
        }

        string[] majorKeyArray = majorKey.ToArray();

        return majorKeyArray;
    }

    private List<Chord> generateChordsToUse(string[] majorKey) {
        List<Chord> chordList = new List<Chord>();
        for(int i=0; i < majorKey.Length; i++) {
            int[] chordPos = {i, i+2, i+4};

            List<Key> chordKeys = new List<Key>();

            foreach (var pos in chordPos) {
                Key newKey = new Key(
                    majorKey[pos%7],
                    pos%7
                );
                chordKeys.Add(newKey);
            }

            Chord newChord = new Chord(
                chordKeys,
                i
            );
            chordList.Add(newChord);
        }

        return chordList;
    }

    private List<Chord> getStrongChords(List<Chord> chords) {
        List<Chord> strongChordList = new List<Chord>();
        foreach (Chord chord in chords) {
            if (chord.chordGrade == ChordGrade.Tonica || chord.chordGrade == ChordGrade.Subdominante) {
                strongChordList.Add(chord);
            }
        }

        return strongChordList;
    }

    private List<Chord> getWeakChords(List<Chord> chords) {
        List<Chord> weakChordList = new List<Chord>();
        foreach (Chord chord in chords) {
            if (chord.chordGrade == ChordGrade.Dominante || chord.chordGrade == ChordGrade.Subdominante) {
                weakChordList.Add(chord);
            }
        }

        return weakChordList;
    }

    public Chord getRandomStrongChord() {
        int choice = Random.Range(0, strongChords.Count);
        Chord randomStrong = strongChords[choice];

        return randomStrong;
    }

    public Chord getRandomWeakChord() {
        int choice = Random.Range(0, weakChords.Count);
        Chord randomWeak = weakChords[choice];

        return randomWeak;
    }
}
