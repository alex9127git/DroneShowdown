using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _vx;
    [SerializeField] private float _vy;
    private const float _accelPerSecond = 20f;
    private const float _decayPerSecond = 0.3f;
    private const float _maxv = 10f;

    private float _surgeChargingProcess = 0f;
    private float _surgeX = 0f;
    private float _surgeY = 0f;
    private float _surgeDecayX = 0f;
    private float _surgeDecayY = 0f;
    private float _surgingProcess = 0f;
    private bool _isSurging = false;

    [SerializeField] private Renderer _renderer;

    private void Start()
    {
        _vx = 0;
        _vy = 0;
    }

    private void Update()
    {
        transform.position += new Vector3(_vx, _vy) * Time.deltaTime;
        if (_isSurging)
        {
            transform.position += new Vector3(_surgeX, _surgeY) * Time.deltaTime;
            _surgeX -= _surgeDecayX * Time.deltaTime;
            _surgeY -= _surgeDecayY * Time.deltaTime;
            _surgingProcess += 2 * Time.deltaTime;
            if (_surgingProcess >= 1f)
            {
                _isSurging = false;
                _surgeX = _surgeY = _surgeDecayX = _surgeDecayY = 0f;
                _surgingProcess = 0f;
            }
        }
        else
        {
            bool isAcceleratingX = false;
            bool isAcceleratingZ = false;
            if (Input.GetKey(KeyCode.W))
            {
                _vy += _accelPerSecond * Time.deltaTime * Mathf.Max(1 - _vy / _maxv, 0);
                isAcceleratingZ = true;
            }
            if (Input.GetKey(KeyCode.S))
            {
                _vy -= _accelPerSecond * Time.deltaTime * Mathf.Max(1 + _vy / _maxv, 0);
                isAcceleratingZ = true;
            }
            if (Input.GetKey(KeyCode.D))
            {
                _vx += _accelPerSecond * Time.deltaTime * Mathf.Max(1 - _vx / _maxv, 0);
                isAcceleratingX = true;
            }
            if (Input.GetKey(KeyCode.A))
            {
                _vx -= _accelPerSecond * Time.deltaTime * Mathf.Max(1 + _vx / _maxv, 0);
                isAcceleratingX = true;
            }
            if (!isAcceleratingX)
            {
                _vx *= Mathf.Pow(_decayPerSecond, Time.deltaTime);
            }
            if (!isAcceleratingZ)
            {
                _vy *= Mathf.Pow(_decayPerSecond, Time.deltaTime);
            }
            if (Mathf.Abs(_vx) < 0.01f)
            {
                _vx = 0f;
            }
            if (Mathf.Abs(_vy) < 0.01f)
            {
                _vy = 0f;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                _surgeChargingProcess += 2 * Time.deltaTime;
                _surgeChargingProcess = Mathf.Clamp(_surgeChargingProcess, 0, 1);
            }
            else
            {
                if (_surgeChargingProcess >= 1f && (_vx * _vx + _vy * _vy) > 1)
                {
                    _surgeX = _vx * 4;
                    _surgeY = _vy * 4;
                    _surgeDecayX = _surgeX * 2;
                    _surgeDecayY = _surgeY * 2;
                    _surgeChargingProcess = 0f;
                    _isSurging = true;
                }
                _surgeChargingProcess = 0f;
            }
        }
        _renderer.material.SetFloat("_Charging_Process", _surgeChargingProcess);
        _renderer.material.SetFloat("_Surging_Process", _isSurging ? 1 - _surgingProcess : 0);
    }

    public Vector3 GetVelocity()
    {
        return new Vector3(_vx, _vy);
    }
}
