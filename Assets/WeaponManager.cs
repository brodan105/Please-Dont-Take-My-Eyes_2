using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;

    public bool hasWeapon = false;

    public enum attackType { swing, }
    public attackType aType = attackType.swing;

    [SerializeField] GameObject weapon;
    [SerializeField] Animator weaponAnim;

    [SerializeField] float swingCooldownTime;

    public float damage = 25f;

    bool canAttack;
    bool canSwing = true;

    private void Start()
    {
        instance = this;

        if (!hasWeapon)
        {
            weapon.SetActive(false);
            canAttack = false;
        }
        else
        {
            weapon.SetActive(true);
            canAttack = true;
        }
    }

    public void RecieveWeapon()
    {
        hasWeapon = true;
        canAttack = true;
        weapon.SetActive(true);
    }

    private void Swing()
    {
        if (!canSwing) return;

        StartCoroutine(SwingCooldown());
        weaponAnim.SetTrigger("swing");
        SwingManager.instance.slashAnim.SetTrigger("Slash");
        SwingManager.instance.slashHitBox.GetComponent<Collider2D>().enabled = true;
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (!canAttack) return;

        if (context.action.WasPressedThisFrame())
        {
            if(aType == attackType.swing)
            {
                Swing();
            }
        }
    }

    public void StartTalking()
    {
        weaponAnim.SetTrigger("talk");
        canAttack = false;
    }

    public void StopTalking()
    {
        weaponAnim.SetTrigger("stopTalk");

        if (hasWeapon)
            canAttack = true;
    }

    IEnumerator SwingCooldown()
    {
        SwingManager.instance.slashHitBox.GetComponent<Collider2D>().enabled = false;
        canSwing = false;
        SwingManager.instance.canSwing = false;
        yield return new WaitForSeconds(swingCooldownTime);
        SwingManager.instance.canSwing = true;
        canSwing = true;
    }
}
