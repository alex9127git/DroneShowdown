using UnityEngine;

public class BulletParent : MonoBehaviour
{
    public static BulletParent Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
}
