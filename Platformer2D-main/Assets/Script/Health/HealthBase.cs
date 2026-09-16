using System;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    public int StartLife;
    public bool DestroyOnKill = false;
    private int _currentLife;
    private bool _isDead = false;

    public Action OnKill;

    private FlashColor _flashColor;
    private void Awake()
    {
        Init();
        if(_flashColor == null)
        {
            _flashColor = GetComponent<FlashColor>();

        }
    }

  
    private void Init()
    {
        _isDead = false;
        _currentLife = StartLife;
    }

    public void Damage(int damage)
    {
        if (_isDead) return;
        _currentLife -= damage;

        if (_currentLife <= 0)
        {
            Kill();
        }

        if(_flashColor != null)
        {
            _flashColor.Flash();
        }
    }

    private void Kill()
    {
        _isDead = true;

        if (DestroyOnKill)
        {
            Destroy(gameObject);
        }

        OnKill?.Invoke();

    }
}
