using UnityEngine;

public class TokenManager : MonoBehaviour
{
    [SerializeField] private Token[] _tokenPrefabs;
    [SerializeField] private int[] _tokenValues;

    public static TokenManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SpawnTokens(int amount, Vector3 position)
    {
        int left = amount;
        int value;
        Token prefab;
        for (int prefab_index = 0; prefab_index < _tokenPrefabs.Length; prefab_index++)
        {
            value = _tokenValues[prefab_index];
            prefab = _tokenPrefabs[prefab_index];
            for (; left >= value; left -= value)
            {
                Token t = Instantiate(prefab, position, Quaternion.identity);
                t.Init(value);
                t.transform.parent = transform;
            }
        }
    }
}
