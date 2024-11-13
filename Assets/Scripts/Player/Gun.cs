using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Ray origins")]
    [SerializeField]
    GameObject trailOrigin;
    [SerializeField]
    GameObject aimOrigin;

    [Header("Visual trail configuration")]
    [SerializeField]
    float trailDuration;

    [Header("Current weapon settings")]
    [SerializeField]
    GunSO gunSettings;

    float currentAimTime = 0;
    LineRenderer lineRenderer;

    private void Awake() => lineRenderer = trailOrigin.GetComponent<LineRenderer>();

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
        Vector2 direction = (mouseWorldPosition - aimOrigin.transform.position).normalized;

        // Perform raycast from virtual bullet origin
        return Physics2D.Raycast(aimOrigin.transform.position, direction);
    }

    private bool AimingToEnemy(RaycastHit2D hit) => hit.collider != null && hit.collider.gameObject.layer == (int)Layer.Enemy;

    private void TryToShot(RaycastHit2D hit)
    {
        currentAimTime += Time.deltaTime;
        if (currentAimTime >= gunSettings.aimTime)
        {
            VisualShot(hit);
            currentAimTime = 0;
        }
    }

    private void VisualShot(RaycastHit2D hit)
    {
        lineRenderer.SetPosition(0, trailOrigin.transform.position);
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
