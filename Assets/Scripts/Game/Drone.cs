using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Drone : MonoBehaviour
{
    [SerializeField] private GameObject _structure;
    [SerializeField] private ParticleSystem _dieEffect;
    private bool _isPlayer;
    private int _maxHP;
    private int _hp;
    protected bool _alive = true;

    public bool Alive { get { return _alive; } }

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
            _alive = false;
            Die();
        }
    }
    
    public void Die()
    {
        Destroy(_structure.gameObject);
        if (!_isPlayer)
        {
            TokenManager.Instance.SpawnTokens(_maxHP, transform.position);
        }
        else
        {
            Player p = GetComponent<Player>();
            Progress.Instance.AddTokens(p.EarnedTokens);
        }
        _dieEffect.Play();
        StartCoroutine(ScheduleDestroy());
    }

    public void Shoot(Vector3 target, Bullet bulletPrefab)
    {
        foreach (Block block in _structure.GetComponentsInChildren<Block>())
        {
            block.Attack(target, bulletPrefab, _isPlayer);
        }
    }

    IEnumerator ScheduleDestroy()
    {
        yield return new WaitForSeconds(2f);
        if (_isPlayer)
        {
            Menu.LoadMenu();
        }
        else Destroy(gameObject);
    }
}
