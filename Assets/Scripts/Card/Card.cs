using UnityEngine;

public class Card : MonoBehaviour
{
    public DataCard dataCard;

    public void InitialiseCard(DataCard dataCard)
    {
        this.dataCard = dataCard;
        GetComponent<CardDisplay>()?.UpdateCardDisplay(this.dataCard);
    }
}
