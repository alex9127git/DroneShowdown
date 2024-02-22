using TMPro;
using UnityEngine;

public class BuyBlockButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private int _index;
    private int _price;

    private int PriceFormula(int index, int level)
    {
        switch (index)
        {
            case 0: return 10 * (level + 1);
            case 1: return 20 * (int)Mathf.Pow(2, level);
            case 2: return 30 * (int)Mathf.Pow(2, level);
            case 3: return 20 * (int)Mathf.Pow(3, level);
            case 4: return 30 * (int)Mathf.Pow(3, level);
            case 5: return 50 * (int)Mathf.Pow(2, level);
            default: return 0;
        }
    }

    private void Update()
    {
        _price = PriceFormula(_index, Progress.Instance.GetBoughtBlocks(_index));
        _text.SetText("Купить\n" + _price + " Т\n(" + (Progress.Instance.GetBoughtBlocks(_index) - Progress.Instance.CountBlocksInDrone(_index)) + "/" + Progress.Instance.GetBoughtBlocks(_index) + ")");
    }

    public void BuyBlock()
    {
        Progress.Instance.BuyBlock(_index, _price);
    }
}
