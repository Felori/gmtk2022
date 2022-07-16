using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomShowForDetails : MonoBehaviour
{
    [Range (0f,100f)]
    public float likeliness = 10.0f; 
    void Start()
    {
        float value = Random.Range(0.0f, 100.0f);
        if (value > likeliness)
        {
            gameObject.SetActive(false);
        }
    }
}
