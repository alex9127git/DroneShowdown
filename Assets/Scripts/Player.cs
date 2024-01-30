using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _accelX;
    [SerializeField] private float _accelZ;
    private const float _deltaAccelPerSecond = 20f;
    private const float _accelDecayPerSecond = 0.3f;
    private const float _maxAccel = 10f;

    private void Start()
    {
        _accelX = 0;
        _accelZ = 0;
    }

    private void Update()
    {
        bool isAcceleratingX = false;
        bool isAcceleratingZ = false;
        if (Input.GetKey(KeyCode.W))
        {
            _accelZ += _deltaAccelPerSecond * Time.deltaTime * Mathf.Max(1 - _accelZ / _maxAccel, 0);
            isAcceleratingZ = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _accelZ -= _deltaAccelPerSecond * Time.deltaTime * Mathf.Max(1 + _accelZ / _maxAccel, 0);
            isAcceleratingZ = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _accelX += _deltaAccelPerSecond * Time.deltaTime * Mathf.Max(1 - _accelX / _maxAccel, 0);
            isAcceleratingX = true;
        }
        if (Input.GetKey(KeyCode.A)) 
        { 
            _accelX -= _deltaAccelPerSecond * Time.deltaTime * Mathf.Max(1 + _accelX / _maxAccel, 0);
            isAcceleratingX = true;
        }
        if (!isAcceleratingX)
        {
            _accelX *= Mathf.Pow(_accelDecayPerSecond, Time.deltaTime);
        }
        if (!isAcceleratingZ)
        {
            _accelZ *= Mathf.Pow(_accelDecayPerSecond, Time.deltaTime);
        }
        if (Mathf.Abs(_accelX) < 0.01f)
        {
            _accelX = 0f;
        }
        if (Mathf.Abs(_accelZ) < 0.01f)
        {
            _accelZ = 0f;
        }
        transform.position += new Vector3(_accelX, 0, _accelZ) * Time.deltaTime;
    }
}
