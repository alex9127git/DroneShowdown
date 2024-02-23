using System.Collections;
using TMPro;
using UnityEngine;

public class SigilText : MonoBehaviour
{
    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    public void Show()
    {
        StartCoroutine(ShowProcess());
    }

    IEnumerator ShowProcess()
    {
        _text.color = Color.white;
        yield return new WaitForSeconds(2f);
        _text.color = new Color(1, 1, 1, 0);
    }
}
