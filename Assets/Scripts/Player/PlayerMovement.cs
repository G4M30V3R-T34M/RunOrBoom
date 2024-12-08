using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerSO playerSettings;

    private Animator animator;
    private Vector2 _movementInput;
    private Rigidbody2D _rigidbody;
    private float currentSpeed;

    private void Awake() => animator = GetComponent<Animator>();

    private void Start() => _rigidbody = GetComponent<Rigidbody2D>();

    private void OnEnable() => currentSpeed = playerSettings.speed;

    private void FixedUpdate() => _rigidbody.linearVelocity = _movementInput * currentSpeed;

    private void OnMove(InputValue inputValue)
    {
        _movementInput = inputValue.Get<Vector2>();
        bool is_walking = _movementInput != Vector2.zero;
        animator.SetBool("Walking", is_walking);
    }

    public void SlowDown()
    {
        if (currentSpeed >= playerSettings.slowDownValue)
        {
            StartCoroutine(SlowDownCoroutine());
        }
    }

    private IEnumerator SlowDownCoroutine()
    {
        currentSpeed -= playerSettings.slowDownValue;
        yield return new WaitForSeconds(playerSettings.slowDownDuration);
        currentSpeed += playerSettings.slowDownValue;
    }
}
