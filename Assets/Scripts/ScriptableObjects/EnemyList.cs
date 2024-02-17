using System;
using UnityEngine;

[Serializable]
public class EnemyEntry
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _chanceToSpawn;

    public float Chance { get { return _chanceToSpawn; } }
    public Enemy Prefab { get { return _enemy; } }
}

[CreateAssetMenu(fileName = "New Enemy List", menuName = "ScriptableObjects/EnemyList", order = 1)]
public class EnemyList : ScriptableObject
{
    [SerializeField] private EnemyEntry[] _enemies;

    public EnemyEntry[] Enemies { get { return _enemies; } }
}
