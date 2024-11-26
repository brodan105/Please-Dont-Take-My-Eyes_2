using System.Collections;
using UnityEngine;

public class WizardBossController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform playerPos;
    [SerializeField] Vector2 playerOffset;
    [SerializeField] GameObject hand_Left;
    [SerializeField] GameObject hand_Right;
    [SerializeField]  BossEnemy _bossEnemy;

    Animator anim;

    // Properties
    string bossName;
    float attackCooldown;
    float health;
    float damage;

    // last attack tracker
    int lastAttack;
    int attacksRepeated;

    bool canAttack;
    bool animationActive;

    [Header("Properties")]
    [SerializeField] float attack1Time;
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float attackSpeed = 5f;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        bossName = _bossEnemy._name;
        attackCooldown = _bossEnemy._attackCooldown;
        health = _bossEnemy._health;
        damage = _bossEnemy._damage;
    }

    private void Idle()
    {
        anim.SetBool("isIdle", true);
    }

    public void Stuck()
    {
        anim.SetBool("isStuck", true);
    }

    private void CheckIfAttack()
    {
        if (canAttack)
        {
            int count = Random.Range(0, 30);

            if(count <= 10)
            {
                if(attacksRepeated > 1)
                {
                    attacksRepeated = 0;

                    int count2 = Random.Range(0, 20);

                    if(count2 <= 10)
                    {
                        Attack2();
                    }
                    else
                    {
                        Attack3();
                    }
                }
                else
                {
                    Attack1();

                    if(lastAttack == 1)
                    {
                        attacksRepeated++;
                    }
                }
            }
            else if(count > 10 && count < 21)
            {
                if (attacksRepeated > 1)
                {
                    attacksRepeated = 0;

                    int count2 = Random.Range(0, 20);

                    if (count2 <= 10)
                    {
                        Attack1 ();
                    }
                    else
                    {
                        Attack3();
                    }
                }
                else
                {
                    Attack2();

                    if (lastAttack == 2)
                    {
                        attacksRepeated++;
                    }
                }
            }
            else
            {
                if (attacksRepeated > 1)
                {
                    attacksRepeated = 0;

                    int count2 = Random.Range(0, 20);

                    if (count2 <= 10)
                    {
                        Attack1();
                    }
                    else
                    {
                        Attack2();
                    }
                }
                else
                {
                    Attack3();

                    if (lastAttack == 3)
                    {
                        attacksRepeated++;
                    }
                }
            }

            canAttack = false;
        }
    }

    public void ActionCooldown()
    {
        StartCoroutine(actionCooldown());
    }

    // Fist Slam Attack
    private void Attack1()
    {
        // set hand to fist
        anim.SetTrigger("attack1");
        lastAttack = 1;

        Debug.Log("ATTACK 1");
    }

    private void Attack2()
    {
        anim.SetTrigger("attack2");
        lastAttack = 2;

        Debug.Log("ATTACK 2");
    }

    private void Attack3()
    {
        anim.SetTrigger("attack3");
        lastAttack = 3;

        Debug.Log("ATTACK 3");

    }

    public void AttackCooldown()
    {
        StartCoroutine(attackCooldownTimer());
    }

    IEnumerator attackCooldownTimer()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    IEnumerator actionCooldown()
    {
        yield return new WaitForSeconds(5);
        canAttack = true;
        CheckIfAttack();
    }
}
