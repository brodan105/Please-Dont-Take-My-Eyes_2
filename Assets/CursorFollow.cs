using UnityEngine;

public class CursorFollow : MonoBehaviour
{
    public static CursorFollow instance;

    [SerializeField] GameObject cursor;
    [SerializeField] RectTransform canvasRect;

    private void Start()
    {
        instance = this;
        DisableCursor();

        Cursor.visible = false;
    }

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z; // Distance from the camera
        transform.position = Camera.main.ScreenToWorldPoint(mousePosition);


        if (cursor.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void EnableCursor()
    {
        cursor.SetActive(true);
    }

    public void DisableCursor()
    {
        cursor.SetActive(false);
    }
}
