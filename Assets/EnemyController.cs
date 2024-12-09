using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Enemy enemySO;

    [SerializeField] KillBug k_bug;

    float health;
    float damage;

    private void Start()
    {
        health = enemySO.enemyHealth;
        damage = enemySO.enemyDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (k_bug.isDead) return;

        if(collision.tag == "SlashHitBox")
        {
            health -= WeaponManager.instance.damage;

            // flash white on enemy

            // pause enemy movement momentarily

            if(health <= 0)
            {
                k_bug.killBug();
            }
        }

        if(collision.tag == "Player")
        {
            PlayerHealth.instance.playerHealth -= damage;
            CameraShakeManager.instance.CameraShake(k_bug.impulseSource);

            // CHANGE TO PUSH UP AND BACK AWAY FROM ENEMY
            PlayerMovement.instance.ImpulseJump();

            Debug.Log("Player Health: " + PlayerHealth.instance.playerHealth);

            if (PlayerHealth.instance.playerHealth <= 0)
            {
                PlayerDie.instance.Die();
            }
        }
    }
}
