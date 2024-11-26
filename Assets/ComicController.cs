using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ComicController : MonoBehaviour
{
    [SerializeField] PlayerInput input;
    [SerializeField] Animator comicAnim;
    [SerializeField] AudioClip sfx;

    AudioSource _source;

    // Start next scene event
    [SerializeField] UnityEvent _event;

    bool canClick = true;
    int slideCount = 0;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
        NextSlide();
    }

    public void NextSlide_Button(InputAction.CallbackContext context)
    {
        NextSlide();
    }

    void NextSlide()
    {
        // If slide count is above the current number of slides, then stop counting and trying to call more animations
        // Or if the cooldown hasn't ended, then can't click
        if (!canClick) return;

        if (slideCount == 6)
        {
            _event.Invoke();

            canClick = false;
            return;
        }

        if(slideCount != 0)
        {
            // Play SFX
            _source.PlayOneShot(sfx);
        }

        // Add to slide count
        slideCount++;

        // Activate slide animation
        comicAnim.SetTrigger("Panel" + slideCount);

        // Cooldown for slide to animate
        StartCoroutine(slideCooldown());
    }

    IEnumerator slideCooldown()
    {
        canClick = false;
        yield return new WaitForSeconds(0.2f);
        canClick = true;
    }
}
