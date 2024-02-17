using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _angle;
    private float _speedModifier = 20f;
    private bool _isPlayer;

    private void Update()
    {
        transform.position += transform.right * _speedModifier * Time.deltaTime;
    }

    public void Init(float angle, float speed, bool p)
    {
        transform.parent = BulletParent.Instance.transform;
        _angle = angle;
        transform.localEulerAngles = new Vector3(0, 0, _angle);
        _speedModifier = speed;
        _isPlayer = p;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.GetComponent<Enemy>() is Enemy enemy) && _isPlayer)
        {
            enemy.Damage(1);
            Destroy(gameObject);
        }
        if ((collision.gameObject.GetComponent<Player>() is Player player) && !_isPlayer) 
        {
            if (!player.IsSurging) player.Damage(1);
            Destroy(gameObject);
        }
    }
}
