using UnityEngine;

public enum Roles
{
    Controller,
    Warrior,
    Shooter,
}

public class Character : MonoBehaviour
{
    public Roles role = Roles.Controller;
    private int level = 1;
    private Vector2Int _tilePosition = Vector2Int.zero;
    private Vector2Int _worldPos = Vector2Int.zero;

    [HideInInspector] public Vector2Int attackRange = new(1, 1);
    [HideInInspector] public CharacterBehavior behavior;
    [HideInInspector] public int actions = 2;
    [SerializeField] public int healt = 1;
    [SerializeField] public int attack = 1;
    [SerializeField] public int defense = 1;
    public int movement = 1;
    public float hitRate = 1.0f;

    public Teams team = Teams.A;

    public  BattleManager _battleManager;
    private InfoPanel _infoPanel;
    private MapManager _mapManager;
    private TeamManager _teamManager;
    private DebugManager _debugManager;

    public void InitByMySelf(BattleManager battleManager)
    {
        switch (role)
        {
            case Roles.Controller:
                attackRange = new(1, 2);
                break;
            case Roles.Shooter:
                attackRange = new(2, 2);
                break;
            case Roles.Warrior:
                attackRange = new(1, 1);
                break;
        }

        _battleManager = battleManager;

        if (_battleManager == null)
        {
            Debug.Log("Cannot found battleManager!!!");
            return;
        }

        _infoPanel = _battleManager.GetComponent<InfoPanel>();
        _mapManager = _battleManager.GetComponent<MapManager>();
        _teamManager = _battleManager.GetComponent<TeamManager>();
        _debugManager = _battleManager.GetComponent<DebugManager>();
        behavior = GetComponent<CharacterBehavior>();

        behavior.InitByMySelf();

        WorldPos = new Vector2Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt((int)transform.position.y));
    }

    public Vector2Int WorldPos
    {
        set
        {
            _mapManager.mapArray[_tilePosition.x, _tilePosition.y] = 0;
            _worldPos = value;
            _tilePosition = _worldPos + new Vector2Int(4, 4);
            transform.localPosition = new Vector3(_worldPos.x, _worldPos.y);
            _mapManager.mapArray[_tilePosition.x, _tilePosition.y] = (int)team + 1;
        }
        get
        {
            return _worldPos;
        }
    }

    public Vector2Int TilePos
    {
        set
        {
            _mapManager.mapArray[_tilePosition.x, _tilePosition.y] = 0;
            _tilePosition = value;
            _worldPos = _tilePosition - new Vector2Int(4, 4);
            transform.localPosition = new Vector3(_worldPos.x, _worldPos.y);
            _mapManager.mapArray[_tilePosition.x, _tilePosition.y] = (int)team + 1;
        }
        get
        {
            return _tilePosition;
        }
    }

    public void InitColor()
    {
        SpriteRenderer spr = GetComponent<SpriteRenderer>();

        spr.color = team == Teams.A ? spr.color : new Color(0.8f, 0.2f, 0.2f, 1);
    }

    public override string ToString()
    {
        return $"Role: {role}\n" +
            $"Lv: {level}\n" +
            $"Healt: {healt} \n" +
            $"Attack: {attack}\n" +
            $"Defense: {defense}\n" +
            $"Movement: {movement}\n" +
            $"Attack Range: {attackRange.x}~{attackRange.y}\n" +
            $"Accions: {actions}\n" +
            $"Hit ratio: {hitRate * 100}%";
    }

    public void GetHit(Character attacker, bool counterattack = false)
    {
        healt -= (attacker.attack - defense) > 1 ? (attacker.attack - defense) : 1;

        if (healt <= 0)
        {
            gameObject.SetActive(false);

            if (team == Teams.A)
            {
                if (--_teamManager.membersA <= 0)
                    _battleManager.GameOver(false);
            }
            else 
            {
                if (--_teamManager.membersB <= 0)
                    _battleManager.GameOver(true);
            }

            _debugManager.AddText(name + " is Dead ");
            Debug.Log(name + " is Dead ");
        }
        else if (!counterattack)
        {
            _debugManager.AddText(name + " Counterattack " + attacker.name);
            Debug.Log(name + " Attack " + attacker.name);

            float ranNum = UnityEngine.Random.Range(0.0f, 1.0f);

            if (ranNum <= hitRate)
            {
                attacker.GetHit(this, true);
            }
            else
            {
                _debugManager.AddText(gameObject.name + " Attack Miss");
                Debug.Log(gameObject.name + " Attack Miss");
            }
        }
    }

    private void OnMouseEnter()
    {
        //Debug.Log("MouseEnter");
        _infoPanel.UpdateInfoPanel(this);
    }

    private void OnMouseExit()
    {
        //Debug.Log("MouseExit");
        _infoPanel.UpdateInfoPanel(null);
    }
}

[System.Serializable]
public class CharacterParam
{
    public int role = 0;
    public int tilePosX;
    public int tilePosY;
    public int attackRange1 = 1;
    public int attackRange2 = 1;
    public int actions = 2;
    public int healt = 1;
    public int attack = 1;
    public int defense = 1;
    public int team = 0;
    public int movement = 1;
    public float hitRate = 1.0f;
    public string name = "";
}