using UnityEngine;

public class PatrolRotationController : MonoBehaviour
{
    [Header("References to element to rotate")]
    [SerializeField] GameObject parentToRotate;

    [Header("Script configuration")]
    [SerializeField] EnemySO enemySO;
    [SerializeField] bool rotateRight;

    private int rotationMagnitude;
    private Collider2D wallDetector;
    private PatrolManager patrolManager;

    private void Awake() => wallDetector = GetComponent<Collider2D>();

    private void Start() => rotationMagnitude = rotateRight ? -1 : 1;

    private void OnEnable() => wallDetector.enabled = true;

    private void OnDisable() => wallDetector.enabled = false;

    private void Update() => parentToRotate.transform.Rotate(
        rotationMagnitude * Vector3.forward * enemySO.rotationSpeed * Time.deltaTime);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)Layer.Wall)
        {
            patrolManager?.CollisionWithWall();
        }
    }

    public void SetManager(PatrolManager manager) => patrolManager = manager;
}
