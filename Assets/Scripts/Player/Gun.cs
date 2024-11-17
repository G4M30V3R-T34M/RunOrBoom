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

    private float currentReactionTime = 0;
    private float currentAimTime = 0;

    private float weaponRange;

    private LineRenderer lineRenderer;

    private void Awake() => lineRenderer = trailOrigin.GetComponent<LineRenderer>();

    private void Start() => weaponRange = gunSettings.range == 0 ? Mathf.Infinity : gunSettings.range;

    private void Update()
    {
        RaycastHit2D hit = GetGunAim();

        if (AimingToTarget(hit))
        {
            currentReactionTime += Time.deltaTime;
        }
        else
        {
            currentReactionTime = 0;
            currentAimTime = 0;
        }

        if (HasStartedToAim())
        {
            Aim(hit);
            TryToShot(hit);
        }
    }

    private RaycastHit2D GetGunAim() => Physics2D.Raycast(aimOrigin.transform.position, aimOrigin.transform.right, weaponRange, gunSettings.collisionLayerMask);

    private bool HasStartedToAim() => currentReactionTime >= gunSettings.reactionTime;

    private void Aim(RaycastHit2D hit) => currentAimTime += Time.deltaTime;

    private bool AimingToTarget(RaycastHit2D hit) => hit.collider != null && hit.collider.gameObject.layer == (int)gunSettings.targetLayer;

    private void TryToShot(RaycastHit2D hit)
    {
        if (currentAimTime >= gunSettings.aimingTime)
        {
            // Pending apply damage to target
            VisualShot(hit); // Remove when Event is implement
            currentAimTime = 0;
        }
    }

    // Pending move to other script when Event is implemented
    private void VisualShot(RaycastHit2D hit)
    {
        lineRenderer.SetPosition(0, trailOrigin.transform.position);
        lineRenderer.SetPosition(1, hit.point);
        StartCoroutine(RenderLine());
    }

    // Pending move to other script when Event is implemented
    private IEnumerator RenderLine()
    {
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(trailDuration);
        lineRenderer.enabled = false;
    }
}
