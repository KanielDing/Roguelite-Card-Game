using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public GameObject cardDisplayPrefab;
    public Vector3 deckSpawnPosition;

    public float cardGapScalar;
    public float cardEffectTimeDelay = 0.3f;

    public List<Card> cards = new List<Card>();

    public void DiscardHand()
    {
        for (int i = cards.Count() - 1; i > -1; i--)
        {
            DiscardCard(cards[i], false);
        }
        cards.Clear();
    }

    public void AddCards(List<DataCard> dataCards)
    {
        for (int i = 0; i < dataCards.Count; i++)
        {
            Card newCard = Instantiate(cardDisplayPrefab, deckSpawnPosition, Quaternion.identity, transform).GetComponent<Card>();
            newCard.InitialiseCard(dataCards[i]);
            newCard.GetComponent<PlayableCard>().SetRestPosition(GetCardRestPosition(i));
            cards.Add(newCard);
            EventManager.TriggerEvent(EventName.ON_PLAYER_DRAW_CARD.ToString(), new EventData().With(card: newCard, dataCard: newCard.dataCard));
        }
        RealignCards();
    }

    public void PlayCardInput(Card card)
    {
        if (BattleController.instance.isPlayerTurn && BattleController.instance.HasEnergy(card.dataCard.cost))
        {
            BattleController.instance.SubtractEnergy(card.dataCard.cost);
            card.GetComponent<PlayableCard>().FreezeAndDisableCard();
            StartCoroutine(PlayCardEffect(card, 0));
            EventManager.TriggerEvent(EventName.ON_PLAYER_PLAY_CARD.ToString(), new EventData().With(card: card, dataCard: card.dataCard));
        }
    }

    private IEnumerator PlayCardEffect(Card card, int iterator)
    {
        if (iterator >= card.dataCard.cardEffects.Count)
        {
            DiscardCard(card, true);
            BattleController.instance.UpdateDisplays();
        }
        else
        {
            yield return new WaitForSeconds(iterator == 0? 0: cardEffectTimeDelay);
            card.dataCard.cardEffects[iterator].ApplyEffect();
            StartCoroutine(PlayCardEffect(card, iterator + 1));
        }
    }

    private void DiscardCard(Card card, bool played)
    {
        RemoveCardFromHand(card);
        if (played & card.dataCard.exhausts)
        {
            EventManager.TriggerEvent(EventName.ON_PLAYER_EXHAUST_CARD.ToString(), new EventData().With(card: card, dataCard: card.dataCard));
            return;
        }
        BattleController.instance.discardPile.AddCard(card.dataCard);
    }
    
    private void RemoveCardFromHand(Card card)
    {
        cards.Remove(card);
        Destroy(card.gameObject);
        RealignCards();
    }
    
    private Vector3 GetCardRestPosition(int cardIndex)
    {
        double halfWayPoint = ((double)cards.Count() - 1) / 2;
        return transform.position + new Vector3((float) (((double)cardIndex - halfWayPoint) * cardGapScalar / Mathf.Sqrt(cards.Count())), 0);
    }
    
    private void RealignCards()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].GetComponent<PlayableCard>().SetRestPosition(GetCardRestPosition(i));
        }
    }
}
