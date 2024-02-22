using UnityEngine;

enum EnemyStrat
{
    Idle,
    Walking,
    ChasingPlayer
}

public class Enemy : Drone
{
    private Player _player;
    private Vector3 _targetPos;
    private EnemyStrat _strat;
    private EnemyClass _class;
    private float _timer;
    private const float AggroDistance = 40f;
    private Vector3 _velocity;
    private float _cooldown = 0.1f;
    private float _shootTimer = 0f;

    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float SpeedModifier = 8f;
    [SerializeField] private bool _isBoss = false;

    public EnemyClass Class { get => _class; set => _class = value; }
    public bool IsBoss { get => _isBoss; }

    public void SetPlayerReference(Player p)
    {
        _player = p;
    }

    private void Update()
    {
        if (!_alive) return;
        if (DistanceToPlayer() > 300f) Destroy(gameObject);
        UpdateStrat();
        if (_strat == EnemyStrat.ChasingPlayer)
        {
            _velocity = (_player.transform.position - transform.position).normalized * SpeedModifier;
            transform.position += _velocity * Time.deltaTime;
            _shootTimer -= Time.deltaTime;
            if (_shootTimer < 0f)
            {
                _cooldown = 0.1f;
                _shootTimer = _cooldown;
                Vector3 target = _player.transform.position;
                Shoot(target, _bulletPrefab);
            }
        }
        else if (_strat == EnemyStrat.Walking)
        {
            transform.position += _velocity * Time.deltaTime;
        }
        _timer -= Time.deltaTime;
    }

    private void UpdateStrat()
    {
        if (DistanceToPlayer() < AggroDistance)
        {
            _strat = EnemyStrat.ChasingPlayer;
            _timer = 0f;
        }
        else if (_timer < 0f)
        {
            if (_strat != EnemyStrat.Idle)
            {
                _strat = EnemyStrat.Idle;
                _timer = Random.Range(0f, 2f);
            }
            else
            {
                _strat = EnemyStrat.Walking;
                _targetPos = transform.position;
                float dx = Random.Range(-20f, 20f);
                float dy = Random.Range(-20f, 20f);
                _targetPos.x += dx;
                _targetPos.y += dy;
                float dist = DistanceBetween(transform.position, _targetPos);
                _timer = dist / SpeedModifier;
                _velocity = new Vector3(dx / _timer, dy / _timer);
            }
        }
    }

    private float DistanceToPlayer()
    {
        Vector3 player = _player.transform.position;
        Vector3 enemy = transform.position;
        return DistanceBetween(player, enemy);
    }

    private float DistanceBetween(Vector3 a, Vector3 b)
    {
        return Mathf.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y));
    }
}
