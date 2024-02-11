using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 _direction;
    private const float SpeedModifier = 20f;

    private void Update()
    {
        transform.position += _direction * SpeedModifier * Time.deltaTime;
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Enemy>() is Enemy enemy)
        {
            Destroy(enemy.gameObject);
            Destroy(gameObject);
        }
    }
}
