using UnityEngine;
using Unity.Cinemachine;

public class KillBug : MonoBehaviour
{
    [Header("References")]
    [SerializeField] BoxCollider2D deathTrigger;
    [SerializeField] Animator bugAnim;
    [SerializeField] BugPatrol _patrol;
    [SerializeField] BugAudioController _audioControl;

    CinemachineImpulseSource impulseSource;

    public bool isDead = false;

    private void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;

        if(collision.tag == "Player" && !PlayerDie.instance.hasDied)
        {
            killBug();
        }
    }

    void killBug()
    {
        deathTrigger.enabled = false;
        _patrol.enabled = false;
        bugAnim.SetTrigger("Die");
        _audioControl.bugDeathSound();
        isDead = true;
        PlayerMovement.instance.ImpulseJump();
        CameraShakeManager.instance.CameraShake(impulseSource);

        // Update tally count
        TallyCountManager.instance.enemyCount++;
    }
}
