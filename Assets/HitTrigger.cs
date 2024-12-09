using UnityEngine;

public class HitTrigger : MonoBehaviour
{
    [SerializeField] float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            // collision.gameobject.GetComponent<EnemyDamageScript>().health -= damage;
        }
    }
}
