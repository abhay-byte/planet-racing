using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarColliders : MonoBehaviour
{
    [SerializeField] private Collider body;
    [SerializeField] private Collider engine;


    public Collider Body { get => body; set => body = value; }
    public Collider Engine { get => engine; set => engine = value; }
}
