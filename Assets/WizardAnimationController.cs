using UnityEngine;

public class WizardAnimationController : MonoBehaviour
{
    [SerializeField] Animator main;
    [SerializeField] Animator body;

    DialogueStarter starter;

    bool talking;

    private void Start()
    {
        starter = GetComponent<DialogueStarter>();
    }

    private void Update()
    {
        if (talking)
        {
            body.SetBool("isTalking", true);
        }
        else
        {
            body.SetBool("isTalking", false);
        }
    }

    public void startTalking()
    {
        if (starter.dialogueStarted)
        {
            talking = true;
        }
    }

    public void stopTalking()
    {
        talking = false;
    }

    public void floatAway()
    {
        main.SetTrigger("Leave");
    }
}
