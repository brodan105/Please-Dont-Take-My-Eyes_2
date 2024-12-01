using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class WizardBossController : MonoBehaviour
{
    [SerializeField] UnityEvent _defeatEvent;
    [SerializeField] UnityEvent _creditEvent;

    [Header("References")]
    [SerializeField] Transform playerPos;
    [SerializeField] Vector2 playerOffset;
    [SerializeField] GameObject hand_Left;
    [SerializeField] GameObject hand_Right;
    [SerializeField] BossEnemy _bossEnemy;

    Animator anim;

    // Properties
    string bossName;
    float attackCooldown;
    [SerializeField] float health;
    float damage;

    // last attack tracker
    int lastAttack;
    int attacksRepeated;

    bool canAttack;
    bool animationActive;
    bool defeated;

    [Header("Properties")]
    [SerializeField] float attack1Time;
    float cooldownTime = 5;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        bossName = _bossEnemy._name;
        attackCooldown = _bossEnemy._attackCooldown;
        health = _bossEnemy._health;
        damage = _bossEnemy._damage;
    }

    private void Update()
    {
        if(health == 60)
        {
            cooldownTime = 2.5f;
            anim.SetFloat("SpeedMultiplier", 1.2f);
        }
        else if(health == 40)
        {
            cooldownTime = 1.5f;
            anim.SetFloat("SpeedMultiplier", 1.3f);
        }
        else if(health == 20)
        {
            cooldownTime = 0.5f;
            anim.SetFloat("SpeedMultiplier", 1.4f);
        }
        else
        {
            cooldownTime = 3;
        }

        if(health <= 0 && !defeated)
        {
            defeated = true;
            Defeat();
        }
    }

    public void HurtBoss()
    {
        anim.SetTrigger("Hurt");
    }

    public void Defeat()
    {
        Debug.Log("DEFEATED");
        anim.SetBool("isDefeated", true);
        anim.SetBool("isIdle", false);
        _defeatEvent.Invoke();
        StopAllCoroutines();
        StopCoroutine(actionCooldown());
        TimeController.instance.StopTimer();
        StartCoroutine(Delay());
        TrialController.instance.RetractRightPlatform();
    }

    public void GetDamaged()
    {
        StopAllCoroutines();

        health -= 20;

        Debug.Log(health);
    }

    private void Idle()
    {
        anim.SetBool("isIdle", true);
    }

    public void Stuck()
    {
        anim.SetBool("isStuck", true);
    }

    public void ExtendNextTrials()
    {
        TrialController.instance.NextTrial();
        TrialController.instance.ExtendRightPlatform();
    }

    private void CheckIfAttack()
    {
        if (canAttack && !defeated)
        {
            int count = Random.Range(0, 40);

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
                    else if (count > 10 && count2 < 21)
                    {
                        Attack3();
                    }
                    else
                    {
                        Attack4();
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
                    else if (count > 10 && count2 < 21)
                    {
                        Attack3();
                    }
                    else
                    {
                        Attack4();
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
            else if(count > 21 && count < 31)
            {
                if (attacksRepeated > 1)
                {
                    attacksRepeated = 0;

                    int count2 = Random.Range(0, 30);

                    if (count2 <= 10)
                    {
                        Attack1();
                    }
                    else if(count > 10 && count2 < 21)
                    {
                        Attack2();
                    }
                    else
                    {
                        Attack4();
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
            else
            {
                if (attacksRepeated > 1)
                {
                    attacksRepeated = 0;

                    int count2 = Random.Range(0, 30);

                    if (count2 <= 10)
                    {
                        Attack1();
                    }
                    else if(count2 > 10 && count2 < 21)
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
                    Attack4();

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
    }

    private void Attack2()
    {
        anim.SetTrigger("attack2");
        lastAttack = 2;
    }

    private void Attack3()
    {
        anim.SetTrigger("attack3");
        lastAttack = 3;

    }

    private void Attack4()
    {
        anim.SetTrigger("attack4");
        lastAttack = 4;

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
        yield return new WaitForSeconds(cooldownTime);
        canAttack = true;
        CheckIfAttack();
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(8);
        _creditEvent.Invoke();
    }
}
