using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;

    void Update()
        => transform.position = new Vector3(
            player.transform.position.x,
            player.transform.position.y,
            this.transform.position.z
        );
}
