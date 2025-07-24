using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentInstancer : MonoBehaviour
{
    // Start is called before the first frame update
    public float drawDistance = 125;

    public MeshFilter[] environmentMesh;
    public MeshRenderer[] environmentMaterial;
    public Transform[] position;

    public Material a;
    public Mesh b;


    private int instanceCount;
    private int cacheCount = -1;
    private ComputeBuffer argsBuffer;
    void Start()
    {
        DrawMesh();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GetPositionIfNeeded()
    {
        if (instanceCount == cacheCount)
            return;
    }

    private void DrawMesh()
    {
        Bounds renderBound = new Bounds();
        Graphics.DrawMeshInstancedIndirect(b,0,a, renderBound, argsBuffer);
    }
}
