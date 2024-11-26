using UnityEngine;

public class TriggerSubAnimation : MonoBehaviour
{
    [SerializeField] string animTriggerName;
    [SerializeField] Animator anim;

    public void TriggerAnimation()
    {
        anim.SetTrigger(animTriggerName);
    }
}
