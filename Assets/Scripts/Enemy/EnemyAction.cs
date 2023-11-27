using System;
using UnityEngine;

[Serializable]
public class EnemyAction
{
    public EnemyEffect enemyEffect;
    public int value;

    public void ApplyEffect()
    {
        switch (enemyEffect)
        {
            case EnemyEffect.ATTACK:
                BattleController.instance.combatPlayer.DealDamage(value);
                break;
            case EnemyEffect.DEFEND:
                EnemyManager.instance.enemy.GainArmor(value);
                break;
            case EnemyEffect.HEAL:
                EnemyManager.instance.enemy.Heal(value);
                break;
            case EnemyEffect.REST:
                EnemyManager.instance.enemy.CreatePopUp(Color.blue, "Zzzzzzz....");
                break;
            case EnemyEffect.SELF_DAMAGE:
                EnemyManager.instance.enemy.DealDamage(value);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}