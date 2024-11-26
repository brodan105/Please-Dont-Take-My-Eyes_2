using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueBoxTextController : MonoBehaviour
{
    [SerializeField]
    [TextArea]
    private List<string> _dialogueLines;
    private int _lineIndex;

    private TMP_Text _text;
    private CanvasGroup _group;
    private bool _started;

        private void Start()
    {
        _text = GetComponent<TMP_Text>();
        _group = GetComponent<CanvasGroup>();
        _group.alpha = 0;
    }

    private void Update()
    {
        
    }

    // input action used to select next dialogue when interacting with npc
    public void NextDialogueLine(InputAction.CallbackContext context)
    {
        if (context.action.WasPressedThisFrame())
        {
            if (!_started)
            {
                _lineIndex = 0;
                _text.SetText(_dialogueLines[_lineIndex]);
                _group.alpha = 1;
                _started = true;
            }
            else if(_lineIndex < _dialogueLines.Count)
            {
                _text.SetText(_dialogueLines[_lineIndex++]); // use current value of _lineIndex and then add 1 to it
            }
            else
            {
                _group.alpha = 0;
            }
        }
    }
}
