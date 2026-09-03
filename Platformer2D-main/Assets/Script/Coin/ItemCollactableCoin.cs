using UnityEngine;

public class ItemCollactableCoin : ItemCollactableBase
{
    protected override void OnCollect()
    {
        base.OnCollect();
        ItemManager.Instance.AddCoins();
    }
}
