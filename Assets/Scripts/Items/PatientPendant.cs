
public class PatientPendant : PassiveItem
{
    public int gainedBlock;

    private int counter = 0;

    private void OnPlayCard(EventData eventData)
    {
        counter++;
    }

    private void OnTurnEnd(EventData eventData)
    {
        if (counter < 3)
        {
            BattleController.instance.combatPlayer.GainArmor(gainedBlock);
            ActivateAnimation();
        }
        counter = 0;
    }
    
    private void OnEnable()
    {
        EventManager.StartListening(EventName.ON_PLAYER_PLAY_CARD.ToString(), OnPlayCard);
        EventManager.StartListening(EventName.ON_PLAYER_END_TURN.ToString(), OnTurnEnd);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventName.ON_PLAYER_PLAY_CARD.ToString(), OnPlayCard);
        EventManager.StopListening(EventName.ON_PLAYER_START_TURN.ToString(), OnTurnEnd);
    }
}
