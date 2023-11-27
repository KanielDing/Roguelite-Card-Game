using UnityEngine;
using UnityEngine.Events;

public class PassiveItem: MonoBehaviour
{
    public string itemName;
    public string description;
    public Sprite itemImage;

    protected void ActivateAnimation()
    {
        FindObjectOfType<ItemsView>()?.ActivateItem(this);
    }
}