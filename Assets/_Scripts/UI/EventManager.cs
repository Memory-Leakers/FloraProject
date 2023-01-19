using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public List<TextAsset> jsonFiles;
    public EventNode eventNodeScript;

    public void ShowEvent(bool show)
    {
        eventNodeScript.gameObject.SetActive(show);
    }
}
