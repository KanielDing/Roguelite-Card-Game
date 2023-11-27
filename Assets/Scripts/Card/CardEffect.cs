using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class CardEffect
{
    public PlayerEffect playerEffect;
    public int value;

    public void ApplyEffect()
    {
        switch (playerEffect)
        {
            case PlayerEffect.ATTACK:
                EventManager.TriggerEvent(EventName.ON_PLAYER_DEAL_DAMAGE.ToString(), new EventData().With(integer: value));
                EnemyManager.instance.enemy.DealDamage(value);
                break;
            case PlayerEffect.DEFEND:
                EventManager.TriggerEvent(EventName.ON_PLAYER_GAIN_BLOCK.ToString(), new EventData().With(integer: value));
                BattleController.instance.combatPlayer.GainArmor(value);
                break;
            case PlayerEffect.HEAL:
                BattleController.instance.combatPlayer.Heal(value);
                break;
            case PlayerEffect.DRAW:
                BattleController.instance.combatPlayer.CreatePopUp(Color.blue, $"Draw {value} card");
                BattleController.instance.DrawCardsFromDeckToHand(value);
                break;
            case PlayerEffect.GAIN_ENERGY:
                BattleController.instance.combatPlayer.CreatePopUp(Color.blue, $"Gained {value} energy");
                BattleController.instance.AddEnergy(value);
                break;
            case PlayerEffect.MODIFY_CARD_DRAW_PER_TURN:
                BattleController.instance.combatPlayer.CreatePopUp(Color.yellow, $"Draw {value} card per turn");
                BattleController.instance.cardDrawPerTurn += value; 
                break;
            case PlayerEffect.MODIFY_ENERGY_PER_TURN:
                BattleController.instance.combatPlayer.CreatePopUp(Color.yellow, $"Gain {value} energy per turn");
                BattleController.instance.energyPerTurn += value;
                break;
            case PlayerEffect.SELF_DAMAGE:
                BattleController.instance.combatPlayer.DealDamage(value);
                break;
            case PlayerEffect.DAMAGE_PER_0_COST_CARD_IN_HAND:
                int numZeroCostCardsInHand = 
                    BattleController.instance.playerHand.cards
                    .Where(card => card.dataCard.cost == 0).Count();
                EnemyManager.instance.enemy.DealDamage(numZeroCostCardsInHand * value);
                break;
            case PlayerEffect.REMOVE_ENEMY_ARMOR:
                EnemyManager.instance.enemy.ResetArmor();
                break;
            case PlayerEffect.DAMAGE_PER_CARD_IN_HAND:
                int numCardsInHand = BattleController.instance.playerHand.cards.Count - 1;
                EnemyManager.instance.enemy.DealDamage(numCardsInHand * value);
                break;
            case PlayerEffect.HYPER_PULSE:
                int numOfPulses = PlayerController.instance.deck.cards.Count(card => card.title.ToLower().Contains("pulse"));
                EnemyManager.instance.enemy.DealDamage(numOfPulses * value);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}