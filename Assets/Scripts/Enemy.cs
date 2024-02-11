using System.Collections;
using System.Threading;
using UnityEngine;

enum EnemyStrat
{
    Idle,
    Walking,
    ChasingPlayer
}

public class Enemy : MonoBehaviour
{
    private Player _player;
    private Vector3 _targetPos;
    private EnemyStrat _strat;
    private float _timer;
    private const float SpeedModifier = 8f;
    private const float AggroDistance = 25f;
    private Vector3 _velocity;

    public void SetPlayerReference(Player p)
    {
        _player = p;
    }

    private void Update()
    {
        UpdateStrat();
        if (_strat == EnemyStrat.ChasingPlayer)
        {
            _velocity = (_player.transform.position - transform.position).normalized * SpeedModifier;
            transform.position += _velocity * Time.deltaTime;
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
