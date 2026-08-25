using UnityEngine;

public class ShieldTrigger : MonoBehaviour
{
    public PlayerController player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!player.shieldActive)
            return;

        if (other.CompareTag("Asteroid"))
        {
            Vector2 hitPoint = other.ClosestPoint(transform.position);

            Destroy(other.gameObject);

            player.BreakShield();

            if (player.shieldHitEffectPrefab != null)
            {
                Instantiate(
                    player.shieldHitEffectPrefab,
                    hitPoint,
                    Quaternion.identity
                );
            }
        }
    }
}