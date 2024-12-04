using FeTo.SOArchitecture;
using UnityEngine;

public class GameWinManager : MonoBehaviour
{
    [SerializeField] GameSO gameSettings;
    [SerializeField] GameEvent playerWinEvent;

    private int pickedCodes;

    private void Start() => pickedCodes = 0;

    public void CodePickedUp()
    {
        pickedCodes += 1;
        CheckPlayerWin();
    }

    private void CheckPlayerWin()
    {
        if (WinCondition())
        {
            playerWinEvent.Raise();
        }
    }

    private bool WinCondition() => pickedCodes == gameSettings.totalCodes;
}
