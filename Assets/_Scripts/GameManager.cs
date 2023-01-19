using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CaravanBehavior caravan;
    public EventManager eventManager;
    public EventNode eventNode;

    public Transform[] nodePositions;

    public void MoveCaravan(int nodeNum)
    {
        eventNode.gameObject.SetActive(false);
        caravan.GoTo(nodePositions[nodeNum].position);
    }

    public void CaravanArrived()
    {
        eventNode.gameObject.SetActive(true);
    }
}
