using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] Animator fadeAnim;

    public static SceneController instance;

    AudioSource[] _audioSources;

    TallyCountManager _tally;
    OptionValueHandler _options;

    private void Awake()
    {
        instance = this;

        _audioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
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

    public void FadeAllAudio()
    {
        foreach (AudioSource a in _audioSources)
        {
            if (a.GetComponent<Animator>() != null)
            {
                a.GetComponent<Animator>().enabled = false;
            }
            StartCoroutine(StartFade(a, 1.5f, 0));
        }
    }

    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

    public void MainMenu_Play()
    {
        StartCoroutine(menuPlayFade());

        if(_audioSources.Length > 0)
        {
            FadeAllAudio();
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
            FadeAllAudio();
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
            FadeAllAudio();
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
            FadeAllAudio();
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
            FadeAllAudio();
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
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(1);
    }

    IEnumerator nextSceneFade()
    {
        fadeAnim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator returnMenuFade()
    {
        fadeAnim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }
    IEnumerator reloadSceneFade()
    {
        fadeAnim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion
}
