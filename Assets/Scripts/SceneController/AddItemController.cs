using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AddItemController : MonoBehaviour
{
    public GameObject clickableItemPrefab;
    public int numItemChoices;
    public float itemGapScalar;
    public List<GameObject> possibleItems;
    public List<GameObject> randomItems;

    private void Start()
    {
        RemoveHeldItemsFromPossibleItems();
        GetRandomItems();
        GenerateClickableItems();
    }

    private void RemoveHeldItemsFromPossibleItems()
    {
        for (var index = possibleItems.Count - 1; index >= 0; index--)
        {
            var possibleItem = possibleItems[index];
            if (PlayerController.instance.items.Exists(item =>
                item.itemName == possibleItem.GetComponent<PassiveItem>().itemName))
            {
                possibleItems.Remove(possibleItem);
            }
        }
    }

    private void GetRandomItems()
    {
        if (numItemChoices > possibleItems.Count) numItemChoices = possibleItems.Count;
        for (var itemChoice = 0; itemChoice < numItemChoices; itemChoice++)
        {
            {
                var selectedUniqueItem = false;
                while (selectedUniqueItem == false)
                {
                    var randomItem = possibleItems[Random.Range(0, possibleItems.Count)];
                    if (randomItems.Contains(randomItem)) continue;
                    randomItems.Add(randomItem);
                    selectedUniqueItem = true;
                }
            }
        }
    }

    private void GenerateClickableItems()
    {
        for (var i = 0; i < randomItems.Count; i++)
        {
            var clickableItem = Instantiate(clickableItemPrefab, GetCardRestPosition(i), Quaternion.identity, transform)
                .GetComponent<Clickable>();
            clickableItem.GetComponent<ItemDisplay>().Initialise(randomItems[i].GetComponent<PassiveItem>());
            var index = i;
            clickableItem.GetComponent<Clickable>().onClick.AddListener(() => AddItem(randomItems[index]));
        }
    }

    private Vector3 GetCardRestPosition(int cardIndex)
    {
        var halfWayPoint = ((double) randomItems.Count - 1) / 2;
        return transform.position +
               new Vector3((float) ((cardIndex - halfWayPoint) * itemGapScalar / Mathf.Sqrt(randomItems.Count)), 0);
    }

    private void AddItem(GameObject clickableItem)
    {
        PlayerController.instance.AddPassiveItem(clickableItem);
        FinishEncounter();
    }

    public void FinishEncounter()
    {
        GameController.instance.LoadNextEncounter();
    }
}
