using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogScriptable", menuName = "Scriptables/DialogScriptable", order = 6)]
public class DialogScriptable : ScriptableObject
{
    public DialogEntryScriptable[] dialog;

    [Range(0, 1)]
    public float waitTime;
}
