using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public Image imageDisplay;
    public TextMeshProUGUI costText;

    private int cost;
    private string title;
    private string description;
    private Sprite image;

    private bool beingDragged;

    public void UpdateCardDisplay(DataCard dataCard)
    {
        cost = dataCard.cost;
        title = dataCard.title;
        description = dataCard.description;
        image = dataCard.image;
        InitialiseCardDisplay();
    }
    
    private void InitialiseCardDisplay()
    {
        costText.text = cost.ToString();
        titleText.text = title;
        descriptionText.text = description;
        imageDisplay.sprite = image;
    }
}