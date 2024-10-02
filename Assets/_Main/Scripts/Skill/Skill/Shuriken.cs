using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//skill phi tiêu bắn theo hướng di chuyển của nhân vật
public class Shuriken : Skill
{
    [SerializeField] private int _speed;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        
    }
    private void ShurikenAtk()
    {
        _rb.velocity = transform.forward * _speed;
    }
}
