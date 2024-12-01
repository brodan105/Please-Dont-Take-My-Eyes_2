using System.Collections;
using UnityEngine;

public class TrialController : MonoBehaviour
{
    public int trialCounter = 0;

    public static TrialController instance;

    [SerializeField] Animator anim;
    [SerializeField] Animator rightAnim;

    private void Start()
    {
        instance = this;
    }

    public void QueueNextTrial()
    {
        StartCoroutine(TrialDelay());
        TrialRetract();
    }
    public void NextTrial()
    {
        //if (trialCounter == 0 || trialCounter > 5) return;

        trialCounter++;
        TrialExtend();
    }

    void TrialExtend()
    {
        anim.SetTrigger("Trial" + trialCounter + "_Extend");
    }

    void TrialRetract()
    {
        anim.SetTrigger("Trial" + trialCounter + "_Retract");
    }

    public void ExtendRightPlatform()
    {
        rightAnim.SetTrigger("Extend");
    }

    public void RetractRightPlatform()
    {
        rightAnim.SetTrigger("Retract");
    }

    IEnumerator TrialDelay()
    {
        yield return new WaitForSeconds(15);
        NextTrial();
    }
}
