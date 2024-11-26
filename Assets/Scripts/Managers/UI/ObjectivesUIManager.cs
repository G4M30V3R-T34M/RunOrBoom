using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectivesUIManager : MonoBehaviour
{
    [SerializeField] private Image[] codes;

    private Queue<Image> codesQueue = new Queue<Image>();

    private void Start() => StartQueue();

    private void StartQueue()
    {
        foreach (Image code in codes)
        {
            codesQueue.Enqueue(code);
        }
    }

    public void PickUpCode()
    {
        if (codesQueue.Count == 0)
        {
            return;
        }
        Image code = codesQueue.Dequeue();
        Color currentColor = code.color;
        currentColor.a = 1;
        code.color = currentColor;
    }
}
