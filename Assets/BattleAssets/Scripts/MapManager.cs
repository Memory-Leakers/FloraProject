using System.Linq;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public int[,] mapArray = new int[9, 9];

    void Start()
    {
        var all = mapArray.Cast<int>();

        all.ToList().ForEach(x => x = 0);
    }

    private void OnDisable()
    {
        var all = mapArray.Cast<int>();

        all.ToList().ForEach(x => x = 0);
    }
}
