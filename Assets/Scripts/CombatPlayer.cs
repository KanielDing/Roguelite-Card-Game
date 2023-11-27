using System.Collections;
using UnityEngine;

public class CombatPlayer : CombatUnit
{
    public void OnBattleWin()
    {
        PlayerController.instance.currentHealth = currentHp;
        PlayerController.instance.maxHealth = maxHp;
    }
    protected override IEnumerator Die()
    {
        BattleController.instance.playerHand.cards.ForEach(card => card.GetComponent<PlayableCard>().FreezeAndDisableCard());
        yield return new WaitForSeconds(1);
        GameController.OnGameLoss();
    }
}
