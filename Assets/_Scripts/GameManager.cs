using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CaravanBehavior caravan;
    public EventManager eventManager;
    public EventNode eventNode;
    public CaravanManager caravanManager;

    public Transform[] nodePositions;

    public bool willWinBattle = true; // For debug purposes!

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
        // Hago AddCharacter
        // Activo el GameManager


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
