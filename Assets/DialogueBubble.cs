using UnityEngine;
using UnityEngine.UI;

public class DialogueBubble : MonoBehaviour
{
    [SerializeField] private Canvas canvas;          // Reference to the Canvas
    [SerializeField] private RectTransform bubbleUI; // RectTransform of the bubble UI
    [SerializeField] private Transform character;    // Transform of the character
    [SerializeField] private Vector2 offset;         // Offset in screen space (e.g., above the character)

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (character != null && bubbleUI != null)
        {
            UpdateBubblePosition();
        }
    }

    private void UpdateBubblePosition()
    {
        // Convert the character's world position to screen space
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(character.position);

        // Check if the character is in front of the camera
        if (screenPosition.z > 0)
        {
            // Apply the offset to the screen position
            Vector2 adjustedPosition = screenPosition + (Vector3)offset;

            // Convert screen position to Canvas space (for non-World Space canvases)
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.GetComponent<RectTransform>(),
                adjustedPosition,
                canvas.worldCamera,
                out Vector2 canvasPosition);

            // Update the bubble position
            bubbleUI.anchoredPosition = canvasPosition;
        }
        else
        {
            // Hide the bubble if the character is behind the camera
            bubbleUI.gameObject.SetActive(false);
        }
    }
}
