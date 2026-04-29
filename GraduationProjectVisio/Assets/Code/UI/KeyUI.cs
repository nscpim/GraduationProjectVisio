using UnityEngine;
using UnityEngine.UI;

public class KeyUI : MonoBehaviour
{
    public KeyCode key;
    public Image image;

    public Color normalColor = Color.white;
    public Color requiredColor = Color.yellow;
    public Color pressedColor = Color.green;
    public Color inCorrect = Color.red;
    public Color neutralColor = Color.white;

    private void Awake()
    {
        image = GetComponent<Image>();
        GameManager.GetManager<VisualKeyboardManager>().keys.Add(this);
    }
    public void SetNormal()
    {
        image.color = normalColor;
    }

    public void SetRequired()
    {
        image.color = requiredColor;
    }

    public void SetPressed()
    {
        image.color = pressedColor;
    }

    public void SetIncorrect()
    {
        image.color = inCorrect;
    }
    public void SetDefault()
    {
        image.color = neutralColor;
    }
}
