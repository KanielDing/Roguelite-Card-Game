using System.Collections.Generic;
using UnityEngine;

public class PlayerController: MonoBehaviour
{
    public static PlayerController instance;

    public int maxHealth;
    public int currentHealth;
    public CardStore deck;

    public List<PassiveItem> items;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void AddPassiveItem(GameObject passiveItem)
    {
        PassiveItem newPassiveItem = Instantiate(passiveItem, transform).GetComponent<PassiveItem>();
        items.Add(newPassiveItem);
        FindObjectOfType<ItemsView>()?.UpdateItemDisplays();
    }
}