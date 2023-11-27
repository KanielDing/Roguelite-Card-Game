using System;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Random = UnityEngine.Random;

public class AddCardController : MonoBehaviour
{
    public GameObject clickableCardPrefab;
    public int numCardChoices;
    public float cardGapScalar;
    public List<DataCard> possibleCards;

    private List<DataCard> availableCards = new List<DataCard>();

    void Start()
    {
        LoadCardsFromAssets();

        possibleCards = possibleCards.Where(card => card.rarity != 0).ToList();

        GetRandomCards();
        GenerateClickableCards();
    }

    private void LoadCardsFromAssets()
    {
#if UNITY_EDITOR
        possibleCards.Clear();
        String[] allCards = AssetDatabase.FindAssets("t:DataCard");
        foreach (string cardPath in allCards)
        {
            possibleCards.Add(AssetDatabase.LoadAssetAtPath<DataCard>(AssetDatabase.GUIDToAssetPath(cardPath)));
        }
#endif
    }

    private void GetRandomCards()
    {
        for (int cardChoice = 0; cardChoice < numCardChoices; cardChoice++)
        {
            var selectedUniqueCard = false;
            while (!selectedUniqueCard)
            {
                DataCard randomCard = possibleCards[Random.Range(0, possibleCards.Count)];
                if (!availableCards.Contains(randomCard))
                {
                    availableCards.Add(randomCard);
                    selectedUniqueCard = true;
                }
            }
        }
    }

    private void GenerateClickableCards()
    {
        for (int i = 0; i < availableCards.Count; i++)
        {
            var dataCard = availableCards[i];
            Card card = Instantiate(clickableCardPrefab, GetCardRestPosition(i), Quaternion.identity, transform)
                .GetComponent<Card>();
            card.InitialiseCard(dataCard);
            card.GetComponent<Clickable>().onClick.AddListener(() => AddCard(dataCard));
        }
    }

    private Vector3 GetCardRestPosition(int cardIndex)
    {
        double halfWayPoint = ((double) availableCards.Count - 1) / 2;
        return transform.position +
               new Vector3((float) ((cardIndex - halfWayPoint) * cardGapScalar / Mathf.Sqrt(availableCards.Count)), 0);
    }

    public void AddCard(DataCard dataCard)
    {
        PlayerController.instance.deck.AddCard(dataCard);
        FinishEncounter();
    }

    public void FinishEncounter()
    {
        GameController.instance.LoadNextEncounter();
    }
}
