using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] Animator fadeAnim;

    public static SceneController instance;

    TallyCountManager _tally;
    OptionValueHandler _options;

    private void Awake()
    {
        instance = this;
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
    public void MainMenu_Play()
    {
        StartCoroutine(menuPlayFade());
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
