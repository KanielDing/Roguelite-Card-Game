using UnityEngine;
using UnityEngine.EventSystems;

public class PlayableCard : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public bool playable = true;
    public float dragMoveScalar = 0.5f;
    public float restReturnScalar = 0.2f;
    private bool beingDragged;
    private bool inPlayCardZone;
    public Vector3 restPosition;

    public void SetRestPosition(Vector3 newRestPosition)
    {
        restPosition = newRestPosition;
    }

    public void FreezeAndDisableCard()
    {
        playable = false;
        dragMoveScalar = 0f;
        restReturnScalar = 0f;
    }

    private void Update()
    {
        if (beingDragged)
        {
            MoveCardTowardsMouse();
        }
        else
        {
            MoveCardTowardsRestPosition();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        beingDragged = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        beingDragged = false;
        HandlePlayCard();
    }
    
    private void HandlePlayCard()
    {
        if (inPlayCardZone && playable)
        {
            BattleController.instance.playerHand.PlayCardInput(GetComponent<Card>());
        }
    }

    private void MoveCardTowardsMouse()
    {
        Vector3 targetPosition = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
        var oldPosition = transform.position;
        targetPosition.z = oldPosition.z;
        transform.position = Vector3.Slerp(oldPosition, targetPosition, dragMoveScalar);
    }
    
    private void MoveCardTowardsRestPosition()
    {
        var oldPosition = transform.position;
        transform.position = Vector3.Lerp(oldPosition, restPosition, restReturnScalar);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayZone"))
        {
            inPlayCardZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayZone"))
        {
            inPlayCardZone = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
    }
}