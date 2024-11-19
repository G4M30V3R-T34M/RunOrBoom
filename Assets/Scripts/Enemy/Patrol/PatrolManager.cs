using UnityEngine;

public class PatrolManager : MonoBehaviour, IVisionNotificable
{
    [SerializeField] PatrolRotationController patrolRight;
    [SerializeField] PatrolRotationController patrolLeft;
    [SerializeField] PatrolRotateToTarget patrolRotateToTarget;

    private void Start()
    {
        patrolRight.SetManager(this);
        patrolLeft.SetManager(this);
        patrolLeft.enabled = false;
        patrolRotateToTarget.enabled = false;
    }

    public void CollisionWithWall()
    {
        patrolRight.enabled = !patrolRight.enabled;
        patrolLeft.enabled = !patrolLeft.enabled;
    }

    public void TargetInSight(RaycastHit2D hit)
    {
        patrolLeft.enabled = false;
        patrolRight.enabled = false;
        patrolRotateToTarget.enabled = true;
        patrolRotateToTarget.SetTarget(hit.transform.gameObject);
    }
    public void TargetNotFound(RaycastHit2D hit)
    {
        if (patrolRotateToTarget.enabled)
        {
            patrolLeft.enabled = false;
            patrolRight.enabled = true;
            patrolRotateToTarget.enabled = false;
        }
    }
}
