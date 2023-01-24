using UnityEngine;

public class MapColor : MonoBehaviour
{
    private void Awake()
    {
        SpriteRenderer[] spr = GetComponentsInChildren<SpriteRenderer>();

        if (spr.Length <= 0)
        {
            Debug.LogWarning("No sprite renderer found!!!");
            return;
        }

        for (int i = 0; i < spr.Length; i++)
        {
            if (i % 2 == 0)
                spr[i].color = new Color(0.7f, 0.7f, 0.7f, 1.0f);
        }
    }
}
