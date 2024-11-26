using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    [SerializeField] int sceneBuildIndexNum;

    [SerializeField] Animator fadePanel;

    bool triggerEntered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggerEntered) return;

        if(collision.tag == "Player")
        {
            triggerEntered = true;

            // Set scene as previous scene to reference in next scene
            SceneTrackerManager.instance.previousScene = SceneManager.GetActiveScene().buildIndex;

            // fade screen to black
            fadePanel.SetTrigger("FadeOut");

            // start coroutine to teleport player (after screen is black)
            StartCoroutine(nextScene());

            // stop player movement
            PlayerMovement.instance.StopMovement();
        }
    }

    IEnumerator nextScene()
    {
        yield return new WaitForSeconds(3);

        SceneManager.LoadScene(sceneBuildIndexNum);
    }
}
