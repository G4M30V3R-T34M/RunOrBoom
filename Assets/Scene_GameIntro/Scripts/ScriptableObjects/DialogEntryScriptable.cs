using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogEntryScriptable", menuName = "Scriptables/DialogEntryScriptable", order = 5)]
public class DialogEntryScriptable : ScriptableObject
{
    public string speaker;
    [TextArea]
    public string content;
}
