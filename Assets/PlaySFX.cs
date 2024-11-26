using UnityEngine;

public class PlaySFX : MonoBehaviour
{
    [SerializeField] AudioSource _source;

    [SerializeField] AudioClip _sfx;
    public void PlaySFXOnce()
    {
        _source.PlayOneShot(_sfx);
    }
}
