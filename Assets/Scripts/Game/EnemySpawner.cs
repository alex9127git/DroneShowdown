using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Player _player;

    private void Start()
    {
        StartCoroutine(SpawningEnemies());
    }

    IEnumerator SpawningEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0f, 5f));
            Vector3 enemyPos = _player.transform.position;
            enemyPos.x += Random.Range(-100f, 100f);
            enemyPos.y += Random.Range(-100f, 100f);
            Enemy e = Instantiate(_enemyPrefab, enemyPos, Quaternion.identity);
            e.SetPlayerReference(_player);
            e.transform.parent = transform;
        }
    }
}
