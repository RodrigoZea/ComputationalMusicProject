using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Form : MonoBehaviour
{
    public string formIdentifier;
    public List<Compass> compasses;
    public List<MelodyKey> keys;
    public Form(string formIdentifier, List<Compass> compasses, List<MelodyKey> keys) {
        this.formIdentifier = formIdentifier;
        this.compasses = compasses;
        this.keys = keys;
    }
}
