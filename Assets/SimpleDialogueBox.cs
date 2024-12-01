using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class SimpleDialogueBox : MonoBehaviour
{
    // The event that will be called after all the strings in this dialoge chain are run through (used to start a new dialogue chain)
    [SerializeField] UnityEvent _onCompleteEvent;

    [SerializeField]
    [TextArea]
    private List<string> _dialogueLines;

    private int _lineIndex;

    private TMP_Text _text;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    // When instantiated into the world (usually by a trigger or some sort of world event), set the dialogue lines needed for the dialogue chain
    public void SetDialogueStrings(List<string> dialogueLines)
    {
        _dialogueLines = new List<string>(dialogueLines);
    }
}
