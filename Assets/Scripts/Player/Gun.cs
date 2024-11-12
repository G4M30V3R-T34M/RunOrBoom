using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    GameObject rayOrigin;
    [SerializeField]
    float aimTime;
    [SerializeField]
    float trailDuration;

    float currentAimTime = 0;
    LineRenderer lineRenderer;

    private void Awake() => lineRenderer = rayOrigin.GetComponent<LineRenderer>();

    private void Update()
    {
        RaycastHit2D aim = GetGunAim();

        if (AimingToEnemy(aim))
        {
            TryToShot(aim);
        }
        else
        {
            currentAimTime = 0;
        }
    }

    private RaycastHit2D GetGunAim()
    {
        // Get mouse position
        Vector3 mouseWorldPosition = MouseHelper.GetPosition();

        // Compute direction
        Vector2 direction = (mouseWorldPosition - rayOrigin.transform.position).normalized;

        // Perform raycast from virtual bullet origin
        return Physics2D.Raycast(rayOrigin.transform.position, direction);
    }

    private bool AimingToEnemy(RaycastHit2D hit) => hit.collider != null && hit.collider.gameObject.layer == (int)Layer.Enemy;

    private void TryToShot(RaycastHit2D hit)
    {
        currentAimTime += Time.deltaTime;
        if (currentAimTime >= aimTime)
        {
            VisualShot(hit);
            currentAimTime = 0;
        }
    }

    private void VisualShot(RaycastHit2D hit)
    {
        lineRenderer.SetPosition(0, rayOrigin.transform.position);
        lineRenderer.SetPosition(1, hit.point);
        StartCoroutine(RenderLine());
    }

    private IEnumerator RenderLine()
    {
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(trailDuration);
        lineRenderer.enabled = false;
    }
}
