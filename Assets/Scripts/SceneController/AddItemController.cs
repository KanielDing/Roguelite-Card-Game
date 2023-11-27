using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class AddItemController : MonoBehaviour
{
    public GameObject clickableItemPrefab;
    public int numItemChoices;
    public float itemGapScalar;
    public List<GameObject> possibleItems;

    public List<GameObject> randomItems;


    void Start()
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
        for (int itemChoice = 0; itemChoice < numItemChoices; itemChoice++)
        {
            {
                bool selectedUniqueItem = false;
                while (selectedUniqueItem == false)
                {
                    GameObject randomItem = possibleItems[Random.Range(0, possibleItems.Count)];
                    if (!randomItems.Contains(randomItem))
                    {
                        randomItems.Add(randomItem);
                        selectedUniqueItem = true;
                    }
                }
            }
        }
    }

    private void GenerateClickableItems()
    {
        for (int i = 0; i < randomItems.Count; i++)
        {
            Clickable clickableItem = Instantiate(clickableItemPrefab, GetCardRestPosition(i), Quaternion.identity, transform)
                .GetComponent<Clickable>();
            clickableItem.GetComponent<ItemDisplay>().Initialise(randomItems[i].GetComponent<PassiveItem>());
            var index = i;
            clickableItem.GetComponent<Clickable>().onClick.AddListener(() => AddItem(randomItems[index]));
        }
    }

    private Vector3 GetCardRestPosition(int cardIndex)
    {
        double halfWayPoint = ((double) randomItems.Count - 1) / 2;
        return transform.position +
               new Vector3((float) ((cardIndex - halfWayPoint) * itemGapScalar / Mathf.Sqrt(randomItems.Count)), 0);
    }

    public void AddItem(GameObject clickableItem)
    {
        PlayerController.instance.AddPassiveItem(clickableItem);
        FinishEncounter();
    }

    public void FinishEncounter()
    {
        GameController.instance.LoadNextEncounter();
    }
}