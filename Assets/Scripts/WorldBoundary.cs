using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBoundary : MonoBehaviour
{
    // Start is called before the first frame update
    MeshRenderer Renderer;
    void Start()
    {
        Renderer = GetComponent<MeshRenderer>();
    }

    void OnCollisionEnter(Collision collision)
    {
        Renderer.enabled = true;
    }
    void OnCollisionExit(Collision collision)
    {
        Renderer.enabled = false;
    }
}
