using UnityEngine;

public class ObstacleDestroy : Obstacle
{
    internal override void OnCollision(Collision collision)
    {
        base.OnCollision(collision);
        Destroy(gameObject);
    }
}
