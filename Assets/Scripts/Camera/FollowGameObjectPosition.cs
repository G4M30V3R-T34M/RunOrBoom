using UnityEngine;

public class FollowGameObjectPosition : MonoBehaviour
{
    [SerializeField] private GameObject master;

    // Update is called once per frame
    void Update()
        => gameObject.transform.position = new Vector3(
            master.transform.position.x,
            master.transform.position.y,
            gameObject.transform.position.z);
}
