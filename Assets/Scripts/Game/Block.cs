using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private int hp;

    public int HP { get { return hp; } }
}
