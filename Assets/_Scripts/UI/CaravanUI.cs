using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaravanUI : MonoBehaviour
{
    public TMPro.TextMeshProUGUI currentTime;
    public TMPro.TextMeshProUGUI currentDate;

    private CaravanManager _caravan;

    private void Start()
    {
        _caravan = FindObjectOfType<CaravanManager>();
    }

    private void Update()
    {
        int minutes = (int)_caravan.gametimeinminutes;
        int hours = (int)_caravan.gametimeinhours;

        int day = _caravan.gameDay;
        int month = _caravan.gameMonth;

        currentTime.text = (hours < 10 ? "0" + hours : hours) + ":" + (minutes < 10 ? "0" + minutes: minutes);
        currentDate.text = (month < 10 ? "0" + month : month) + "/" + (day < 10 ? "0" + day : day);
    }

}
