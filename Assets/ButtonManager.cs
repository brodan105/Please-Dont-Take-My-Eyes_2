using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] EventSystem _event;

    GameObject currentSelected;
    GameObject lastSelected;

    private void Awake()
    {
        currentSelected = _event.currentSelectedGameObject;
        lastSelected = currentSelected;
    }

    private void Update()
    {
        if(_event.currentSelectedGameObject == null && lastSelected != null)
        {
            _event.SetSelectedGameObject(lastSelected);
        }

        
        if(currentSelected != _event.currentSelectedGameObject)
        {
            lastSelected = currentSelected;
            currentSelected = _event.currentSelectedGameObject;
        }
        
    }
}
