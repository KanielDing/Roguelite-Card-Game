using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public GameObject enemyDisplayPrefab;
        
    public Enemy enemy;
    public DataEnemy dataEnemy;
    public Vector3 enemyPosition;

    private void Start()
    {
        instance = this;
        try
        {
            DataEnemy currentDataEnemy = GameController.instance.currentEnemy;
            if (currentDataEnemy != null)
            {
                dataEnemy = currentDataEnemy;
            }
        }
        catch
        {
            Debug.Log("No Enemy Currently loaded, falling back to default.");
        }
        enemy = Instantiate(enemyDisplayPrefab, enemyPosition, Quaternion.identity, transform).GetComponent<Enemy>();
        enemy.InitialiseEnemy(dataEnemy);
    }
}
