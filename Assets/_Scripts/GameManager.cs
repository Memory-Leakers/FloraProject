using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleInfo
{
    public CharacterParam[] characters;
}

public class GameManager : MonoBehaviour
{
    public CaravanBehavior caravan;
    public EventManager eventManager;
    public EventNode eventNode;
    public CaravanManager caravanManager;

    public TeamManager teamManager;
    public BattleManager BattleManager;

    public Transform[] nodePositions;

    public bool willWinBattle = true; // For debug purposes!

    public List<TextAsset> jsonBattles;

    int nextWinEvent;
    int nextLoseEvent;

    private void Start()
    {
        eventNode.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        eventNode.gameObject.SetActive(true);
    }

    public void MoveCaravan(int nodeNum)
    {
        eventNode.gameObject.SetActive(false);
        caravan.GoTo(nodePositions[nodeNum].position);
        caravanManager.PlayGameTime();
    }

    public void CaravanArrived()
    {
        eventNode.gameObject.SetActive(true);
        caravanManager.StopGameTime();
    }

    public void StartBattle(int battleID, int winEvent, int loseEvent)
    {
        // Do the battle with the given battle id
        nextWinEvent = winEvent;
        nextLoseEvent = loseEvent;

        // Leo el JSON
        BattleInfo battleInfo = JsonUtility.FromJson<BattleInfo>(jsonBattles[battleID].text);

        // Hago AddCharacter
        for (int i = 0; i < battleInfo.characters.Length; i++)
        {
            teamManager.AddCharacter(battleInfo.characters[i]);
        }

        // Activo el GameManager
        BattleManager.gameObject.SetActive(true);

        eventNode.gameObject.SetActive(false);
    }

    public void EndBattle(bool win)
    {
        TextAsset eventJSON;
        // If win
        if (win)
            eventJSON = eventManager.jsonFiles[nextWinEvent];
        else
            eventJSON = eventManager.jsonFiles[nextLoseEvent];

        eventNode.LoadEventInfo(eventJSON);

        // If we want to move the caravan before the next event after a battle, this could should be changed.
        eventNode.gameObject.SetActive(true);
    }
}
