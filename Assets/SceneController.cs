using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] Animator fadeAnim;

    [SerializeField] float delayTime = 3f;

    public static SceneController instance;

    AudioSource[] _audioSources;

    TallyCountManager _tally;
    OptionValueHandler _options;

    private void Awake()
    {
        instance = this;

        _audioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);

        FadeInAllAudio();
    }

    private void Update()
    {
        if(_tally == null)
        {
            _tally = FindAnyObjectByType<TallyCountManager>();
        }

        if(_options == null)
        {
            _options = FindAnyObjectByType<OptionValueHandler>();
        }
    }

    #region Functions

    public void FadeOutAllAudio()
    {
        foreach (AudioSource a in _audioSources)
        {
            if (a.GetComponent<Animator>() != null)
            {
                a.GetComponent<Animator>().enabled = false;
            }
            float startVolume = a.volume;

            StartCoroutine(StartFade(a, 1.5f, startVolume, 0));
        }
    }

    public void FadeInAllAudio()
    {
        foreach(AudioSource a in _audioSources)
        {
            if(a.GetComponent<Animator>() != null)
            {
                a.GetComponent<Animator>().enabled = false;
            }

            float targetVolume = a.volume;

            StartCoroutine(StartFade(a, 1.5f, 0, targetVolume));
        }
    }

    public static IEnumerator StartFade(AudioSource audioSource, float duration, float startVolume, float targetVolume)
    {
        float currentTime = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

    public void MainMenu_Play()
    {
        StartCoroutine(menuPlayFade());

        if(_audioSources.Length > 0)
        {
            FadeOutAllAudio();
        }
    }

    public void ReturnToMainMenu()
    {
        StartCoroutine(returnMenuFade());

        if(TimeController.instance != null)
        {
            TimeController.instance.StopTimer();
        }
        
        if(_tally != null)
        {
            Destroy(_tally.gameObject);
        }

        if (_audioSources.Length > 0)
        {
            FadeOutAllAudio();
        }
    }

    public void NextScene()
    {
        StartCoroutine(nextSceneFade());
        if(PlayerMovement.instance != null)
        {
            PlayerMovement.instance.StopMovement();
        }

        if (TimeController.instance != null)
        {
            TimeController.instance.StopTimer();
        }

        if (_audioSources.Length > 0)
        {
            FadeOutAllAudio();
        }
    }

    public void ReloadScene()
    {
        StartCoroutine(reloadSceneFade());
        if (PlayerMovement.instance != null)
        {
            PlayerMovement.instance.StopMovement();
        }

        if (TimeController.instance != null)
        {
            TimeController.instance.ReloadTimer();
        }

        _tally.RestartLevelCounts();

        if (_audioSources.Length > 0)
        {
            FadeOutAllAudio();
        }
    }

    public void ReloadBossScene()
    {
        StartCoroutine(reloadSceneFade());
        if (PlayerMovement.instance != null)
        {
            PlayerMovement.instance.StopMovement();
        }

        if(TimeController.instance != null)
        {
            TimeController.instance.ReloadTimer();
        }

        if (_audioSources.Length > 0)
        {
            FadeOutAllAudio();
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
    #endregion

    #region Timers
    IEnumerator menuPlayFade()
    {
        fadeAnim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(1);
    }

    IEnumerator nextSceneFade()
    {
        fadeAnim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator returnMenuFade()
    {
        fadeAnim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(1);
    }
    IEnumerator reloadSceneFade()
    {
        fadeAnim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion
}
