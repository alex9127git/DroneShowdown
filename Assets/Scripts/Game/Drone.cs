using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Drone : MonoBehaviour
{
    [SerializeField] protected GameObject _structure;
    [SerializeField] private ParticleSystem _dieEffect;
    private bool _isPlayer;
    protected int _maxHP;
    protected int _hp;
    protected bool _alive = true;

    public int HP { get => _hp; }
    public int MaxHP { get => _maxHP; }

    public bool Alive { get { return _alive; } }

    protected void CalculateHP()
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

    protected virtual void Awake()
    {
        CalculateHP();
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

    public void RestoreHP(int hp)
    {
        _hp += hp;
        if (_hp >= MaxHP)
        {
            _hp = MaxHP;
        }
    }
    
    public void Die()
    {
        Destroy(_structure.gameObject);
        if (!_isPlayer)
        {
            TokenManager.Instance.SpawnTokens(_maxHP, transform.position);
            Enemy e = GetComponent<Enemy>();
            Player p = FindObjectOfType<Player>();
            if (e.IsBoss)
            {
                switch (e.Class)
                {
                    case EnemyClass.Armed: Progress.Instance.AddSigil(0); break;
                    case EnemyClass.Blitz: Progress.Instance.AddSigil(1); break;
                    case EnemyClass.Heavy: Progress.Instance.AddSigil(2); break;
                    case EnemyClass.Watcher: Progress.Instance.AddSigil(3); break;
                }
                p.RestoreHP(3);
            }
            else
            {
                if (e.Class != EnemyClass.Neutral) p.RestoreHP(1);
            }
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
