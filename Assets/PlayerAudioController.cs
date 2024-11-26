using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    public static PlayerAudioController instance;

    [Header("Audio Sources")]
    [SerializeField] AudioSource _sourceMain;
    [SerializeField] AudioSource _sourceJump;
    [SerializeField] AudioSource _sourcePulse;

    [Header("SFX")]
    public List<AudioClip> sfx;
    [SerializeField] AudioClip stepClip;
    [SerializeField] AudioClip deathClip;
    [SerializeField] AudioClip jumpClip;
    [SerializeField] AudioClip pulseClip;
    [SerializeField] AudioClip wallSlideClip;

    [Header("Volume Control")]
    [SerializeField] float stepVol = 0.25f;
    [SerializeField] float sfxVol = 0.5f;
    [SerializeField] float deathVol = 0.1f;
    [SerializeField] float jumpVol = 0.075f;

    [Header("Random Pitch Range")]
    [SerializeField] float pulsePitchMin = 0.4f, pulsePitchMax = 1.25f;
    [SerializeField] float stepPitchMin = 0.5f, stepPitchMax = 1.35f;
    [SerializeField] float jumpPitchMin = 0.75f, jumpPitchMax = 1.35f;

    float defaultPitch_main;

    private void Awake()
    {
        instance = this;

        defaultPitch_main = _sourceMain.pitch;
    }

    public void playerDeath()
    {
        _sourceMain.volume = deathVol;
        _sourceMain.PlayOneShot(deathClip);
    }

    public void playerPulse()
    {
        _sourcePulse.pitch = Random.Range(pulsePitchMin, pulsePitchMax);
        _sourcePulse.PlayOneShot(pulseClip);
    }

    public void playerStep()
    {
        if (!PlayerDie.instance.hasDied)
        {
            _sourceMain.volume = stepVol;
            _sourceMain.pitch = Random.Range(stepPitchMin, stepPitchMax);

            _sourceMain.PlayOneShot(stepClip);
        }
    }

    public void playerJump()
    {
        _sourceJump.volume = jumpVol;
        _sourceJump.pitch = Random.Range(jumpPitchMin, jumpPitchMax);

        _sourceJump.PlayOneShot(jumpClip);
    }

    public void PlaySFX(int audioClipInt)
    {
        _sourceMain.volume = sfxVol;
        _sourceMain.pitch = Random.Range(jumpPitchMin, jumpPitchMax);
        _sourceMain.PlayOneShot(sfx[audioClipInt]);
    }
}
