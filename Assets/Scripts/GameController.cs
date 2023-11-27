using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public DataEnemy currentEnemy;

    public List<DataEnemy> enemyList;
    public float fightChance;

    private int fightCounter = 0;

    public void LoadNextEncounter()
    {
        if (fightCounter == enemyList.Count)
        {
            OnGameWin();
            return;
        }

        var nextEncounter = GetRandomEncounter();
        LoadEncounter(nextEncounter);
    }

    private EncounterType GetRandomEncounter()
    {
        if (Random.value <= fightChance)
        {
            return EncounterType.FIGHT;
        }
        else
        {
            var randomSceneFloat = Random.value;
            if (randomSceneFloat < 0.6) return EncounterType.CARD_CHOICE;
            return randomSceneFloat < 0.8 ? EncounterType.CARD_REMOVE : EncounterType.ITEM_CHOICE;
        }
    }

    private void LoadEncounter(EncounterType encounterType)
    {
        switch (encounterType)
        {
            case EncounterType.FIGHT:
                currentEnemy = enemyList[fightCounter];
                fightCounter++;
                SceneManager.LoadScene("Battle Scene");
                break;
            case EncounterType.CARD_CHOICE:
                SceneManager.LoadScene("Card Add Scene");
                break;
            case EncounterType.CARD_REMOVE:
                SceneManager.LoadScene("Card Remove Scene");
                break;
            case EncounterType.WIN:
                SceneManager.LoadScene("Win Scene");
                break;
            case EncounterType.ITEM_CHOICE:
                SceneManager.LoadScene("Item Add Scene");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public static void OnGameLoss()
    {
        SceneManager.LoadScene("Lose Scene");
    }

    private static void OnGameWin()
    {
        SceneManager.LoadScene("Win Scene");
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }
}