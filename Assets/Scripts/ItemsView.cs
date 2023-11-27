using System.Collections.Generic;
using UnityEngine;

public class ItemsView : MonoBehaviour
{
    public Vector2 itemDisplayOffset;
    public Vector2 itemDisplaySpacing;
    public GameObject itemDisplayPrefab;
    private List<PassiveItem> items;
    private readonly List<GameObject> itemDisplays = new();
    
    private void Start()
    {
        DrawItems();
    }

    public void UpdateItemDisplays()
    {
        foreach (var itemDisplay in itemDisplays)
        {
            Destroy(itemDisplay);
        }
        itemDisplays.Clear();
    }

    private void DrawItems()
    {
        items = PlayerController.instance.items;
        for (var i = 0; i < items.Count; i++)
        {
            var itemDisplay = Instantiate(
                    itemDisplayPrefab,
                    new Vector3(itemDisplayOffset.x + (i * itemDisplaySpacing.x), itemDisplayOffset.y + (i*itemDisplaySpacing.y), 0),
                    Quaternion.identity,
                    transform )
                .GetComponent<ItemDisplay>();
            itemDisplays.Add(itemDisplay.gameObject);
            itemDisplay.Initialise(items[i]);
        }
    }
    

    public void ActivateItem(PassiveItem passiveItem)
    {
        var index = items.IndexOf(passiveItem);
        itemDisplays[index].GetComponent<Animator>().SetTrigger("Activate");
    }
}
