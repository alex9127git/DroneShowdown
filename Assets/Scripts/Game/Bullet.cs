using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 _direction;
    private const float SpeedModifier = 20f;
    private bool _isPlayer;

    private void Update()
    {
        transform.position += _direction * SpeedModifier * Time.deltaTime;
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }

    public void SetIsPlayer(bool p)
    {
        _isPlayer = p;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.GetComponent<Enemy>() is Enemy enemy) && _isPlayer)
        {
            enemy.HP.Damage(1);
        }
        if ((collision.gameObject.GetComponent<Player>() is Player player) && !_isPlayer) 
        { 
            player.HP.Damage(1);
        }
        Destroy(gameObject);
    }
}
