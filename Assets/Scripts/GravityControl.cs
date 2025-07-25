using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityControl : MonoBehaviour
{
    
    public GravityOrbit Gravity;

    private Rigidbody Rb;

    [SerializeField]
    private float RotationSpeed = 20;
    void Start()
    {
        Rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Gravity)
        {
            Vector3 gravityUp = Vector3.zero;

            gravityUp = (transform.position - Gravity.transform.position).normalized;
            Vector3 localUp = transform.up;

            Quaternion targetrotation = Quaternion.FromToRotation(localUp, gravityUp) * transform.rotation;

            transform.up = Vector3.Lerp(transform.up, gravityUp, RotationSpeed * Time.deltaTime);

            Rb.AddForce((-gravityUp * Gravity.Gravity) * Rb.mass);
        }
    }
}
