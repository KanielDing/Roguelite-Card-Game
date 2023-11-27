using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class DataEnemy : ScriptableObject
{
    public string title;
    public int health;
    public bool isBoss;
    public Sprite image;
    
    public List<EnemyTurn> actions;

}
