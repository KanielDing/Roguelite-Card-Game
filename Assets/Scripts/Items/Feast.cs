public class Feast : PassiveItem
{
    public int hpGainAfterEachBattle;

    private void HealHp(EventData eventData)
    {
        BattleController.instance.combatPlayer.Heal(hpGainAfterEachBattle);
        ActivateAnimation();
    }
    
    private void OnEnable()
    {
        EventManager.StartListening(EventName.ON_COMBAT_END.ToString(), HealHp);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventName.ON_COMBAT_END.ToString(), HealHp);
    }
}
