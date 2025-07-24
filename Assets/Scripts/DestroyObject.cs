using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] private float destroyWithIn;
    [SerializeField] private GameObject destroyEffect;

    private void Start()
    {
        destroyWithIn = Random.Range(6, destroyWithIn);
    }
    private void FixedUpdate()
    {
        if (destroyWithIn > 0)
        {
            destroyWithIn -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
            Instantiate(destroyEffect, 
                gameObject.transform.localPosition,
                gameObject.transform.rotation).transform.localScale = gameObject.transform.localScale;
        }
    
    }
}
