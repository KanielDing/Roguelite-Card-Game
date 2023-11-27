using UnityEngine;

public class RemoveCardController : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<DeckView>().onCardClicked.AddListener(RemoveCardAndSwitchScene);
    }

    private void RemoveCardAndSwitchScene(DataCard dataCard)
    {
        PlayerController.instance.deck.Remove(dataCard);
        EndEncounter();
    }

    public void EndEncounter()
    {
        GameController.instance.LoadNextEncounter();
    }
}
