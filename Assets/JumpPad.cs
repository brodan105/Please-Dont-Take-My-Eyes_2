using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] float jumpPower = 16f;

    [SerializeField] AudioClip sfx;

    Animator anim;
    AudioSource _source;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        _source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerMovement.instance.JumpPad(jumpPower);
            PlayerMovement.instance.usedJumpad = true;

            anim.SetTrigger("Spring");

            _source.pitch = Random.Range(0.8f, 1.25f);
            _source.PlayOneShot(sfx);
        }
    }
}
