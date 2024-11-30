using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public void PlayAgain()
        => SceneManager.Instance.LoadScene((int)Scenes.GAME_INTRO);
}
