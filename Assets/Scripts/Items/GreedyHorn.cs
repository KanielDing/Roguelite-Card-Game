﻿using Random = UnityEngine.Random;

public class GreedyHorn : PassiveItem
{
    public float duplicateChance;

    private void AttemptDuplicate(EventData eventData)
    {
        if (eventData.dataCard.cost != 0) return;
        if (!(duplicateChance > Random.value)) return;
        BattleController.instance.AddNewCardToDiscardPile(eventData.dataCard);
        ActivateAnimation();
    }
    
    private void OnEnable()
    {
        EventManager.StartListening(EventName.ON_PLAYER_PLAY_CARD.ToString(), AttemptDuplicate);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventName.ON_PLAYER_PLAY_CARD.ToString(), AttemptDuplicate);
    }
}
