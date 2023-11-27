using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class DataCard : ScriptableObject
{
    public int rarity = 0;
    public int cost;
    public string title;
    public string description;
    public Sprite image;
    public List<CardEffect> cardEffects;
    public bool exhausts = false;
}