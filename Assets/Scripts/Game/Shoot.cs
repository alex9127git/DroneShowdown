using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Bullet _bulletPrefab;
    private float _cooldown = 1f;
    private float _shootTimer = 0f;

    private void Update()
    {
        if (Input.GetMouseButton(0) && _shootTimer == 0f)
        {
            CreateBullet();
            _shootTimer = _cooldown;
        }
        _shootTimer -= Time.deltaTime;
        if (_shootTimer < 0f)
        {
            _shootTimer = 0f;
        }
    }

    private void CreateBullet()
    {
        Vector3 target = GetMousePos();
        Vector3 direction = (transform.position - target).normalized;
        Bullet b = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        b.SetDirection(direction);
        b.SetIsPlayer(true);
    }

    private Vector3 GetMousePos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = _camera.transform.position.z;
        Vector3 worldPos = _camera.ScreenToWorldPoint(mousePos);
        worldPos.z = 0f;
        return worldPos;
    }
}
