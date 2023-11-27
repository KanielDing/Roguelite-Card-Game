using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    public TextMeshProUGUI itemTitleText;
    public TextMeshProUGUI itemDescriptionText;
    public Image image;
    public GameObject descriptionPanel;
    
    public void Initialise(PassiveItem passiveItem)
    {
        itemTitleText.text = passiveItem.itemName;
        itemDescriptionText.text = passiveItem.description;
        image.sprite = passiveItem.itemImage;
    }
    
    private void OnMouseEnter()
    {
        descriptionPanel.SetActive(true);
    }

    private void OnMouseExit()
    {
        descriptionPanel.SetActive(false);
    }
}
