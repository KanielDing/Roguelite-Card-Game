using UnityEngine;
using UnityEngine.Events;

public class Clickable : MonoBehaviour
{
    [SerializeField]
    public UnityEvent onClick = new UnityEvent();

    private void OnMouseDown()
    {
        onClick.Invoke();
    }
}
