using System;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public class RestingStar : PassiveItem
{
    public int healPerEnergy;

    private void HealForEnergyRemaining(EventData eventData)
    {
        int remainingEnergy = BattleController.instance.GetCurrentEnergy();
        if (remainingEnergy > 0)
        {
            ActivateAnimation();
            BattleController.instance.combatPlayer.Heal(healPerEnergy * remainingEnergy);
        }
    }
    
    private void OnEnable()
    {
        EventManager.StartListening(EventName.ON_PLAYER_END_TURN.ToString(), HealForEnergyRemaining);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventName.ON_PLAYER_END_TURN.ToString(), HealForEnergyRemaining);
    }
}