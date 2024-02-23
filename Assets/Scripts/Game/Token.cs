using UnityEngine;

public class Token : MonoBehaviour
{
    private const float _maxSpeed = 20f;
    private const float _speedDecay = 0.1f;

    private Vector3 _speed;
    private int _value;

    private bool _collected;

    public int Value { get { return _value; } }

    public void Init(int value)
    {
        _speed = new Vector3(Random.Range(0f, _maxSpeed), Random.Range(0f, _maxSpeed));
        _value = value;
        _collected = false;
    }

    private void Update()
    {
        if (PauseManager.Instance.Paused) return;
        transform.position += _speed * Time.deltaTime;
        _speed *= Mathf.Pow(_speedDecay, Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>() is Player player && !_collected)
        {
            player.AddTokens(_value);
            _collected = true;
            Audio.Instance.TokenPickUp.Play();
            Destroy(gameObject);
        }
    }
}
