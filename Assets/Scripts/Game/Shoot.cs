using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Player _host;

    private void Awake()
    {
        _host = GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 target = GetMousePos();
            Vector3 direction = (transform.position - target).normalized;
            _host.Shoot(direction, _bulletPrefab);
        }
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
