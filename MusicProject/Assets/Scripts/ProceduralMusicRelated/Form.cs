using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Form : MonoBehaviour
{
    public string formIdentifier;
    public List<Compass> compasses;
    public List<Key> keys;
    public Form(string formIdentifier, List<Compass> compasses, List<Key> keys) {
        this.formIdentifier = formIdentifier;
        this.compasses = compasses;
        this.keys = keys;
    }
}
