using UnityEngine;
using TMPro;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;

    private void Start()
    {
        coinText.text = ItemManager.Instance.coins.ToString();
        ItemManager.Instance.OnCoinsChanged += UpdateCoinText;
    }

    private void OnDestroy()
    {
        if (ItemManager.Instance != null)
            ItemManager.Instance.OnCoinsChanged -= UpdateCoinText;
    }

    private void UpdateCoinText(int amount)
    {
        coinText.text = amount.ToString();
    }
}
