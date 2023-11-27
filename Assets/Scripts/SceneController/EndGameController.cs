using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameController : MonoBehaviour
{
    public void ResetGame()
    {
        Destroy(PlayerController.instance.gameObject);
        Destroy(GameController.instance.gameObject);
        SceneManager.LoadScene("Main Menu");
    }
}
