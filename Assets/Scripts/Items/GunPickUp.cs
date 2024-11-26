using FeTo.SOArchitecture;
using UnityEngine;

public class GunPickUp : PickableItem
{
    [Header("Event related")]
    [SerializeField] SOVariable newGunPickUpVariable;
    [SerializeField] GameEvent gunPickedUp;

    private SpriteRenderer gunSpriteRenderer;
    public GunSO gun { private get; set; }

    public override void PickItem()
    {
        newGunPickUpVariable.SetValue(gun);
        gunPickedUp.Raise();
        gameObject.SetActive(false);
    }

    private void Awake() => gunSpriteRenderer = GetComponent<SpriteRenderer>();

    private void OnEnable() => gunSpriteRenderer.sprite = gun?.gunPickUpSprite;


}
