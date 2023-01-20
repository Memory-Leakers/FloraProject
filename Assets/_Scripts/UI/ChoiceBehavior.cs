using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceBehavior : MonoBehaviour
{
    private EventNode eventNode;

    public GameObject informationPanel;
    public TMPro.TextMeshProUGUI text;

    public int choiceNum;
    bool mouseOver = false;

    private void Start()
    {
        eventNode = FindObjectOfType<EventNode>();
    }

    private void Update()
    {
        if (mouseOver)
        {
            Vector3 pos = Input.mousePosition + new Vector3(150, 40, 0);
            informationPanel.transform.position = pos;
            text.text = eventNode.eventInfo.Choices[choiceNum].InformationText;
        }

    }

    public void OnMouseEnter()
    {
        informationPanel.SetActive(true);
        mouseOver = true;
    }

    public void OnMouseExit()
    {
        Deactivate();
    }

    public void Deactivate()
    {
        informationPanel.SetActive(false);
        mouseOver = false;
    }
}
