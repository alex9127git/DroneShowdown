using UnityEngine;
using UnityEngine.UI;

public class GridTile : MonoBehaviour
{
    private int _gridX;
    private int _gridY;
    private Image _image;
    private int _typeIndex;

    public int X { get { return _gridX; } }
    public int Y { get { return _gridY; } }
    public int Index { get { return _typeIndex; } }

    public void Init(Transform parent, int x, int y, Sprite image, int index)
    {
        transform.SetParent(parent);
        _gridX = x;
        _gridY = y;
        _image = GetComponent<Image>();
        _image.sprite = image;
        _typeIndex = index;
        transform.localPosition = new Vector3(59 + _gridX * 97, -641 + _gridY * 97, 0);
        transform.localScale = Vector3.one;
    }
}
