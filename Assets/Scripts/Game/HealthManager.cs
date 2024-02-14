using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private GameObject _structure;
    private int _maxHP;
    private int _hp;

    void Awake()
    {
        int layer = GetComponent<Player>() ? 6 : 7;
        _maxHP = 0;
        foreach (Block block in _structure.GetComponentsInChildren<Block>())
        {
            _maxHP += block.HP;
            block.gameObject.layer = layer;
        }
        _hp = _maxHP;
    }

    public void Damage(int damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            Die();
        }
    }
    
    public void Die()
    {
        Destroy(gameObject);
    }
}
