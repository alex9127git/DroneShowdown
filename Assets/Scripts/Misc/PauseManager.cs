using TMPro;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private bool _paused;
    [SerializeField] private TMP_Text _pauseText;

    public static PauseManager Instance;

    public bool Paused { get { return _paused; } }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        _paused = false;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) SwitchState();
        _pauseText.gameObject.SetActive(_paused);
    }

    public void SwitchState()
    {
        _paused = !_paused;
    }
}
