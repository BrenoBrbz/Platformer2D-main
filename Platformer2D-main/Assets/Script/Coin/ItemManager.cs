using UnityEngine;
using System;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;

    public int coins;

    public Action<int> OnCoinsChanged;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        coins = 0;
        OnCoinsChanged?.Invoke(coins);
    }

    public void AddCoins(int amount = 1)
    {
        coins += amount;
        OnCoinsChanged?.Invoke(coins);
    }
}
