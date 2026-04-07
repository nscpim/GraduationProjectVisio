using UnityEngine;

public class UIMagnifier : MonoBehaviour
{
    public RectTransform magnifierRoot;
    public RectTransform zoomedUI;
    public Canvas canvas;
    public float zoom = 2f;

    void Update()
    {
        Vector2 mousePos = Input.mousePosition;

        // Move magnifier
        magnifierRoot.position = mousePos;

        // Offset zoomed UI
        zoomedUI.localScale = Vector3.one * zoom;


        // Convert from screen to mouse
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, mousePos, null, out localPoint);

        // Apply the zoom
        zoomedUI.anchoredPosition = -localPoint * (zoom - 1f);
    }
}