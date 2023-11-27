using System.Collections.Generic;
using UnityEngine;

public class ItemsView : MonoBehaviour
{
    public Vector2 itemDisplayOffset;
    public Vector2 itemDisplaySpacing;
    public GameObject itemDisplayPrefab;
    private List<PassiveItem> items;
    private List<GameObject> itemDisplays = new List<GameObject>();
    
    void Start()
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
        for (int i = 0; i < items.Count; i++)
        {
            ItemDisplay itemDisplay = Instantiate(
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
        int index = items.IndexOf(passiveItem);
        itemDisplays[index].GetComponent<Animator>().SetTrigger("Activate");
    }
}
