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
            _tally = GameObject.FindAnyObjectByType<TallyCountManager>();
        }

        if(_options == null)
        {
            _options = GameObject.FindAnyObjectByType<OptionValueHandler>();
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

        TimeController.instance.StopTimer();

        if(_options != null)
        {
            Destroy(_options);
        }
        
        if(_tally != null)
        {
            Destroy(_tally);
        }
    }

    public void NextScene()
    {
        StartCoroutine(nextSceneFade());
        PlayerMovement.instance.StopMovement();

        TimeController.instance.StopTimer();
    }

    public void ReloadScene()
    {
        StartCoroutine(reloadSceneFade());
        PlayerMovement.instance.StopMovement();

        TimeController.instance.StopTimer();
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
