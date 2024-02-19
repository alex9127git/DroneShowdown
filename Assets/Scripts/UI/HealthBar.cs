using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Image _filled;

    void Update()
    {
        _filled.transform.localScale = new Vector3(Mathf.Clamp((float) _player.HP / _player.MaxHP, 0f, 1f), 1f, 1f);
    }
}
