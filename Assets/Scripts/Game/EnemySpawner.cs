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
        _maxTimer = 1f - 0.001f * _difficulty * _difficulty;
        _maxTimer = Mathf.Clamp(_maxTimer, 0.2f, 1f);
    }

    IEnumerator SpawningEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.2f, _maxTimer));
            if (transform.childCount >= 50)
            {
                continue;
            }
            Vector3 enemyPos = _player.transform.position;
            enemyPos.x += Random.Range(30f, 150f) * (Random.Range(0f, 1f) < 0.5f ? -1 : 1);
            enemyPos.y += Random.Range(30f, 150f) * (Random.Range(0f, 1f) < 0.5f ? -1 : 1);
            Enemy prefab = null;
            float difficultyFactor = Mathf.Clamp((Difficulty - 1) / 40 + 1, 0f, 2f);
            float distanceFactor = Mathf.Clamp((_player.transform.position.magnitude) / 1000 + 1, 0f, 2f);
            bool armedFlag = enemyPos.x >= -500 && enemyPos.y >= -500;
            bool watcherFlag = enemyPos.x >= -500 && enemyPos.y <= 500;
            bool blitzFlag = enemyPos.x <= 500 && enemyPos.y <= 500;
            bool heavyFlag = enemyPos.x <= 500 && enemyPos.y >= -500;
            int totalFlags = (armedFlag ? 1 : 0) + (watcherFlag ? 1 : 0) + (blitzFlag ? 1 : 0) + (heavyFlag ? 1 : 0);
            foreach (EnemyEntry entry in _enemyList.Enemies)
            {
                if (!armedFlag && entry.Class == EnemyClass.Armed) continue;
                if (!watcherFlag && entry.Class == EnemyClass.Watcher) continue;
                if (!blitzFlag && entry.Class == EnemyClass.Blitz) continue;
                if (!heavyFlag && entry.Class == EnemyClass.Heavy) continue;
                if (Random.Range(0f, 100f) < entry.Chance * difficultyFactor * distanceFactor / totalFlags)
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
