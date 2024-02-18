using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyList _enemyList;
    [SerializeField] private Player _player;
    private const float _deltaDifficulty = 0.05f;
    private float _difficulty = 1f;
    private float _maxTimer = 2f;

    public static EnemySpawner Instance;

    public float Difficulty { get { return _difficulty; } }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        StartCoroutine(SpawningEnemies());
    }

    private void Update()
    {
        _difficulty += _deltaDifficulty * Time.deltaTime;
        _maxTimer = 2f - 0.00001f * _difficulty * _difficulty;
        _maxTimer = Mathf.Clamp(_maxTimer, 0.2f, 2f);
    }

    IEnumerator SpawningEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.2f, _maxTimer));
            Vector3 enemyPos = _player.transform.position;
            enemyPos.x += Random.Range(50f, 125f) * (Random.Range(0f, 1f) < 0.5f ? -1 : 1);
            enemyPos.y += Random.Range(50f, 125f) * (Random.Range(0f, 1f) < 0.5f ? -1 : 1);
            Enemy prefab = null;
            float difficultyFactor = Mathf.Clamp((Difficulty - 1) / 10 + 1, 0f, 2f);
            float distanceFactor = Mathf.Clamp((_player.transform.position.magnitude) / 2500 + 1, 0f, 2f);
            foreach (EnemyEntry entry in _enemyList.Enemies)
            {
                if (Random.Range(0f, 100f) < entry.Chance * difficultyFactor * distanceFactor)
                {
                    prefab = entry.Prefab;
                    break;
                }
            }
            Enemy e = Instantiate(prefab, enemyPos, Quaternion.identity);
            e.SetPlayerReference(_player);
            e.transform.parent = transform;
        }
    }
}
