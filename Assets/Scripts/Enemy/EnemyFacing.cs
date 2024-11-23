using UnityEngine;

public class EnemyFacing : MonoBehaviour
{

    private static readonly LayerMask wall = LayerMaskHelper.CreateLayerMask(new Layer[] { Layer.Wall });

    private void OnEnable() => FaceToLongestDistance();

    private void FaceToLongestDistance()
    {
        Vector2 direction = GetLongestDirection();
        FaceToDirection(direction);
    }

    private Vector2 GetLongestDirection()
    {
        Vector2[] directions = {
            Vector2.up,
            Vector2.down,
            Vector2.left,
            Vector2.right,
        };

        float maxDistance = 0;
        Vector2 longestDirection = Vector2.zero;

        foreach (Vector2 direction in directions)
        {
            float distance = GetWallDistance(direction);
            if (distance > maxDistance)
            {
                maxDistance = distance;
                longestDirection = direction;
            }
        }
        return longestDirection;
    }

    private float GetWallDistance(Vector2 direction) => Physics2D.Raycast(
        transform.position,
        direction,
        Mathf.Infinity,
        wall
    ).distance;

    private void FaceToDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
