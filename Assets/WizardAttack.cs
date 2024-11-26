using System.Collections;
using UnityEngine;

public class WizardAttack : MonoBehaviour
{
    [SerializeField] GameObject _attackObject;
    [SerializeField] Transform _attackSpawnPoint;

    Animator anim;

    bool canAttack = true;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            anim.SetTrigger("Attack");
        }
    }

    public void Attack()
    {
        if (!canAttack) return;

        Debug.Log("ATTACK HIZAAAHH");

        // Instaniate attack and fire towards player current position
        Instantiate(_attackObject, _attackSpawnPoint);

        StartCoroutine(attackCooldown());
    }

    IEnumerator attackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(3f);
        canAttack = true;
    }
}
