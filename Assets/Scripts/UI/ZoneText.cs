using TMPro;
using UnityEngine;

public class ZoneText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Animator _animator;

    public void Animate(string text)
    {
        _text.SetText(text);
        _animator.SetTrigger("Animate");
    }
}
