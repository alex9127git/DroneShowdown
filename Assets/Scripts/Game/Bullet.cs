using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _angle;
    private float _speedModifier = 20f;
    private bool _isPlayer;
    private bool _active;
    private float _lifetime;
    private int _damage;

    private void Update()
    {
        if (PauseManager.Instance.Paused) return;
        transform.position += transform.right * _speedModifier * Time.deltaTime;
        _lifetime -= Time.deltaTime;
        if (_lifetime < 0 )
        {
            Destroy(gameObject);
        }
    }

    public void Init(float angle, float speed, bool p, int dmg)
    {
        _active = true;
        _lifetime = 5f;
        transform.parent = BulletParent.Instance.transform;
        _angle = angle;
        transform.localEulerAngles = new Vector3(0, 0, _angle);
        _speedModifier = speed;
        _isPlayer = p;
        _damage = dmg;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.GetComponent<Enemy>() is Enemy enemy) && _isPlayer && _active && enemy.Alive)
        {
            enemy.Damage(_damage);
            _active = false;
            Destroy(gameObject);
        }
        if ((collision.gameObject.GetComponent<Player>() is Player player) && !_isPlayer && _active) 
        {
            if (!player.IsSurging && player.IFrameTimer <= 0f)
            {
                player.Damage(_damage);
                player.AddIFrames();
                Audio.Instance.Hit.Play();
                Vignette.Instance.Flash();
            }
            _active = false;
            Destroy(gameObject);
        }
    }
}
