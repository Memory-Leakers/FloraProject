using TMPro;
using UnityEngine;

public class InfoPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _characterName;

    [SerializeField]
    private TextMeshProUGUI _characterParameter;

    public void UpdateInfoPanel(Character character)
    {
        if (character)
        {
            _characterName.text = character.name;

            _characterParameter.text = character.ToString();

            return;
        }

        _characterName.text = "";

        _characterParameter.text = "";
    }
}
