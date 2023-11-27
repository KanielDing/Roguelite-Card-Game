using System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EchoPulse : PassiveItem
{
    public int echoDamage;

    private int counter = 3;

    private void OnPlayCard(EventData eventData)
    {
        if (eventData.dataCard.name.ToLower().Contains("pulse"))
        {
            if (counter > 0)
            {
                EnemyManager.instance.enemy.DealDamage(echoDamage * counter);
                ActivateAnimation();
            }
            counter++;
        }
    }

    private void ResetCounter(EventData eventData)
    {
        counter = 0;
    }
    
    private void OnEnable()
    {
        EventManager.StartListening(EventName.ON_PLAYER_PLAY_CARD.ToString(), OnPlayCard);
        EventManager.StartListening(EventName.ON_PLAYER_START_TURN.ToString(), ResetCounter);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventName.ON_PLAYER_PLAY_CARD.ToString(), OnPlayCard);
        EventManager.StopListening(EventName.ON_PLAYER_START_TURN.ToString(), ResetCounter);
    }
}