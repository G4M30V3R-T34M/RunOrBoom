using FeTo.SOArchitecture;
using UnityEngine;

public class CodeFragment : PickableItem
{
    [SerializeField] GameEvent codePickedUp;
    public override void PickItem()
    {
        codePickedUp.Raise();
        gameObject.SetActive(false);
    }
}
