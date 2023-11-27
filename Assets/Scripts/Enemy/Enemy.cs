using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : CombatUnit
{
    public TextMeshProUGUI intendedActionText;
    public Image imageDisplay;
    public float waitTime = 1f;

    private List<EnemyTurn> enemyTurns;
    private int turnCounter = 0;
    
    private Sprite image;

    public void TakeTurn()
    {
        ResetArmor();
        StartCoroutine(PerformAction(0));
    }
    public IEnumerator PerformAction(int actionInTurnCount)
    {
        yield return new WaitForSeconds(waitTime);
        if (actionInTurnCount >= enemyTurns[turnCounter].actions.Count)
        {
            OnEnemyTurnEnd();
        }
        else
        {
            enemyTurns[turnCounter].actions[actionInTurnCount].ApplyEffect();
            StartCoroutine(PerformAction(actionInTurnCount + 1));
        }
    }

    private void OnEnemyTurnEnd()
    {
        turnCounter++;
        turnCounter %= enemyTurns.Count;
        intendedActionText.text = enemyTurns[turnCounter].description;
        BattleController.instance.OnPlayerTurnStart();
    }

    public void InitialiseEnemy(DataEnemy initialiseDataEnemy)
    {
        title = initialiseDataEnemy.title;
        image = initialiseDataEnemy.image;
        enemyTurns = initialiseDataEnemy.actions;
        maxHp = initialiseDataEnemy.health;
        currentHp = maxHp;
        intendedActionText.text = enemyTurns[0].description;
        InitialiseEnemyDisplay();
    }

    private void InitialiseEnemyDisplay()
    {
        titleText.text = title;
        imageDisplay.sprite = image;
        UpdateHealthBarAndArmorValue();
    }

    protected override IEnumerator Die()
    {
        EventManager.TriggerEvent(EventName.ON_COMBAT_END.ToString(), new EventData());
        BattleController.instance.playerHand.cards.ForEach(card => card.GetComponent<PlayableCard>().FreezeAndDisableCard());
        yield return new WaitForSeconds(1);
        BattleController.instance.OnEnemyDeath(this);
    }
}