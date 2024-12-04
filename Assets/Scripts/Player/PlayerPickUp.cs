using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    private PickableItem pickedUpItem;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)Layer.PickUp)
        {
            pickedUpItem = collision.gameObject.GetComponent<PickableItem>();
            pickedUpItem?.PickItem();
        }
    }
}
