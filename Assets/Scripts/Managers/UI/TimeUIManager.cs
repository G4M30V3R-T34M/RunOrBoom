using TMPro;
using UnityEngine;

public class TimeUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;
    public void UpdateTimeDisplay(float remainingTime)
    {
        int seconds = Mathf.RoundToInt(remainingTime % 60);
        int minutes = (int)remainingTime / 60;
        timeText.SetText(
            minutes.ToString("00") + ":" + seconds.ToString("00")
        );
    }
}
