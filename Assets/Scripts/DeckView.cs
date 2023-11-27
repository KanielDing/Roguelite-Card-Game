using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeckView : MonoBehaviour
{
    
    [SerializeField]
    public UnityEvent<DataCard> onCardClicked = new UnityEvent<DataCard>();
    
    public GameObject clickableCardPrefab;
    public GameObject displayCardsContainer;

    public int numPerRow;
    public float xSpacing, ySpacing;
    public float xOffset, yOffset;
    public float containerSizeScalar = 210f;

    private void Start()
    {
        ArrangeCards();
    }

    private void ArrangeCards()
    {
        List<DataCard> dataCards = PlayerController.instance.deck.cards;
        SetScaleOfContainer(dataCards.Count);
        for (int i = 0; i < dataCards.Count; i++)
        {
            Card card = Instantiate(
                clickableCardPrefab,
                new Vector3(((i % numPerRow) * xSpacing) + xOffset,  ((i / numPerRow) * ySpacing) + yOffset, 0),
                Quaternion.identity, 
                displayCardsContainer.transform
            ).GetComponent<Card>();
            DataCard dataCard = dataCards[i];
            card.InitialiseCard(dataCard);
            card.GetComponent<Clickable>().onClick.AddListener((() => onCardClicked.Invoke(dataCard))); 
        }
    }

    private void SetScaleOfContainer(int cardCount)
    {
        RectTransform rectTransform = displayCardsContainer.GetComponent<RectTransform>();
        float ySize = (float) (Math.Ceiling(cardCount /(double) numPerRow) * -containerSizeScalar + (2 * containerSizeScalar));
        ySize = Mathf.Min(ySize, 0f);
        rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, ySize);
    }

}
