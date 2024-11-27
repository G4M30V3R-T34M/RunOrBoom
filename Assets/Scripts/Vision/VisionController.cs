using System.Collections.Generic;
using UnityEngine;

public class VisionController : MonoBehaviour
{
    [SerializeField] EnemySO enemySettings;

    // Since Interface are not serializable this array store game objects which
    // should be IVisionNotificalbe
    [SerializeField] List<MonoBehaviour> visionListeners;

    private LayerMask detectionLayerMasks;
    private LineRenderer lineRenderer;

    private void Awake() => lineRenderer = GetComponent<LineRenderer>();

    private void Start()
        => detectionLayerMasks = LayerMaskHelper.CreateLayerMask(
            enemySettings.detectionLayers
        );

    private void Update()
    {
        RaycastHit2D vision = GetVisibility();
        UpdateVisionLine(vision);
        if (TargetInSight(vision))
        {
            NotifyTagetInSight(vision);
        }
        else
        {
            NotifyTargetNotFound(vision);
        }
    }

    private RaycastHit2D GetVisibility() => Physics2D.Raycast(
        transform.position,
        transform.right,
        enemySettings.visionRange,
        detectionLayerMasks
    );

    private void UpdateVisionLine(RaycastHit2D vision)
    {
        if (vision.collider)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, vision.point);
        }
        else
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position + (Vector3)transform.right * enemySettings.visionRange);
        }
    }

    private bool TargetInSight(RaycastHit2D hit)
        => hit.collider != null
            && hit.collider.gameObject.layer == (int)enemySettings.targetLayer;

    // Due we can't serialize IVisionNotificable use listener as IVisionNotificable
    // and if it's the interface call to TargetInSight
    private void NotifyTagetInSight(RaycastHit2D hit)
        => visionListeners.ForEach(
            listener => (listener as IVisionNotificable)
            ?.TargetInSight(hit)
        );

    // Due we can't serialize IVisionNotificable use listener as IVisionNotificable
    // and if it's the interface call to TargetNotFound
    private void NotifyTargetNotFound(RaycastHit2D hit)
        => visionListeners.ForEach(
            listener => (listener as IVisionNotificable)
            ?.TargetNotFound(hit)
        );

}
