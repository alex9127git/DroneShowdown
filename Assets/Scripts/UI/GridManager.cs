using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class BlockData
{
    public Block Prefab;
    public int TypeIndex;
    public int X;
    public int Y;

    public BlockData(Block prefab, int x, int y, int index)
    {
        Prefab = prefab;
        X = x;
        Y = y;
        TypeIndex = index;
    }
}

public class GridManager : MonoBehaviour
{
    [SerializeField] private BlockList _blocks;
    [SerializeField] private Block _corePrefab;
    [SerializeField] private GridTile _tilePrefab;
    private bool holding = false;
    [SerializeField] private Image _preview;
    private int _held_index;

    public void HoldBlock(int index)
    {
        if (Progress.Instance.GetBoughtBlocks(index) == Progress.Instance.CountBlocksInDrone(index)) return;
        holding = true;
        _held_index = index;
        _preview.sprite = _blocks.Sprites[index];
        _preview.color = new Color(1, 1, 1, 0.5f);
    }

    private void Awake()
    {
        BlockData[] blocks = Progress.Instance.LoadDrone();
        foreach (BlockData block in blocks)
        {
            GridTile tile = Instantiate(_tilePrefab);
            tile.Init(transform, block.X + 3, block.Y + 3, _blocks.Sprites[block.TypeIndex], block.TypeIndex);
        }
    }

    public void GoBack()
    {
        SaveCurrentDrone();
        Menu.LoadMenu();
    }

    private void SaveCurrentDrone()
    {
        BlockData[] blocks = new BlockData[transform.childCount - 1];
        int i = 0;
        foreach (GridTile tile in GetComponentsInChildren<GridTile>())
        {
            blocks[i] = new BlockData(_blocks.Prefabs[tile.Index], tile.X - 3, tile.Y - 3, tile.Index);
            i += 1;
        }
        Progress.Instance.SaveDrone(blocks);
    }

    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        int leftGridEdge = Screen.width / 32;
        int rightGridEdge = Screen.width / 10 * 4;
        int gridSide = (rightGridEdge - leftGridEdge) / 7;
        int upperGridEdge = Screen.height - leftGridEdge * 2;
        int lowerGridEdge = upperGridEdge - gridSide * 7;
        bool inGrid = mousePos.x < rightGridEdge && mousePos.x > leftGridEdge && mousePos.y > lowerGridEdge && mousePos.y < upperGridEdge;
        int gridPosX = (int)Mathf.Clamp((int)(mousePos.x - leftGridEdge) / gridSide, 0f, 6f);
        int gridPosY = (int)Mathf.Clamp((int)(mousePos.y - lowerGridEdge) / gridSide, 0f, 6f);
        _preview.transform.localPosition = new Vector3(59 + gridPosX * 97, -641 + gridPosY * 97, 0);
        if (Input.GetMouseButtonDown(0) && inGrid)
        {
            if (gridPosX == gridPosY && gridPosX == 3) return;
            GridTile t = findTile(gridPosX, gridPosY);
            if (t != null)
            {
                t.transform.SetParent(null);
                Destroy(t.gameObject);
            }
            if (holding)
            {
                GridTile tile = Instantiate(_tilePrefab);
                tile.Init(transform, gridPosX, gridPosY, _preview.sprite, _held_index);
                holding = false;
                _preview.color = new Color(1, 1, 1, 0f);
            }
            SaveCurrentDrone();
        }
    }

    private GridTile findTile(int x, int y)
    {
        foreach (GridTile tile in GetComponentsInChildren<GridTile>())
        {
            if (tile.X == x && tile.Y == y) return tile;
        }
        return null;
    }
}
