using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float speed = 1f;

    private Vector2 _movementInput;
    private Rigidbody2D _rigidbody;

    private void Start() => _rigidbody = GetComponent<Rigidbody2D>();

    private void FixedUpdate() => _rigidbody.linearVelocity = _movementInput * speed;

    private void OnMove(InputValue inputValue) => _movementInput = inputValue.Get<Vector2>();
}
