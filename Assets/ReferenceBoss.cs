using UnityEngine;

public class ReferenceBoss : MonoBehaviour
{
    [SerializeField] WizardBossController _control;

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void HurtBoss2()
    {
        Debug.Log("ATTACK");
        //anim.SetTrigger("Attack");
        _control.HurtBoss();
    }
}
