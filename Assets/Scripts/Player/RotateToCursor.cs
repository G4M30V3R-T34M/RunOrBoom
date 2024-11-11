using UnityEngine;

public class RotateToCursor : MonoBehaviour
{
    Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(GetMousePosition());
        transform.eulerAngles = new Vector3(0, 0, TanAngleDeg(mousePosition));
    }

    private Vector3 GetMousePosition()
    {
        return new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - mainCamera.transform.position.z);
    }

    private float TanAngleDeg(Vector3 position)
    {
        return Mathf.Atan2((position.y - transform.position.y), (position.x - transform.position.x)) * Mathf.Rad2Deg;
    }
}
