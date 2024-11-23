using UnityEngine;

interface IVisionNotificable
{
    public void TargetInSight(RaycastHit2D hit);
    public void TargetNotFound(RaycastHit2D hit);
}
