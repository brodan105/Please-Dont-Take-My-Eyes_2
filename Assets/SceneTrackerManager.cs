using UnityEngine;

public class SceneTrackerManager : MonoBehaviour
{

    public static SceneTrackerManager instance;

    public int previousScene = 0;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        instance = this;
    }
}
