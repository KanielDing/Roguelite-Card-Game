using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public float shakeTimeDivider = 30f;
    public float magnitudeScalar = 0.1f;

    private void Start()
    {
        instance = this;
    }

    public void Shake(float magnitude)
    {
        StartCoroutine(DoCameraShake( magnitude / shakeTimeDivider, magnitude));
    }

    private IEnumerator DoCameraShake(float duration, float magnitude)
    {
        var originalPos = transform.position;
        var elapsed = 0.0f;

        while (elapsed < duration)
        {
            var x = Random.Range(-1f, 1f) * magnitude * magnitudeScalar;
            var y = Random.Range(-1f, 1f) * magnitude * magnitudeScalar;
            
            transform.localPosition = new Vector3(x, y, originalPos.y);

            elapsed += Time.deltaTime;
            
            yield return null;
        }
    }
}
