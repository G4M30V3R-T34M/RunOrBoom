using FeTo.SOArchitecture;
using UnityEngine;

public class GunPickUp : PickableItem
{
    [Header("Gun to pick up")]
    [SerializeField] GunSO gun;

    [Header("Event related")]
    [SerializeField] SOVariable newGunPickUpVariable;
    [SerializeField] GameEvent gunPickedUp;

    SpriteRenderer gunSpriteRenderer;

    public override void PickItem()
    {
        newGunPickUpVariable.SetValue(gun);
        gunPickedUp.Raise();
    }

    private void Awake() => gunSpriteRenderer = GetComponent<SpriteRenderer>();

    private void OnEnable() => gunSpriteRenderer.sprite = gun.gunPickUpSprite;
}
