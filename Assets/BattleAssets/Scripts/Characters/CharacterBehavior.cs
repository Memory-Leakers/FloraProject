using System;
using System.Linq;
using UnityEngine;

public class CharacterBehavior : MonoBehaviour
{
    private Character _characterInfo;

    private TeamManager _teamManager;

    private MapManager _mapManager;

    private DebugManager _debugManager;

    public void InitByMySelf()
    {
        _characterInfo = GetComponent<Character>();

        GameObject gameManager = _characterInfo._battleManager.gameObject;

        if (!gameManager)
        {
            Debug.LogWarning("Cannot found GameManager!!!");
            return;
        }

        _teamManager = gameManager.GetComponent<TeamManager>();

        _mapManager = gameManager.GetComponent<MapManager>();

        _debugManager = gameManager.GetComponent<DebugManager>();
    }

    public void DoAction()
    {
        Character _target = _teamManager.GetNeariestEnemic(_characterInfo, out int distance);

        if (!_target)
            return;

        for (int j = 0; j < _characterInfo.movement; j++)
        {
            int manhattanDistance = Mathf.Abs(_characterInfo.TilePos.x - _target.TilePos.x) +
                Mathf.Abs(_characterInfo.TilePos.y - _target.TilePos.y);

            if (manhattanDistance >= _characterInfo.attackRange.x && distance <= _characterInfo.attackRange.y)
            {
                //Attack
                _debugManager.AddText(gameObject.name + " Attack " + _target.name);
                Debug.Log(gameObject.name + " Attack " + _target.name);

                float ranNum = UnityEngine.Random.Range(0.0f, 1.0f);

                if (ranNum <= _characterInfo.hitRate)
                {
                    _target.GetHit(_characterInfo);
                }
                else
                {
                    _debugManager.AddText(gameObject.name + " Attack Miss");
                    Debug.Log(gameObject.name + " Attack Miss");
                }            
                return;
            }

            //Try to move to attackRange
            Vector2Int distanceVec = -(_characterInfo.TilePos - _target.TilePos);

            int moveRange = 0;

            switch (_characterInfo.role)
            {
                case Roles.Warrior:
                    moveRange = 0;
                    break;
                case Roles.Controller:
                case Roles.Shooter:
                    moveRange = 1;
                    break;
            }

            Vector2Int[] rand = { Vector2Int.zero, Vector2Int.zero };

            if (distanceVec.y > moveRange)
                rand[0] = Vector2Int.up;
            else if (distanceVec.y < -moveRange)
                rand[0] = Vector2Int.down;

            if (distanceVec.x > moveRange)
                rand[1] = Vector2Int.right;
            else if (distanceVec.x < -moveRange)
                rand[1] = Vector2Int.left;

            rand = rand.OrderBy(x => Guid.NewGuid()).ToArray();

            bool moved = false;

            for (int i = 0; i < rand.Length; i++)
            {
                if (MoveInTile(rand[i]))
                {
                    moved = true;
                    break;
                }               
            }

            if (moved)
                continue;

            Vector2Int[] rand2 = { Vector2Int.up, Vector2Int.down, Vector2Int.right, Vector2Int.left };
            rand2 = rand2.OrderBy(x => Guid.NewGuid()).ToArray();

            for (int i = 0; i < rand2.Length; i++)
            {
                if (MoveInTile(rand2[i]))
                {
                    moved = true;
                    break;
                }
            }
        }
    }

    private bool MoveInTile(Vector2Int dir)
    {
        Vector2Int nextStep = _characterInfo.TilePos + dir;

        if (nextStep.x > 8 || nextStep.y > 8 || nextStep.x < 0 || nextStep.y < 0)
            return false;

        if (_mapManager.mapArray[nextStep.x, nextStep.y] != 0)
            return false;

        _characterInfo.TilePos = nextStep;

        if (dir.x == 1)
        {
            _debugManager.AddText(gameObject.name + " Move Right");
            Debug.Log(gameObject.name + " Move Right");
        }
        if (dir.x == -1)
        {
            _debugManager.AddText(gameObject.name + " Move Left");
            Debug.Log(gameObject.name + " Move Left");
        }
        if (dir.y == 1)
        {
            _debugManager.AddText(gameObject.name + " Move Up");
            Debug.Log(gameObject.name + " Move Up");
        }
        if (dir.y == -1)
        {
            _debugManager.AddText(gameObject.name + " Move Down");
            Debug.Log(gameObject.name + " Move Down");
        }

        return true;
    }

    private void AttackEnemy(Character _target)
    {

    }
}
