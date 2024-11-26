using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] float jumpPower = 16f;

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerMovement.instance.JumpPad(jumpPower);

            anim.SetTrigger("Spring");
        }
    }
}
