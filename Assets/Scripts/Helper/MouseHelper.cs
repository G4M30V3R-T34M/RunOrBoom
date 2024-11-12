using UnityEngine;
using UnityEngine.InputSystem;

public static class MouseHelper
{
    public static Vector3 GetPosition() => Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
}
