using TMPro;
using UnityEngine;

public class TokenCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _tokenText;

    private void Update()
    {
        _tokenText.text = Progress.Instance.Data.Tokens.ToString();
    }
}
