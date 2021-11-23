using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormGenerator : MonoBehaviour
{
    private Dictionary<string, List<Compass>> formCompassDictionary = new Dictionary<string, List<Compass>>();
    private Dictionary<string, List<MelodyKey>> formKeysDictionary = new Dictionary<string, List<MelodyKey>>();
    public GameObject player;
    public ProgressionGenerator progressionGenerator;
    public GameObject pianoPlayer;
    public MelodyGenerator melodyGenerator;

    List<Compass> formedCompassList = new List<Compass>();
    List<MelodyKey> formedKeyList = new List<MelodyKey>();


    // Start is called before the first frame update
    void Start()
    {
        getForm();
        pianoPlayer.GetComponent<PianoPlayer>().setPianoInfo(formedCompassList);
        pianoPlayer.GetComponent<PianoPlayer>().setMelodyInfo(formedKeyList);
        player.GetComponent<PlayerScript>().enabled = true;
        player.GetComponent<PlayerScript>().setIsEnabled(true);
        player.GetComponent<PlayerScript>().StartPlayer();
    }

    private void getForm() {
        // Define forms
        // AABB, ABAB, ABBA, AAAB, ABCD, ABCA, ABCB, ABCC
        List<string[]> forms = new List<string[]>(){
            new[] {"A", "A", "B", "B"},
            new[] {"A", "B", "A", "B"},
            new[] {"A", "B", "B", "A"},
            new[] {"A", "A", "A", "B"},
            new[] {"A", "B", "B", "C"},           
            new[] {"A", "B", "C", "D"},
            new[] {"A", "B", "C", "A"},
            new[] {"A", "B", "C", "B"},
            new[] {"A", "B", "C", "C"}
        };

        int formSelection = Random.Range(0, forms.Count);
        string[] selectedForm = forms[formSelection];

        foreach(string letter in selectedForm) {
            if (formCompassDictionary.ContainsKey(letter) && formKeysDictionary.ContainsKey(letter)) {
                // Get compass list
                List<Compass> compassList = formCompassDictionary[letter];
                formedCompassList.AddRange(compassList);
                // Get keys list
                List<MelodyKey> melodyList = formKeysDictionary[letter];
                formedKeyList.AddRange(melodyList);
            } else {
                // Generate new form
                List<Compass> compassList = progressionGenerator.calculateCompassDistribution();
                List<MelodyKey> formMelodyKeys = new List<MelodyKey>();

                foreach(Compass compass in compassList) {
                    List<MelodyKey> compassMelodyKeys = melodyGenerator.generateMelodyFromCompass(compass);
                    formMelodyKeys.AddRange(compassMelodyKeys);
                }
                formCompassDictionary[letter] = compassList;
                formKeysDictionary[letter] = formMelodyKeys;
            }
        }
    }
}
