using FeTo.SOArchitecture;
using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Ray origins")]
    [SerializeField] GameObject trailOrigin;
    [SerializeField] GameObject aimOrigin;

    [Header("Visual trail configuration")]
    [SerializeField] float trailDuration;

    [Header("Current weapon settings")]
    [SerializeField] GunSO gunSettings;

    [Header("Events")]
    [SerializeField] GameEvent bulletShotEvent;
    [SerializeField] GameEvent outOfAmmoEvent;

    [Header("Variable")]
    [SerializeField] IntVariable currentAmmoVariable;

    private float currentReactionTime = 0;
    private float currentAimTime = 0;

    private float weaponRange;

    private LineRenderer lineRenderer;
    private SpriteRenderer spriteRenderer;

    private int currentAmmo;

    private void Awake()
    {
        lineRenderer = trailOrigin.GetComponent<LineRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable() => UpdateGun();

    private void Start()
        => weaponRange = GetWeaponRange();

    private void Update()
    {
        RaycastHit2D hit = GetGunAim();

        if (TargetInSight(hit))
        {
            currentReactionTime += Time.deltaTime;
        }
        else
        {
            currentReactionTime = 0;
            currentAimTime = 0;
        }

        if (CanReact())
        {
            Aim(hit);
            TryToShot(hit);
        }
    }

    public void ChangeGun(GunSO newGun)
    {
        gunSettings = newGun;
        UpdateGun();
    }

    private void UpdateGun()
    {
        spriteRenderer.sprite = gunSettings.gunSprite;
        currentAmmo = gunSettings.ammunition;
        currentAmmoVariable?.SetValue(currentAmmo);
    }

    private RaycastHit2D GetGunAim() => Physics2D.Raycast(
        aimOrigin.transform.position,
        aimOrigin.transform.right,
        weaponRange,
        gunSettings.collisionLayerMask
    );

    private bool CanReact() => currentReactionTime >= gunSettings.reactionTime;

    private void Aim(RaycastHit2D hit) => currentAimTime += Time.deltaTime;

    private bool TargetInSight(RaycastHit2D hit)
        => hit.collider != null
            && hit.collider.gameObject.layer == (int)gunSettings.targetLayer;

    private void TryToShot(RaycastHit2D hit)
    {
        if (currentAimTime >= gunSettings.aimingTime)
        {
            hit.collider.gameObject.GetComponent<HealthManager>().TakeDamage(gunSettings.damage);
            VisualShot(hit);
            currentAimTime = 0;
            currentAmmo -= 1;
            currentAmmoVariable?.SetValue(currentAmmo);
            NotifyGunState();
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

    private float GetWeaponRange()
        => (gunSettings.range == 0)
            ? Mathf.Infinity
            : gunSettings.range;

    private void NotifyGunState()
    {
        if (gunSettings.isDefault)
        {
            return;
        }
        if (currentAmmo == 0)
        {
            outOfAmmoEvent?.Raise();
        }
        else
        {
            bulletShotEvent?.Raise();
        }
    }
}
