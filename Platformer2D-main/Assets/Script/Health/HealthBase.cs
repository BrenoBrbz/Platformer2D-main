using UnityEngine;

public class HealthBase : MonoBehaviour
{
    public int StartLife;
    public bool DestroyOnKill = false;
    private int _currentLife;
    private bool _isDead = false;
    private void Awake()
    {
        Init();
    }

    // Update is called once per frame
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
    }

    private void Kill()
    {
        _isDead = true;

        if (DestroyOnKill)
        {
            Destroy(gameObject);
        }
    }
}
