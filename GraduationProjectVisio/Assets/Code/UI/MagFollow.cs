using UnityEngine;

public class MagFollow : MonoBehaviour
{
    public Camera mainCamera;
    public Camera magnifierCamera;
    public float distance = 2f;
    public RectTransform magnifierRoot;

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        Vector3 worldPos = mainCamera.ScreenToWorldPoint(
            new Vector3(mousePos.x, mousePos.y, distance)
        );

        magnifierCamera.transform.position = new Vector3(
            worldPos.x,
            worldPos.y,
            magnifierCamera.transform.position.z
        );

        magnifierRoot.position = mousePos;
    }
}
