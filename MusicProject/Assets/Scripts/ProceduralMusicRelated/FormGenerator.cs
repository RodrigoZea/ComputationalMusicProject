using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormGenerator : MonoBehaviour
{
    public Dictionary<string, List<Compass>> formDictionary;
    public GameObject player;
    public ProgressionGenerator progressionGenerator;
    public GameObject pianoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        List<Compass> formedCompassList = getForm();
        pianoPlayer.GetComponent<PianoPlayer>().setPianoInfo(formedCompassList);
        player.GetComponent<PlayerScript>().enabled = true;
        player.GetComponent<PlayerScript>().setIsEnabled(true);
        player.GetComponent<PlayerScript>().StartPlayer();
    }

    private List<Compass> getForm() {
    List<Compass> producedFormList = new List<Compass>();
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
            if (formDictionary.ContainsKey(letter)) {
                // Get the compass list
                List<Compass> value = formDictionary[letter];
                producedFormList.AddRange(value);
            } else {
                // Generate a new compass list
                List<Compass> compassList = progressionGenerator.calculateCompassDistribution();
                formDictionary[letter] = compassList;
                producedFormList.AddRange(compassList);
            }
        }

        return producedFormList;
    }
}
