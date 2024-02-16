using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _angle;
    private const float SpeedModifier = 20f;
    private bool _isPlayer;

    private void Update()
    {
        transform.position += transform.right * SpeedModifier * Time.deltaTime;
    }

    public void SetDirection(float angle)
    {
        _angle = angle;
        transform.localEulerAngles = new Vector3(0, 0, _angle);
    }

    public void SetIsPlayer(bool p)
    {
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
            player.Damage(1);
            Destroy(gameObject);
        }
    }
}
