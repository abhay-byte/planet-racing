using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHitSurface : MonoBehaviour
{

    public Transform TreePrefab;
    public Transform RockPrefab;
    public Transform ExplodePrefab;
    public int hp = 150;
    public bool tree;
    public bool rock;

    AudioSource carHittingSource;
    void Start()
    {

    }
    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 position = contact.point;
        if (tree) { Instantiate(TreePrefab, position, rotation); }
        if (rock) { Instantiate(RockPrefab, position, rotation); }
        //Destroy(gameObject);
        hp -= (int)collision.relativeVelocity.magnitude;
        if(hp < 0) { Instantiate(ExplodePrefab, position, rotation); Destroy(gameObject); }


    }
}
