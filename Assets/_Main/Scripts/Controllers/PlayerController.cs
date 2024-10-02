using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamagable
{
    private Rigidbody _rb;

    [SerializeField] private int _hp;
    [SerializeField] private int _armor;
    [SerializeField] private int _atk;
    [SerializeField] private int _crit;
    [SerializeField] private float _cooldown;
    [SerializeField] private float _moveSpeed = 5;

    public float MoveSpeed => _moveSpeed;

    private bool IsCrit => Random.Range(0, 1) < _crit;
    private int Damage => _atk * (IsCrit ? 2 : 1);
    public int CurrentHp { get; private set; }
    public int Level { get; private set; } = 1;
    public int CurrentExp { get; private set; }
    public int Exp => (int)(100 * Level);

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        var direction = Vector3.right * x + Vector3.forward * z;
        _rb.velocity = direction.normalized * MoveSpeed;

        _rb.rotation = Quaternion.LookRotation(direction);
    }

    public void TakeDamage(int damage)
    {
        CurrentHp -= damage;
        if (CurrentHp <= 0)
        {
            CurrentHp = 0;
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player dead!");
        gameObject.SetActive(false);
    }

    #region Level
    public void IncreaseExp(int exp)
    {
        CurrentExp += exp;
        if (CurrentExp < Exp)
        {

        }
        else
        {
            int surplus = CurrentHp - Exp;
            LevelUp();
            IncreaseExp(surplus);
        }
    }

    public void LevelUp()
    {
        CurrentHp = 0;
        Level++;
    }
    #endregion
}
