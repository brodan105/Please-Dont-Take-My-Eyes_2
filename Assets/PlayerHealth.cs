using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    public float playerHealth = 100f;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Awake()
    {
        instance = this;
    }
}
