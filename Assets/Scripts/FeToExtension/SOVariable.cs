using FeTo.SOArchitecture;
using UnityEngine;

[CreateAssetMenu(fileName = "SOVariable", menuName = "FeTo/SO_Architecture/SOVariable", order = 22)]
public class SOVariable : ScriptableVariable<ScriptableObject>
{
    public override void ApplyChange(ScriptableObject value) => throw new System.NotImplementedException();
    public override void ApplyChange(ScriptableVariable<ScriptableObject> variable) => throw new System.NotImplementedException();
}
