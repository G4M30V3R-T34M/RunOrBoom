using UnityEngine;
using UnityEngine.InputSystem;

public class RotateToCursor : MonoBehaviour
{
    private Camera mainCamera;

    private void Start() => mainCamera = Camera.main;

    private void Update() => Rotate();

    private void Rotate()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(GetMousePosition());
        transform.eulerAngles = new Vector3(0, 0, TanAngleDeg(mousePosition));
    }

    private Vector3 GetMousePosition()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        return new Vector3(mousePosition.x, mousePosition.y, mainCamera.transform.position.z);
    }

    private float TanAngleDeg(Vector3 position) => Mathf.Atan2(position.y - transform.position.y, (position.x - transform.position.x)) * Mathf.Rad2Deg;

}
