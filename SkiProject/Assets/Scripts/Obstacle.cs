using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public delegate void playerHitAction();
    public static event playerHitAction OnPlayerHit;
    private void OnCollisionEnter(Collision collision)
    {
        OnCollision(collision);
    }

    internal virtual void OnCollision(Collision collision) // virtual = var parrakstit
    {
        if (collision.collider.tag.Equals("Player"))
        {
            Debug.Log("Player Collier with " + name);
            OnPlayerHit.Invoke();
        }
    }
}
