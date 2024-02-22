using TMPro;
using UnityEngine;

public class SigilCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private int _index;
    
    void Update()
    {
        _text.SetText(Progress.Instance.Data.Sigils[_index].ToString());
    }
}
