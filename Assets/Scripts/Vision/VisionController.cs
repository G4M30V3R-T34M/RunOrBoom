using System.Collections.Generic;
using UnityEngine;

public class VisionController : MonoBehaviour
{
    [SerializeField] float visionRange;
    [SerializeField] Layer[] detectionLayers;
    [SerializeField] Layer targetLayer;
    // Since Interface are not serializable this array store game objects which
    // should be IVisionNotificalbe
    [SerializeField] List<MonoBehaviour> visionListeners;

    private LayerMask detectionLayerMasks;

    private void Start()
        => detectionLayerMasks = LayerMaskHelper.CreateLayerMask(
            detectionLayers
        );

    private void Update()
    {
        RaycastHit2D vision = GetVisibility();
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
        visionRange,
        detectionLayerMasks
    );

    private bool TargetInSight(RaycastHit2D hit)
        => hit.collider != null
            && hit.collider.gameObject.layer == (int)targetLayer;

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
