using UnityEngine;

public class PatrolStateMachine : MonoBehaviour
{
    [SerializeField] PatrolRotationController patrolRight;
    [SerializeField] PatrolRotationController patrolLeft;

    public void RightCollision()
    {
        patrolRight.enabled = false;
        patrolLeft.enabled = true;
    }

    public void LeftCollision()
    {
        patrolRight.enabled = true;
        patrolLeft.enabled = false;
    }

    public void PlayerInSight()
    {
        patrolLeft.enabled = false;
        patrolRight.enabled = false;
    }

    public void PlayerLost()
    {
        patrolRight.enabled = true;
        patrolLeft.enabled = false;
    }
}
