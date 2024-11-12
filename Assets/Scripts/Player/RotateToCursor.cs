using UnityEngine;

public class RotateToCursor : MonoBehaviour
{
    private void Update() => Rotate();

    private void Rotate()
    {
        Vector3 mousePosition = MouseHelper.GetPosition();
        transform.eulerAngles = new Vector3(0, 0, TanAngleDeg(mousePosition));
    }

    private float TanAngleDeg(Vector3 position) => Mathf.Atan2(position.y - transform.position.y, (position.x - transform.position.x)) * Mathf.Rad2Deg;

}
