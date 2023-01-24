using UnityEngine;

public enum Teams
{
    A,
    B
}

public class TeamManager : MonoBehaviour
{
    public GameObject teamAParent;
    public GameObject teamBParent;

    public GameObject characterPrefab;

    private Character[] _teamA;
    private Character[] _teamB;

    public int membersA = 0;
    public int membersB = 0;

    private bool _inited = false;

    private void Awake()
    {
        Debug.Log("TeamManager Awake");
        InitTeam();
    }

    private void OnEnable()
    {
        InitTeam();
    }

    private void OnDisable()
    {
        for (int i = 0; i < _teamA.Length; i++)
        {
            Destroy(_teamA[i].gameObject);
        }
        for (int i = 0; i < _teamB.Length; i++)
        {
            Destroy(_teamB[i].gameObject);
        }

        _inited = false;
    }

    private void InitTeam()
    {
        if (_inited)
            return;

        _inited = true;

        _teamA = teamAParent.GetComponentsInChildren<Character>();
        _teamB = teamBParent.GetComponentsInChildren<Character>();

        if (_teamA.Length <= 0 || _teamB.Length <= 0)
        {
            Debug.LogWarning("Some team start without member!!!");
            return;
        }

        membersA = _teamA.Length;
        membersB = _teamB.Length;
    }

    private void AddCharacter(CharacterParam param)
    {
        Instantiate(characterPrefab);

        Character character = characterPrefab.GetComponent<Character>();

        character.team = (Teams)param.team;     
        character.attackRange = new Vector2Int(param.attackRange1, param.attackRange2);
        character.healt = param.healt;
        character.attack = param.attack;
        character.defense = param.defense;
        character.movement = param.movement;
        character.hitRate = param.hitRate;
        character.role = (Roles)param.role;

        characterPrefab.transform.SetParent(character.team == Teams.A ? teamAParent.transform : teamBParent.transform);

        character.TilePos = new Vector2Int(param.tilePosX, param.tilePosY);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="teamEnum"> your teamEnum </param>
    /// <returns></returns>
    public Character[] GetEnemicTeam(Teams teamEnum)
    {
        switch (teamEnum)
        {
            case Teams.A:
                return _teamB;
            case Teams.B:
                return _teamA;
            default:
                Debug.LogWarning("Team not exist!");
                return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="teamEnum"> team enum you want to get</param>
    /// <returns></returns>
    public Character[] GetTeamWithEnum(Teams teamEnum)
    {
        switch (teamEnum)
        {
            case Teams.A:
                return _teamA;
            case Teams.B:
                return _teamB;
            default:
                Debug.LogWarning("Team not exist!");
                return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tag"> team tag you want to get</param>
    /// <returns></returns>
    public Character[] GetTeamWithTag(string tag)
    {
        if (tag == "TeamA")
            return _teamA;
        if (tag == "TeamB")
            return _teamB;

        Debug.LogWarning("Team not exist!");
        return null;
    }

    public Character GetNeariestEnemic(Character character, out int manhattanDistance)
    {
        manhattanDistance = 0;

        if (membersA <= 0 || membersB <= 0)
            return null;

        Vector2Int characterPos = character.TilePos;

        int distance = 9999;

        int enemyIndex = 0;

        Character[] anotherTeam;

        anotherTeam = character.team == Teams.A ? _teamB : _teamA;

        for (int i = 0; i < anotherTeam.Length; i++)
        {
            if (!anotherTeam[i].gameObject.activeSelf)
                continue;

            manhattanDistance = Mathf.Abs(characterPos.x - anotherTeam[i].TilePos.x) +
                Mathf.Abs(characterPos.y - anotherTeam[i].TilePos.y);

            if (manhattanDistance < distance)
            {
                distance = manhattanDistance;
                enemyIndex = i;
            }
        }

        manhattanDistance = distance;

        return anotherTeam[enemyIndex];
    }
}
