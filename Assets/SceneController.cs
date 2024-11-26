using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] Animator fadeAnim;

    public static SceneController instance;

    private void Awake()
    {
        instance = this;
    }

    #region Functions
    public void MainMenu_Play()
    {
        StartCoroutine(menuPlayFade());
    }

    public void ReturnToMainMenu()
    {
        StartCoroutine(returnMenuFade());
    }

    public void NextScene()
    {
        StartCoroutine(nextSceneFade());
        PlayerMovement.instance.StopMovement();
    }

    public void ReloadScene()
    {
        StartCoroutine(reloadSceneFade());
        PlayerMovement.instance.StopMovement();
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
