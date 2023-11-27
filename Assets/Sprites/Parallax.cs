using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Serialization;

public class Parallax : MonoBehaviour
{
    private float length;
    private Vector2 startPosition;
    [FormerlySerializedAs("camera")] public GameObject gameCamera;
    public float parallaxEffectScalar;
    void Start()
    {
        startPosition = transform.position;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    
    void Update()
    {
        Vector2 distance = gameCamera.transform.position * parallaxEffectScalar;
        
        transform.position = new Vector3(startPosition.x + distance.x, startPosition.y + distance.y, transform.position.z);
    }
}
