using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventNode : MonoBehaviour
{
    public TextAsset nodeJSON;

    public TMPro.TextMeshProUGUI eventName;
    public TMPro.TextMeshProUGUI eventText;

    public UnityEngine.UI.Button[] eventChoices = new UnityEngine.UI.Button[3];

    private EventManager _eventManager;

    private GameManager _gameManager;

    [System.Serializable]   
    public class EventInfo
    {
        public string Name;
        public string Text;
        public EventChoice[] Choices;
    }
    [System.Serializable]
    public class EventChoice
    {
        public string ChoiceText;

        public int RedChange;
        public int BlueChange;
        public int GreenChange;

        public int NextEvent;
        public int NextNode;
    }

    public EventInfo eventInfo = new EventInfo();
    // Start is called before the first frame update
   
    void LoadEventInfo(TextAsset JSONFile)
    {
        _eventManager = FindObjectOfType<EventManager>();
        eventInfo = JsonUtility.FromJson<EventInfo>(JSONFile.text);
        for (int i = 0; i < eventChoices.Length; ++i)
        {
            eventChoices[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < eventInfo.Choices.Length; i++)
        {
            eventChoices[i].gameObject.SetActive(true);
        }
    }
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        LoadEventInfo(nodeJSON);
    }

    // Update is called once per frame
    void Update()
    {
        eventName.text = eventInfo.Name;
        eventText.text = eventInfo.Text;
        for (int i = 0; i < eventInfo.Choices.Length; i++)
        {
            eventChoices[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = eventInfo.Choices[i].ChoiceText;
        }
    }

    public void ChooseOption(int option)
    {
        int nextEvent = eventInfo.Choices[option].NextEvent;
        string nextEventFilename = "event" + nextEvent + ".json";
        int nextNode = eventInfo.Choices[option].NextNode;

        TextAsset eventJSON = _eventManager.jsonFiles[nextEvent];

        LoadEventInfo(eventJSON);

        if (nextNode == -1)
        {
            // Next event pops up.
        }
        else
        {
            // Disable Event window
            _gameManager.MoveCaravan(nextNode); // This sets the EventNode gameObject as false!
            // Move caravan to the next node
        }
    }
}
