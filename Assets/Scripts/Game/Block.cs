using System;
using UnityEngine;
using Random = UnityEngine.Random;

enum AttackType
{
    Nothing,
    Straight,
    Fast,
    Spread,
    BigSpread,
    Round
}

[Serializable]
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
        if (PauseManager.Instance.Paused) return;
        _attackTimer -= Time.deltaTime;
        if (_attackTimer < 0f) _attackTimer = 0f;
    }

    private float GetAngle(Vector3 v)
    {
        float angle = Vector3.Angle(new Vector3(1, 0), v);
        angle = v.y >= 0 ? angle : 360 - angle;
        return angle;
    }

    public bool Attack(Vector3 target, Bullet bulletPrefab, bool pl)
    {
        if (_attackTimer > 0f) return false;
        if (_attackType == AttackType.Nothing) return false;
        if (!_isPlayer)
        {
            target.x += Random.Range(-1.5f, 1.5f);
            target.y += Random.Range(-1.5f, 1.5f);
        }
        Vector3 direction = (target - transform.position).normalized;
        float angle = GetAngle(direction);
        _attackTimer = _cooldown + (_isPlayer ? 0 : Random.Range(0f, 0.1f));
        switch (_attackType)
        {
            case AttackType.Straight:
                ShootAtAngle(angle, bulletPrefab, pl, (_isPlayer && Random.Range(0f, 1f) <= Progress.Instance.Data.SigilUpgradeLevels[0] * 0.2) ? 2 : 1); break;
            case AttackType.Fast:
                ShootAtAngle(angle, bulletPrefab, pl, 1);
                _attackTimer *= _isPlayer ? (1 - Progress.Instance.Data.SigilUpgradeLevels[1] * 0.1f) : 1;
                break;
            case AttackType.Spread:
                ShootAtAngle(angle, bulletPrefab, pl, (_isPlayer && Random.Range(0f, 1f) <= Progress.Instance.Data.SigilUpgradeLevels[2] * 0.2) ? 2 : 1);
                ShootAtAngle(angle - 30, bulletPrefab, pl, (_isPlayer && Random.Range(0f, 1f) <= Progress.Instance.Data.SigilUpgradeLevels[2] * 0.2) ? 2 : 1);
                ShootAtAngle(angle + 30, bulletPrefab, pl, (_isPlayer && Random.Range(0f, 1f) <= Progress.Instance.Data.SigilUpgradeLevels[2] * 0.2) ? 2 : 1);
                break;
            case AttackType.BigSpread:
                ShootAtAngle(angle, bulletPrefab, pl, (_isPlayer && Random.Range(0f, 1f) <= Progress.Instance.Data.SigilUpgradeLevels[2] * 0.2) ? 2 : 1);
                ShootAtAngle(angle - 20, bulletPrefab, pl, (_isPlayer && Random.Range(0f, 1f) <= Progress.Instance.Data.SigilUpgradeLevels[2] * 0.2) ? 2 : 1);
                ShootAtAngle(angle + 20, bulletPrefab, pl, (_isPlayer && Random.Range(0f, 1f) <= Progress.Instance.Data.SigilUpgradeLevels[2] * 0.2) ? 2 : 1);
                ShootAtAngle(angle - 40, bulletPrefab, pl, (_isPlayer && Random.Range(0f, 1f) <= Progress.Instance.Data.SigilUpgradeLevels[2] * 0.2) ? 2 : 1);
                ShootAtAngle(angle + 40, bulletPrefab, pl, (_isPlayer && Random.Range(0f, 1f) <= Progress.Instance.Data.SigilUpgradeLevels[2] * 0.2) ? 2 : 1);
                break;
            case AttackType.Round:
                for (int i = 0; i < 6; i++)
                {
                    ShootAtAngle(angle + i * 60, bulletPrefab, pl, 1);
                }
                _attackTimer *= _isPlayer ? (1 - Progress.Instance.Data.SigilUpgradeLevels[3] * 0.1f) : 1;
                break;
        }
        return true;
    }

    public void ShootAtAngle(float angle, Bullet bulletPrefab, bool pl, int dmg)
    {
        angle %= 360;
        Bullet b = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        b.Init(angle, _projSpeed, pl, dmg);
    }
}
