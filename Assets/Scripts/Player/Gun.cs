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

    private float currentTimeToAim = 0;
    private float currentShotCooldown = 0;

    private float weaponRange;

    private LineRenderer lineRenderer;

    private void Awake() => lineRenderer = trailOrigin.GetComponent<LineRenderer>();

    private void Start() => weaponRange = gunSettings.range == 0 ? Mathf.Infinity : gunSettings.range;

    private void Update()
    {
        RaycastHit2D aim = GetGunAim();

        currentTimeToAim = AimingToEnemy(aim) ? currentTimeToAim + Time.deltaTime : 0;
        currentShotCooldown = AimingToEnemy(aim) ? currentShotCooldown : 0;

        Debug.Log(aim.collider);
        Debug.Log("Current time to aim: " + currentTimeToAim);
        Debug.Log("Current shot cooldown: " + currentShotCooldown);

        if (HasStartedToAim())
        {
            Aim(aim);
            TryToShot(aim);
        }
        Debug.DrawRay(aimOrigin.transform.position, aimOrigin.transform.right, Color.red);
    }

    private RaycastHit2D GetGunAim() => Physics2D.Raycast(aimOrigin.transform.position, aimOrigin.transform.right, weaponRange, gunSettings.collisionLayerMask);

    private bool HasStartedToAim() => currentTimeToAim >= gunSettings.timeToAim;

    private void Aim(RaycastHit2D aim) => currentShotCooldown += Time.deltaTime;

    private bool AimingToEnemy(RaycastHit2D hit) => hit.collider != null && hit.collider.gameObject.layer == (int)gunSettings.enemyLayer;

    private void TryToShot(RaycastHit2D hit)
    {
        if (currentShotCooldown >= gunSettings.shotCooldown)
        {
            // Pending apply damage to target
            VisualShot(hit); // Remove when Event is implement
            currentShotCooldown = 0;
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
