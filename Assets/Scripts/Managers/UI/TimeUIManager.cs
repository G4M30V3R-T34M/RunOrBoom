using TMPro;
using UnityEngine;

public class TimeUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;
    public void UpdateTimeDisplay(float remainingTime)
    {
        int seconds = Mathf.RoundToInt(remainingTime % 60);
        int hours = (int)remainingTime / 60;
        timeText.SetText(
            hours.ToString("00") + ":" + seconds.ToString("00")
        );
    }
}
