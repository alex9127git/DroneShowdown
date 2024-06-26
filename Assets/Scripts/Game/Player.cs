using UnityEngine;

public class Player : Drone
{
    private float _vx;
    private float _vy;
    private const float _accelPerSecond = 30f;
    private const float _decayPerSecond = 0.3f;
    private const float _maxv = 12f;

    private float _surgeChargingProcess = 0f;
    private float _surgeX = 0f;
    private float _surgeY = 0f;
    private float _surgeDecayX = 0f;
    private float _surgeDecayY = 0f;
    private float _surgingProcess = 0f;
    private bool _isSurging = false;

    private float _iframeTimer = 0f;
    public float IFrameTimer { get => _iframeTimer; }

    [SerializeField] private int _zone;

    [SerializeField] private int _earnedTokens = 0;

    [SerializeField] private BlockList _blocks;

    public int EarnedTokens { get { return _earnedTokens; } }
    public bool IsSurging {  get { return _isSurging; } }

    [SerializeField] private Renderer _renderer;

    protected override void Awake()
    {
        _zone = -1;
        LoadStructure();
        CalculateHP();
    }

    private void LoadStructure()
    {
        BlockData[] blocks = Progress.Instance.LoadDrone();
        foreach (BlockData bd in blocks)
        {
            Instantiate(_blocks.Prefabs[bd.TypeIndex], new Vector3(bd.X, bd.Y), Quaternion.Euler(0, 0, -180), _structure.transform);
        }
    }

    public void AddIFrames()
    {
        _iframeTimer = 0.3f;
    }

    private void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -1490f, 1490f), Mathf.Clamp(transform.position.y, -1490f, 1490f));
        if (PauseManager.Instance.Paused) return;
        _iframeTimer -= Time.deltaTime;
        if (!_alive) return;
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
        CheckZone();
    }

    public Vector3 GetVelocity()
    {
        return new Vector3(_vx, _vy);
    }

    public void AddTokens(int amount)
    {
        _earnedTokens += amount;
    }

    public void CheckZone()
    {
        ZoneText z = FindObjectOfType<ZoneText>();
        if (-490 <= transform.position.x && transform.position.x <= 490)
        {
            if (-490 <= transform.position.y && transform.position.y <= 490 && _zone != 0)
            {
                _zone = 0;
                z.Animate("����������� ����");
            }
            else if (510 <= transform.position.y && _zone != 1)
            {
                _zone = 1;
                z.Animate("��������� ��������");
            }
            else if (transform.position.y <= -510 && _zone != 5)
            {
                _zone = 5;
                z.Animate("������������ ������");
            }
        }
        else if (510 <= transform.position.x)
        {
            if (-490 <= transform.position.y && transform.position.y <= 490 && _zone != 3)
            {
                _zone = 3;
                z.Animate("���� ��������� ��������");
            }
            else if (510 <= transform.position.y && _zone != 2)
            {
                _zone = 2;
                z.Animate("������� �����");
            }
            else if (transform.position.y <= -510 && _zone != 4)
            {
                _zone = 4;
                z.Animate("�������������� �����");
            }
        }
        else if (transform.position.x <= -510)
        {
            if (-490 <= transform.position.y && transform.position.y <= 490 && _zone != 7)
            {
                _zone = 7;
                z.Animate("���������� ����������");
            }
            else if (510 <= transform.position.y && _zone != 8)
            {
                _zone = 8;
                z.Animate("�������� �������� ������");
            }
            else if (transform.position.y <= -510 && _zone != 6)
            {
                _zone = 6;
                z.Animate("�������� \"������\"");
            }
        }
    }
}
