using System.Collections;
using TMPro;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public GameObject[] RequiredGameObjects;

    public TextMeshProUGUI endText;

    private TeamManager _teamManager;

    private Teams phase = Teams.A;

    private Character[] _teamA;

    private Character[] _teamB;

    public Character[][] _teams = new Character[2][];

    public GameManager gameManager;

    // TODO change playSpeed
    private float playSpeed = 1.0f;

    private bool win = false;

    private void Awake()
    {
        _teamManager = GetComponent<TeamManager>();
    }

    private void OnEnable()
    {
        for (int i = 0; i < RequiredGameObjects.Length; i++)
            RequiredGameObjects[i].SetActive(true);

        Debug.Log("GameManager Enable");

        _teamA = _teamManager.GetTeamWithEnum(Teams.A);

        _teamB = _teamManager.GetTeamWithEnum(Teams.B);

        _teams[0] = _teamA;

        _teams[1] = _teamB;

        StartCoroutine(AutoPlay());
    }

    private void OnDisable()
    {
        for (int i = 0; i < RequiredGameObjects.Length; i++)
            RequiredGameObjects[i].SetActive(false);

        endText.text = "";

        StopAllCoroutines();
    }

    private IEnumerator AutoPlay()
    {
        //yield return new WaitForSeconds(1.0f);

        while (_teamManager.membersA > 0 && _teamManager.membersB > 0)
        {
            foreach (var item in _teams[(int)phase])
            {
                if (!item.gameObject.activeSelf)
                    continue;

                for (int i = 0; i < item.actions; i++)
                {
                    if (item.gameObject.activeSelf)
                    {
                        item.behavior.DoAction();

                        yield return new WaitForSeconds(1.0f / playSpeed);
                    }               
                }

                Debug.Log("-------------------------------------------------------");
            }

            phase = (phase == Teams.A ? Teams.B : Teams.A);
            
            yield return null;
        }
    }

    public void GameOver(bool win)
    {
        StopAllCoroutines();

        this.win = win;

        if (win)
        {
            endText.text = "YOU WIN";
            Debug.Log("You Win!!!");
        }
        else
        {
            endText.text = "YOU LOSE";
            Debug.Log("You Lose!!!");
        }

        Invoke(nameof(DisableBattleManager), 3);
    }

    public void DisableBattleManager()
    {
        gameObject.SetActive(false);

        gameManager.EndBattle(win);
    }
}
