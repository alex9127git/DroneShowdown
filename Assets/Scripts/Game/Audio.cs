using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioSource MenuSelect;
    public AudioSource Shoot;
    public AudioSource Explosion;
    public AudioSource TokenPickUp;
    public AudioSource Hit;
    public AudioSource UpgradeBought;
    public AudioSource PlayerExplosion;

    public static Audio Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
}
