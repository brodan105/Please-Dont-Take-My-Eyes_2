using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonAudioController : MonoBehaviour
{
    [SerializeField] AudioSource _source;
    [SerializeField] AudioClip _UISelectSFX;
    [SerializeField] AudioClip _UIPressedSFX;

    EventSystem _eventSys;

    GameObject lastSelected;
    GameObject currentlySelected;

    bool playedSFX = true;
    bool canPlay = true;
    bool canPressOnce;

    private void Awake()
    {
        _eventSys = EventSystem.current;
        lastSelected = _eventSys.firstSelectedGameObject;
    }

    private void LateUpdate()
    {
        currentlySelected = _eventSys.currentSelectedGameObject;

        if (!canPlay) { return; }
        if(currentlySelected != lastSelected)
        {
            lastSelected = currentlySelected;

            playedSFX = false;

            if (!playedSFX)
            {
                PlayUISFX();
                playedSFX = true;
            }
            else return;
        }
    }

    public void DisableTemp()
    {
        StartCoroutine(DisableTempTimer());
    }

    IEnumerator DisableTempTimer()
    {
        canPlay = false;
        yield return new WaitForSeconds(0.1f);
        canPlay = true;
    }

    public void PlayUIPressedSFX()
    {
        if (canPressOnce)
        {
            _source.PlayOneShot(_UIPressedSFX);
            Destroy(this);
            _eventSys.enabled = false;
        }
        else
        {
            _source.PlayOneShot(_UIPressedSFX);
        }
    }

    public void canPressOnceSet(bool set)
    {
        canPressOnce = set;
    }

    void PlayUISFX()
    {
        _source.PlayOneShot(_UISelectSFX);
    }
}
