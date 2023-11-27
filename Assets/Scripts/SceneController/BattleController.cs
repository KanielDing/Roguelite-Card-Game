using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public static BattleController instance;
    public CardStore deck;
    public CardStore discardPile;
    public PlayerHand playerHand;

    public int cardDrawPerTurn;
    public int energyPerTurn;

    public TextMeshProUGUI energyText;
    public TextMeshProUGUI discardCounterText;
    public TextMeshProUGUI deckCounterText;

    private int currentEnergy = 0;
    public bool isPlayerTurn;

    public CombatPlayer combatPlayer;

    private void Start()
    {
        instance = this;
        OnFightStart();
    }

    private void OnFightStart()
    {
        isPlayerTurn = true;
        LoadPlayerIntoBattle();
        deck.Shuffle();
        DrawCardsFromDeckToHand(cardDrawPerTurn);
        currentEnergy = energyPerTurn;
        UpdateDisplays();
        EventManager.TriggerEvent(EventName.ON_PLAYER_START_TURN.ToString(), new EventData());
    }

    private void LoadPlayerIntoBattle()
    {
        deck.cards.AddRange(PlayerController.instance.deck.cards);
        combatPlayer.SetHpAndMaxHp(PlayerController.instance.currentHealth, PlayerController.instance.maxHealth);
    }

    public void OnPlayerTurnEnd()
    {
        if (!isPlayerTurn) return;
        playerHand.DiscardHand();
        isPlayerTurn = false;
        UpdateDisplays();
        EventManager.TriggerEvent(EventName.ON_PLAYER_END_TURN.ToString(), new EventData());

        EnemyManager.instance.enemy.TakeTurn();
    }

    public void OnPlayerTurnStart()
    {
        combatPlayer.ResetArmor();
        EventManager.TriggerEvent(EventName.ON_PLAYER_START_TURN.ToString(), new EventData());

        DrawCardsFromDeckToHand(cardDrawPerTurn);
        currentEnergy = energyPerTurn;
        UpdateDisplays();
        isPlayerTurn = true;
    }

    public bool HasEnergy(int energy)
    {
        return currentEnergy >= energy;
    }

    public int GetCurrentEnergy()
    {
        return currentEnergy;
    }

    public void SubtractEnergy(int energy)
    {
        currentEnergy -= energy;
        UpdateDisplays();
    }

    public void DrawCardsFromDeckToHand(int numCards)
    {
        List<DataCard> drawnDataCards = new List<DataCard>();
        for (int i = 0; i < numCards; i++)
        {
            if (deck.Count() == 0) ShuffleDiscardPileIntoDeck();
            if (deck.Count() != 0) drawnDataCards.Add(deck.GetTopCard(true));
        }

        playerHand.AddCards(drawnDataCards);
        UpdateDisplays();
    }

    public void AddEnergy(int energy)
    {
        currentEnergy += energy;
        UpdateDisplays();
    }

    public void UpdateDisplays()
    {
        energyText.text = currentEnergy.ToString();
        discardCounterText.text = discardPile.Count().ToString();
        deckCounterText.text = deck.Count().ToString();
    }

    public void OnEnemyDeath(Enemy enemy)
    {
        combatPlayer.OnBattleWin();
        GameController.instance.LoadNextEncounter();
    }
    
    public void AddNewCardToDiscardPile(DataCard dataCard)
    {
        discardPile.AddCard(dataCard);
    }

    public void AddNewCardToDrawPile(DataCard dataCard)
    {
        deck.AddCard(dataCard);
    }
    
    public void AddNewCardToPlayerHand(DataCard dataCard)
    {
        playerHand.AddCards(new List<DataCard>{dataCard});
    }

    private void ShuffleDiscardPileIntoDeck()
    {
        deck.AddCards(discardPile.GetAllCards());
        discardPile.Clear();
        deck.Shuffle();
        UpdateDisplays();
    }
}