using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class DialogueStarter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject dialogueCanvas;
    [SerializeField] TMP_Text dialogueText;

    [Header("Dialogue Lines")]
    [TextArea]
    [SerializeField] List<string> dialogueLines;

    [Header("Dialogue Properties")]
    [SerializeField] float dialogueOptionalEventDelay;
    [SerializeField] float dialogueCooldownTime = 1;

    [Header("Bark Lines")]
    [SerializeField] List<string> barkLines;

    [Header("Bark Properties")]
    [SerializeField] float barkDuration = 3f;
    [SerializeField] float barkCooldownTime = 5f;

    [Header("Automatic Dialogue Delays")]
    [SerializeField] List<float> dialogueLineAutomaticDelays;

    [Header("Events")]
    [SerializeField] UnityEvent dialogueStartEvent;
    [SerializeField] UnityEvent dialogueStopEvent;
    [SerializeField] UnityEvent dialogueOptionalEvent;
    [SerializeField] UnityEvent barkStartEvent;
    [SerializeField] UnityEvent barkEndEvent;
    [SerializeField] UnityEvent barkCompleteEvent;

    public enum dialogueType { active, dedicated, bark }
    public dialogueType d_type = dialogueType.dedicated;

    public bool dialogueStarted;

    bool canStartDialogue = true;
    bool canActivateNextLine;

    int dialogueCount;
    int barkCount;
    int lastBarkCount;

    public void ResetCoroutines()
    {
        StopAllCoroutines();
    }

    public void StartDialogue()
    {
        if (canStartDialogue)
        {
            // Start automatic message prompt
            if (d_type == dialogueType.active)
            {
                StartCoroutine(dialogueAutomaticDelay());
            }

            Debug.Log("START TALKING");
            dialogueStarted = true;
            dialogueCount++;
            dialogueCanvas.SetActive(true);
            dialogueText.text = dialogueLines[0];
            dialogueStartEvent.Invoke();

            // Start cooldown timer to press next line
            StartCoroutine(dialogueCooldown());
        }
    }

    public void NextLine()
    {
        if (!canActivateNextLine || PauseMenuManager.instance.isPaused) return;

        Debug.Log("NEXT LINE");

        // Check bounds before updating text
        if (dialogueCount < dialogueLines.Count)
        {
            dialogueText.text = dialogueLines[dialogueCount];
        }

        // Increment the count after displaying the current line
        dialogueCount++;

        // If we've displayed the last line, stop the dialogue on the next iteration
        if (dialogueCount >= dialogueLines.Count)
        {
            if(d_type == dialogueType.active)
            {
                StartCoroutine(StopDialogueAfterDelay());
                return;
            }
        }
        
        if(dialogueCount > dialogueLines.Count)
        {
            StopDialogue();
            return;
        }

        // Start cooldown timer to press next line
        StartCoroutine(dialogueCooldown());

        // Start automatic message prompt if in active mode
        if (d_type == dialogueType.active)
        {
            StartCoroutine(dialogueAutomaticDelay());
        }
    }

    public void StartBark()
    {
        if(d_type == dialogueType.bark)
        {
            if(barkCount > barkLines.Count)
            {
                barkCount = 0;
            }
            if(barkStartEvent != null)
            {
                barkStartEvent.Invoke();
            }
            dialogueCanvas.SetActive(true);
            getRandomBark();
            StartCoroutine(barkCooldown());
        }
    }

    void getRandomBark()
    {
        int barkNum = Random.Range(0, barkLines.Count);

        if(barkNum == lastBarkCount)
        {
            while(barkNum == lastBarkCount)
            {
                barkNum = Random.Range(0, barkLines.Count);
            }
        }
        
        if(barkNum != lastBarkCount)
        {
            dialogueText.text = barkLines[barkNum];
            lastBarkCount = barkNum;
        }
    }

    public void StopBark()
    {
        StopCoroutine(barkCooldown());
        dialogueCanvas.SetActive(false);
        if(barkCompleteEvent != null)
        {
            barkCompleteEvent.Invoke();
        }
    }

    public void StopDialogue()
    {
        dialogueStarted = false;
        dialogueCount = 0;
        dialogueCanvas.SetActive(false);
        dialogueText.text = "";
        dialogueStopEvent.Invoke();

        if(dialogueOptionalEvent != null)
        {
            StartCoroutine(optionalEventDealy());
        }
    }

    public void DisableDialogue()
    {
        canStartDialogue = false;
    }

    public void ActivateNextLineButton()
    {
        if(d_type == dialogueType.dedicated)
        {
            NextLine();
        }
    }

    public void SetDialogueType_Active()
    {
        d_type = dialogueType.active;
        ResetCoroutines();
    }

    public void SetDialogueType_Dedicated()
    {
        d_type = dialogueType.dedicated;
        ResetCoroutines();
    }

    public void SetDialogueType_Bark()
    {
        d_type = dialogueType.bark;
        ResetCoroutines();
    }

    IEnumerator dialogueCooldown()
    {
        canActivateNextLine = false;
        yield return new WaitForSeconds(dialogueCooldownTime);
        canActivateNextLine = true;
    }

    IEnumerator optionalEventDealy()
    {
        yield return new WaitForSeconds(dialogueOptionalEventDelay);
        dialogueOptionalEvent.Invoke();
    }

    IEnumerator dialogueAutomaticDelay()
    {
        if (dialogueCount < dialogueLines.Count)
        {
            yield return new WaitForSeconds(dialogueLineAutomaticDelays[dialogueCount]);
            NextLine();
        }
        else
        {
            // Trigger NextLine() for final bounds check and stopping logic
            NextLine();
        }
    }

    IEnumerator StopDialogueAfterDelay()
    {
        // Optional: Wait for cooldown or any additional delay (e.g., last line delay)
        yield return new WaitForSeconds(dialogueLineAutomaticDelays.Count > 0
            ? dialogueLineAutomaticDelays[^1]
            : dialogueCooldownTime);

        StopDialogue();
    }

    IEnumerator barkCooldown()
    {
        yield return new WaitForSeconds(barkDuration);

        dialogueCanvas.SetActive(false);
        dialogueText.text = "";
        if(barkEndEvent != null)
        {
            barkEndEvent.Invoke();
        }

        yield return new WaitForSeconds(barkCooldownTime);

        StartBark();
    }
}
