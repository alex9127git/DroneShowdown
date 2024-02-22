using TMPro;
using UnityEngine;

public class SigilUpgradeButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private int _index;
    [SerializeField] private string currency;
    private int _level;
    
    private void Update()
    {
        _level = Progress.Instance.Data.SigilUpgradeLevels[_index];
        _text.SetText("Уровень " + _level + "\nСтоимость:\n" + currency + " x" + (_level + 1));
    }

    public void Purchase()
    {
        Progress.Instance.AddSigilUpgrade(_index);
    }
}
