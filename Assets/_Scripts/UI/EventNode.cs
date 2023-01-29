using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text;

public class EventNode : MonoBehaviour
{
    public TextAsset nodeJSON;

    public TMPro.TextMeshProUGUI eventName;
    public TMPro.TextMeshProUGUI eventText;
    public ScrollRect scrollRect;

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
        public string InformationText;

        public int RedChange;
        public int BlueChange;
        public int GreenChange;

        public int NextEvent;
        public int NextNode = -1;
        // Battle parameters
        public int BattleID = -1;
        public int WinBattleEvent;
        public int LoseBattleEvent;
        // Restart
        public int Restart;
    }

    public EventInfo eventInfo = new EventInfo();
    // Start is called before the first frame update

    public void LoadEventInfo(TextAsset JSONFile)
    {
        eventInfo = JsonUtility.FromJson<EventInfo>(JSONFile.text);

        byte[] encodedBytes = Encoding.UTF8.GetBytes(eventInfo.Text);
        string utf8String = Encoding.UTF8.GetString(encodedBytes);
        eventInfo.Text = utf8String;

        for (int i = 0; i < eventChoices.Length; ++i)
        {
            eventChoices[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < eventInfo.Choices.Length; i++)
        {
            eventChoices[i].gameObject.SetActive(true);
        }
        scrollRect.verticalNormalizedPosition = 0;
    }
    void Start()
    {
        _eventManager = FindObjectOfType<EventManager>();
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
        // If Restart is not equal 0, we Restart the game.
        if (eventInfo.Choices[option].Restart == 1)
        {
            SceneManager.LoadScene(0);
            return;
        }

        // If the option is a battle option, it is treated specially, because the event doesnt change right away.
        int eventBattle = eventInfo.Choices[option].BattleID;
        
        if (eventBattle != -1)
        {
            _gameManager.StartBattle(eventBattle, eventInfo.Choices[option].WinBattleEvent, eventInfo.Choices[option].LoseBattleEvent);
            return;
        }

        int nextEvent = eventInfo.Choices[option].NextEvent;
        string nextEventFilename = "event" + nextEvent + ".json";
        int nextNode = eventInfo.Choices[option].NextNode;
       
        TextAsset eventJSON = _eventManager.jsonFiles[nextEvent];

        LoadEventInfo(eventJSON);

        // If we are moving the caravan after this choice...
        if (nextNode != -1)
        {
            // Disable Event window & move caravan to the given node.
            _gameManager.MoveCaravan(nextNode); // This sets the EventNode gameObject as false!
        }
    }
}
