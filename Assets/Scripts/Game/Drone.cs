using UnityEngine;

public class Drone : MonoBehaviour
{
    [SerializeField] private GameObject _structure;
    private bool _isPlayer;
    private int _maxHP;
    private int _hp;

    protected virtual void Awake()
    {
        _isPlayer = this is Player;
        int layer = _isPlayer ? 6 : 7;
        _maxHP = 0;
        foreach (Block block in _structure.GetComponentsInChildren<Block>())
        {
            _maxHP += block.HP;
            block.gameObject.layer = layer;
            block.UpdateHost();
        }
        if (!_isPlayer) _maxHP = (int)(_maxHP * Mathf.Sqrt(EnemySpawner.Instance.Difficulty));
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

    public void Shoot(Vector3 target, Bullet bulletPrefab)
    {
        foreach (Block block in _structure.GetComponentsInChildren<Block>())
        {
            block.Attack(target, bulletPrefab, _isPlayer);
        }
    }
}
