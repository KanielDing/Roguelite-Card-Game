using TMPro;
using UnityEngine;

public class TextPopup : MonoBehaviour
{
    public float liveTime;
    public TextMeshProUGUI textMesh;

    private void Start()
    {
        Destroy(gameObject, liveTime);
    }

    public void Initialise(Color color, string textMessage)
    {
        textMesh.text = textMessage;
        textMesh.color = color;
    }
}
