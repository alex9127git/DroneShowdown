using System;
using UnityEngine;


public enum EnemyClass
{
    Neutral,
    Armed,
    Heavy,
    Blitz,
    Watcher
}


[Serializable]
public class EnemyEntry
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private EnemyClass _enemyClass;
    [SerializeField] private float _chanceToSpawn;

    public EnemyClass Class { get { return _enemyClass; } }
    public float Chance { get { return _chanceToSpawn; } }
    public Enemy Prefab { get { return _enemy; } }
}

[CreateAssetMenu(fileName = "New Enemy List", menuName = "ScriptableObjects/EnemyList", order = 1)]
public class EnemyList : ScriptableObject
{
    [SerializeField] private EnemyEntry[] _enemies;

    public EnemyEntry[] Enemies { get { return _enemies; } }
}
