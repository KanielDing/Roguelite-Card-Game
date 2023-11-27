using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class CardStore : MonoBehaviour
{
    public List<DataCard> cards;
    private static readonly Random Random = new();

    public void Shuffle()
    {
        var n = cards.Count;  
        while (n > 1) {  
            n--;  
            var k = Random.Next(n + 1);  
            (cards[k], cards[n]) = (cards[n], cards[k]);
        }
    }

    public DataCard GetRandomCard(bool removeCard = false)
    {
        int randomCardIndex = Random.Next(cards.Count);
        DataCard randomCard = cards[randomCardIndex];
        if (removeCard)
        {
            cards.RemoveAt(randomCardIndex);
        }
        return randomCard;
    }

    public DataCard GetTopCard(bool removeCard = false)
    {
        DataCard randomCard = cards[0];
        if (removeCard)
        {
            cards.RemoveAt(0);
        }
        return randomCard;
    }

    public void AddCard(DataCard newCard)
    {
        cards.Add(newCard);
    }

    public void AddCards(List<DataCard> dataCards)
    {
        cards.AddRange(dataCards);
    }

    public void Clear()
    {
        cards.Clear();
    }

    public List<DataCard> GetAllCards()
    {
        return cards;
    }

    public int Count()
    {
        return cards.Count;
    }

    public void Remove(DataCard dataCard)
    {
        cards.Remove(dataCard);
    }
}
