using UnityEngine;
using static UnityEngine.GraphicsBuffer;

enum AttackType
{
    Nothing,
    Straight,
    Spread,
    BigSpread,
    Round
}

public class Block : MonoBehaviour
{
    [SerializeField] private int _hp;
    [SerializeField] private AttackType _attackType;
    [SerializeField] private float _cooldown;
    [SerializeField] private float _attackTimer = 0f;
    [SerializeField] private float _projSpeed = 20f;
    private bool _isPlayer;

    public int HP { get { return _hp; } }

    public void UpdateHost()
    {
        _isPlayer = gameObject.layer == 6;
    }

    private void Update()
    {
        _attackTimer -= Time.deltaTime;
        if (_attackTimer < 0f) _attackTimer = 0f;
    }

    private float GetAngle(Vector3 v)
    {
        float angle = Vector3.Angle(new Vector3(1, 0), v);
        angle = v.y >= 0 ? angle : 360 - angle;
        return angle;
    }

    public void Attack(Vector3 target, Bullet bulletPrefab, bool pl)
    {
        if (_attackTimer > 0f) return;
        if (_attackType == AttackType.Nothing) return;
        if (!_isPlayer)
        {
            target.x += Random.Range(-1.5f, 1.5f);
            target.y += Random.Range(-1.5f, 1.5f);
        }
        Vector3 direction = (target - transform.position).normalized;
        float angle = GetAngle(direction);
        Debug.Log(angle);
        _attackTimer = _cooldown + (_isPlayer ? 0 : Random.Range(0f, 0.1f));
        switch (_attackType)
        {
            case AttackType.Straight:
                ShootAtAngle(angle, bulletPrefab, pl); break;
            case AttackType.Spread:
                ShootAtAngle(angle, bulletPrefab, pl);
                ShootAtAngle(angle - 30, bulletPrefab, pl);
                ShootAtAngle(angle + 30, bulletPrefab, pl);
                break;
            case AttackType.BigSpread:
                ShootAtAngle(angle, bulletPrefab, pl);
                ShootAtAngle(angle - 20, bulletPrefab, pl);
                ShootAtAngle(angle + 20, bulletPrefab, pl);
                ShootAtAngle(angle - 40, bulletPrefab, pl);
                ShootAtAngle(angle + 40, bulletPrefab, pl);
                break;
            case AttackType.Round:
                for (int i = 0; i < 6; i++)
                {
                    ShootAtAngle(angle + i * 60, bulletPrefab, pl);
                }
                break;
        }
    }

    public void ShootAtAngle(float angle, Bullet bulletPrefab, bool pl)
    {
        angle %= 360;
        Bullet b = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        b.Init(angle, _projSpeed, pl);
    }
}
