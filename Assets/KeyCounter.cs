using UnityEngine;
using TMPro;

public class KeyCounter : MonoBehaviour
{
    public static KeyCounter instance;

    [Header("References")]
    [SerializeField] GameObject keyCountUI;
    [SerializeField] TMP_Text keyCountUI_Num;

    public int keyCount;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        keyCountUI_Num.text = keyCount.ToString();
    }

    public void AddKey()
    {
        keyCount++;

        if (keyCount > 0)
        {
            keyCountUI.SetActive(true);
        }
    }

    public void ResetKeys()
    {
        keyCount = 0;

        keyCountUI.SetActive(false);
    }
}
