using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 _direction;
    private float _speedModifier = 20f;

    void Update()
    {
        transform.position += _direction * _speedModifier * Time.deltaTime;
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }
}
