using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class DialogueStarter : MonoBehaviour
{
    [SerializeField] GameObject dialogueCanvas;
    [SerializeField] TMP_Text dialogueText;

    [TextArea]
    [SerializeField] List<string> dialogueLines;

    [SerializeField] List<float> dialogueLineAutomaticDelays;

    [SerializeField] UnityEvent dialogueStartEvent;
    [SerializeField] UnityEvent dialogueStopEvent;
    [SerializeField] UnityEvent dialogueOptionalEvent;

    [SerializeField] float dialogueOptionalEventDelay;

    public enum dialogueType { active, dedicated }
    public dialogueType d_type = dialogueType.dedicated;

    [SerializeField] float dialogueCooldownTime = 1;

    public bool dialogueStarted;

    bool canStartDialogue = true;
    bool canActivateNextLine;

    int dialogueCount;

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
        if (!canActivateNextLine) return;

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
}
