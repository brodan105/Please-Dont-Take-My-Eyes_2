using UnityEngine;

public class PlaySFX : MonoBehaviour
{
    [SerializeField] AudioSource _source;

    [SerializeField] AudioClip _sfx;
    [SerializeField] AudioClip _hurt;
    [SerializeField] AudioClip _defeated;
    [SerializeField] AudioClip _hitByObject;
    public void PlaySFXOnce()
    {
        _source.volume = 0.5f;
        _source.PlayOneShot(_sfx);
    }

    public void PlayHurtSFX()
    {
        _source.volume = 0.25f;
        _source.PlayOneShot(_hurt);
        _source.PlayOneShot(_hitByObject);
    }

    public void PlayDefeated()
    {
        _source.volume = 0.35f;
        _source.PlayOneShot(_defeated);
    }
}
