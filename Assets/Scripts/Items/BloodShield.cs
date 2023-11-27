using UnityEngine;
using UnityEngine.Events;


public class BloodShield : PassiveItem
{
    public int damageDealtOnBlock = 2;

    private void OnEnable()
    {
        EventManager.StartListening(EventName.ON_PLAYER_GAIN_BLOCK.ToString(), DealDamage);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventName.ON_PLAYER_GAIN_BLOCK.ToString(), DealDamage);
    }

    private void DealDamage(EventData eventData)
    {
        EnemyManager.instance.enemy.DealDamage(damageDealtOnBlock);
        ActivateAnimation();
    }
}
