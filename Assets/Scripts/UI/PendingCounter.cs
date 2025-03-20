using TMPro;
using UnityEngine;

public class PendingCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _tokenText;

    private void Update()
    {
        Player p = FindObjectOfType<Player>();
        _tokenText.text = p != null ? p.EarnedTokens.ToString() : "0";
    }
}
