using System.Collections;
using UnityEngine;

public class BugAudioController : MonoBehaviour
{
    AudioSource _source;

    [SerializeField] AudioClip bugDeath;
    [SerializeField] AudioClip hit;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    public void bugDeathSound()
    {
        StartCoroutine(slightDelay());
        _source.PlayOneShot(hit);
    }

    IEnumerator slightDelay()
    {
        yield return new WaitForSeconds(0.1f);
        _source.PlayOneShot(bugDeath);
    }
}
