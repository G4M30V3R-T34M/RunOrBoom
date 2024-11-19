using UnityEngine;

public class PatrolRotateToTarget : MonoBehaviour
{
    [SerializeField] GameObject parentToRotate;
    [SerializeField] float rotationSpeed;
    private GameObject target;

    private void OnDisable() => target = null;

    private void Update()
    {
        if (target != null)
        {
            // Set the facing direction to the target
            parentToRotate.transform.right = target.transform.position - parentToRotate.transform.position;
        }
    }

    public void SetTarget(GameObject newTarget) => target = newTarget;
}
