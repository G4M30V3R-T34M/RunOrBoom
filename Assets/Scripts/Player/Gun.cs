using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [SerializeField]
    GameObject rayOrigin;
    [SerializeField]
    float aimTime;

    float currentAimTime = 0;

    void Start()
    {
    }

    void Update()
    {
        CheckRaycast();
    }

    void CheckRaycast()
    {
        // Get mouse position
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mouseWorldPosition.z = 0;

        // Compute direction
        Vector2 direction = (mouseWorldPosition - rayOrigin.transform.position).normalized;

        // create the Raycast to infinity that only colides with enemy layer
        RaycastHit2D hit = Physics2D.Raycast(
            rayOrigin.transform.position,
            direction,
            Mathf.Infinity,
            (int)Layer.Enemy
        );

        if (hit.collider != null) {
            currentAimTime += Time.deltaTime;
            if (currentAimTime >= aimTime) {
                // Shot
                currentAimTime = 0;
            }
        }
        else {
            currentAimTime = 0;
        }

        // Draw the ray for debugging
        Debug.DrawLine(rayOrigin.transform.position, mouseWorldPosition, Color.red);
    }
}
