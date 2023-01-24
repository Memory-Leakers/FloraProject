using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugManager : MonoBehaviour
{
    public RectTransform scrollRect;
    public Scrollbar scroll;
    [HideInInspector] public TextMeshProUGUI debugText;

    private void Start()
    {
        debugText = scrollRect.GetComponent<TextMeshProUGUI>();

        if (!debugText)
            Debug.LogWarning("Cannot found debugText");
    }

    private void Update()
    {
        //scroll.value = 0;
    }

    private void OnDisable()
    {
        scrollRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 16);
        debugText.text = "";
    }

    public void AddText(string message)
    {
        debugText.text += message + "\n";

        scrollRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, scrollRect.rect.height + 16);

        //scroll.value = 0.0f;
    }
}
