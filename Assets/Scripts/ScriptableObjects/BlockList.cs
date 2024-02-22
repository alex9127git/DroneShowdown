using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Block List", menuName = "ScriptableObjects/BlockList", order = 2)]
public class BlockList : ScriptableObject
{
    [SerializeField] private Sprite[] _blockSprites;
    [SerializeField] private Block[] _blockPrefabs;

    public Sprite[] Sprites { get { return _blockSprites; } }
    public Block[] Prefabs { get { return _blockPrefabs; } }
}
